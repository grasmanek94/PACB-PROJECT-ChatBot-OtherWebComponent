using System.Windows.Forms;
using Gecko;
using Gecko.Windows;
using Gecko.DOM;
using Gecko.Collections;
using System.Diagnostics;

namespace ChatBot
{
    public class ChatBot
    {
        /*
        Something global.....
            */
            
        public delegate void OnBeginSearchingDelegate(ChatBot chatbot);
        public event OnBeginSearchingDelegate OnBeginSearching;

        public delegate void OnBeginChatDelegate(ChatBot chatbot);
        public event OnBeginChatDelegate OnBeginChat;

        public delegate void OnNewMessageDelegate(ChatBot chatbot, Message message, int messageCount, int messageCountNotMine);
        public event OnNewMessageDelegate OnNewMessage;

        public delegate void OnEndChatDelegate(ChatBot chatbot);
        public event OnEndChatDelegate OnEndChat;

        public delegate void OnChatBotBrowserGotFocusDelegate(ChatBot chatbot);
        public event OnChatBotBrowserGotFocusDelegate OnChatBotBrowserGotFocus;

        public enum State
        {
            Unknown,
            Loading,
            Searching,
            Chatting,
            Stopped
        };

        private GeckoWebBrowser browser;
        private bool loaded;
        private bool chatBegun;
        private Messages messages;
        private bool ignoreUntilEnd;
        public bool IgnoreMessageDates { get; set; }
        private int CurrentStoryMessage { get; set; }
        public bool Paused { get; set; }
        private Stopwatch stopwatch;
        private string[] story_mode;

        private void CheckStoryMessage(string[] story_messages, long milliseconds_between_messages, long milliseconds_before_first_message)
        {
            if(story_messages.Length <= CurrentStoryMessage)
            {
                return;
            }

            if((CurrentStoryMessage == 0 && stopwatch.ElapsedMilliseconds > milliseconds_before_first_message) || (CurrentStoryMessage > 0 && CurrentStoryMessage < story_messages.Length && stopwatch.ElapsedMilliseconds > milliseconds_between_messages))
            {
                SendMessage(story_messages[CurrentStoryMessage]);
                ++CurrentStoryMessage;
                stopwatch.Restart();
            }
        }

        public void StopStory()
        {
            CurrentStoryMessage = 9999;
        }

        private void RestartStory()
        {
            CurrentStoryMessage = 0;
            stopwatch.Restart();
        }

        private GeckoElement logwrapper { get { return browser.Document.GetElementById("logwrapper"); } }
        private GeckoElement chatStartStopButton { get { return browser.Document.GetElementById("chatStartStopButton"); } }
        private GeckoElement chatMessageInput { get { return browser.Document.GetElementById("chatMessageInput"); } }
        private GeckoElement endChatButton
        {
            get
            {
                GeckoElement modal_quit = browser.Document.GetElementById("modal-quit");
                if(modal_quit != null)
                {
                    IDomHtmlCollection<GeckoElement> elements = modal_quit.GetElementsByTagName("button");
                    if (elements.Length >= 3)
                    {
                        return elements[2];
                    }
                }
                return null;
            }
        }

        public int Identifier { get; private set; }
        public TabPage LinkedTabPage { get; set; }
        public string TabText
        {
            get
            {
                if (LinkedTabPage != null)
                {
                    return LinkedTabPage.Text;
                }
                return "";
             }
            set
            {
                if (LinkedTabPage != null)
                {
                    LinkedTabPage.Text = Identifier.ToString() + " " + value;
                }
            }
        }

        private State lastKnownState;

        public Messages GetMessages()
        {
            return messages;
        }

        public ChatBot(int id = 0, string[] story_mode_strings = null)
        {
            Gecko.Xpcom.Initialize(".\\xulrunner");

            Identifier = id;

            browser = new GeckoWebBrowser();
            loaded = false;
            chatBegun = false;
            ignoreUntilEnd = false;
            messages = new Messages();
            lastKnownState = State.Unknown;
            LinkedTabPage = null;
            IgnoreMessageDates = false;
            stopwatch = new Stopwatch();
            Paused = false;

            browser.DocumentCompleted += DocumentCompleted;
            browser.CreateWindow += Browser_CreateWindow2;
            browser.DomClick += Browser_GotFocus;
            messages.OnNewMessage += OnNewMessageHandler;

            browser.Navigate("http://www.praatanoniem.nl/");
            browser.Visible = true;

            if(story_mode_strings != null && story_mode_strings.Length > 0)
            {
                story_mode = new string[story_mode_strings.Length];
                story_mode_strings.CopyTo(story_mode, 0);
            }
            else
            {
                story_mode = null;
            }
        }

        private void Browser_CreateWindow2(object sender, GeckoCreateWindowEventArgs e)
        {
            e.Cancel = true;
        }

        public bool IsIgnored()
        {
            return ignoreUntilEnd;
        }

        private void Browser_GotFocus(object sender, DomMouseEventArgs e)
        {
            if(OnChatBotBrowserGotFocus != null && e.ScreenX != 0)//X != 0 so we ignore invoked lcick events as they default to 0,0 (CAN BE NEGATIVE IF ON OTHER SCREENS)
            {
                OnChatBotBrowserGotFocus(this);
            }
        }

        private void OnNewMessageHandler(Messages sender, Message message, int messageCount, int messageCountNotMine)
        {
            //and then just pass on to the creator delegate:
            if (OnNewMessage != null)
            {
                OnNewMessage(this, message, messageCount, messageCountNotMine);
            }
        }

        public void Process()
        {
            State newState = GetState();
            if(newState != lastKnownState)
            {
                lastKnownState = newState;
                switch (lastKnownState)
                {
                    case State.Searching:
                        if (OnBeginSearching != null)
                        {
                            OnBeginSearching(this);
                        }
                        break;
                    case State.Chatting:
                        ignoreUntilEnd = false;
                        IgnoreMessageDates = false;
                        messages.Reset();
                        RestartStory();
                        if (OnBeginChat != null)
                        {
                            OnBeginChat(this);
                        }
                        break;
                    case State.Stopped:
                        if (OnEndChat != null)
                        {
                            OnEndChat(this);
                        }
                        break;
                }
            }

            if (lastKnownState == State.Chatting && !ignoreUntilEnd)
            {
                messages.ProcessNewMessages(logwrapper.GetElementsByTagName("div"));

                Message lastMessage = GetMessages().GetLastMessage();
                if (!IgnoreMessageDates && lastMessage != null && lastMessage.Elapsed() > 60000.0)
                {
                    EndCurrentChat();
                }
                //if (searching == null && CanStartSearching() && !Paused)
                //{
                //    searching = bot;
                //    StartNextChat();
                //}
                if (GetLastKnownState() == ChatBot.State.Chatting && story_mode != null)
                {
                    CheckStoryMessage(story_mode, 2500, 10000);
                }
            }
        }

        public bool StartNextChat()
        {
            if (lastKnownState != State.Stopped)
            {
                return false;
            }

            if (chatBegun == false)
            {
                chatBegun = true;
                (browser.Document.GetElementById("rosetta-init") as GeckoHtmlElement).Click();
            }
            else
            {
                (chatStartStopButton as GeckoHtmlElement).Click();
            }
            return true;
        }

        public bool EndCurrentChat()
        {
            GeckoHtmlElement endChatHtmlBtn = endChatButton as GeckoHtmlElement;
            if(endChatHtmlBtn != null)
            {
                endChatHtmlBtn.Click();
                return true;
            }
            return false;
        }

        private bool IsSearching()
        {
            if(logwrapper == null)
            {
                return false;
            }
            var collection = logwrapper.GetElementsByTagName("div");
            if(collection.Length == 1)
            {
                return collection[0].TextContent[0] == 'W';
            }
            return false;
        }

        private bool IsChatting()
        {
            if(chatMessageInput == null)
            {
                return false;
            }
            return chatMessageInput.GetAttribute("disabled") == null;
        }

        private bool IsStopped()
        {
            return browser.Document.GetElementById("rosetta-chat-end-banner") != null;
        }

        private void DocumentCompleted(object sender, Gecko.Events.GeckoDocumentCompletedEventArgs e)
        {
            loaded = true;
        }

        public void SendMessage(string message)
        {
            if (message != null)
            {
                (chatMessageInput as GeckoInputElement).Value = message;
                (browser.Document.GetElementById("chatSendButton") as GeckoHtmlElement).Click();
            }
        }

        private State GetState()
        {
            if (!loaded)
            {
                return State.Loading;
            }

            if (IsStopped() || chatBegun == false)
            {
                return State.Stopped;
            }

            if (IsChatting())
            {
                return State.Chatting;
            }

            if (IsSearching())
            {
                return State.Searching;
            }

            return State.Unknown;
        }

        public State GetLastKnownState()
        {
            return lastKnownState;
        }

        public GeckoWebBrowser GetControl()
        {
            return browser;
        }

        public bool CountForSearching()
        {
            return lastKnownState != State.Chatting;
        }

        public bool CanStartSearching()
        {
            return lastKnownState == State.Stopped;
        }

        public void IgnoreUntilEnd()
        {
            ignoreUntilEnd = true;
        }

        public override string ToString()
        {
            return Identifier.ToString();
        }
    }
}
