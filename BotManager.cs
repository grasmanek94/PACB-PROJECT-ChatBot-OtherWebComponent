using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace ChatBot
{
    public class BotManager
    {
        public delegate void OnChatBotAddedDelegate(BotManager manager, ChatBot chatbot);
        public event OnChatBotAddedDelegate OnChatBotAdded;

        public delegate void OnBeginSearchingDelegate(BotManager manager, ChatBot chatbot);
        public event OnBeginSearchingDelegate OnBeginSearching;

        public delegate void OnBeginChatDelegate(BotManager manager, ChatBot chatbot);
        public event OnBeginChatDelegate OnBeginChat;

        public delegate void OnNewMessageDelegate(BotManager manager, ChatBot chatbot, Message message, int messageCount, int messageCountNotMine);
        public event OnNewMessageDelegate OnNewMessage;

        public delegate void OnEndChatDelegate(BotManager manager, ChatBot chatbot);
        public event OnEndChatDelegate OnEndChat;

        public delegate void OnChatBotBrowserGotFocusDelegate(BotManager manager, ChatBot chatbot);
        public event OnChatBotBrowserGotFocusDelegate OnChatBotBrowserGotFocus;

        public delegate void OnBotManagerBeforeUpdateDelegate(BotManager manager);
        public event OnBotManagerBeforeUpdateDelegate OnBotManagerBeforeUpdate;

        public delegate void OnBotManagerAfterUpdateDelegate(BotManager manager);
        public event OnBotManagerAfterUpdateDelegate OnBotManagerAfterUpdate;

        private List<ChatBot> bots;
        private Timer timer;
        private ChatBot searching;
        private bool paused;
        private long searchRestore;

        public BotManager()
        {
            bots = new List<ChatBot>();

            timer = new Timer();
            timer.Tick += Timer_Tick;
            timer.Interval = 75;
            timer.Start();

            paused = false;
            searching = null;

            searchRestore = 0;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if(++searchRestore >= 12000) // 10 min
            {
                searchRestore = 0;
                Reset();                
            }

            if(OnBotManagerBeforeUpdate != null)
            {
                OnBotManagerBeforeUpdate(this);
            }

            foreach(ChatBot bot in bots)
            {
                bot.Process();
            }

            if (OnBotManagerAfterUpdate != null)
            {
                OnBotManagerAfterUpdate(this);
            }
        }

        public ChatBot AddBot()
        {
            ChatBot bot = new ChatBot(bots.Count);

            bot.OnBeginSearching += Bot_OnBeginSearching;
            bot.OnBeginChat += Bot_OnBeginChat;
            bot.OnNewMessage += Bot_OnNewMessage;
            bot.OnEndChat += Bot_OnEndChat;
            bot.OnChatBotBrowserGotFocus += Bot_OnChatBotBrowserGotFocus;

            bots.Add(bot);

            if(OnChatBotAdded != null)
            {
                OnChatBotAdded(this, bot);
            }
            return bot;
        }

        private void Bot_OnChatBotBrowserGotFocus(ChatBot chatbot)
        {
            if (OnChatBotBrowserGotFocus != null)
            {
                OnChatBotBrowserGotFocus(this, chatbot);
            }
        }

        private void Bot_OnEndChat(ChatBot chatbot)
        {
            if (OnEndChat != null)
            {
                OnEndChat(this, chatbot);
            }
        }

        private void Bot_OnNewMessage(ChatBot chatbot, Message message, int messageCount, int messageCountNotMine)
        {
            if (OnNewMessage != null)
            {
                OnNewMessage(this, chatbot, message, messageCount, messageCountNotMine);
            }
        }

        private void Bot_OnBeginChat(ChatBot chatbot)
        {
            if(chatbot == searching)
            {
                searching = null;
            }

            if (OnBeginChat != null)
            {
                OnBeginChat(this, chatbot);
            }
        }

        private void Bot_OnBeginSearching(ChatBot chatbot)
        {
            if (OnBeginSearching != null)
            {
                OnBeginSearching(this, chatbot);
            }
        }

        public List<ChatBot> GetBots()
        {
            return bots;
        }

        public void Resume()
        {
            paused = false;
        }

        public void Pause()
        {
            paused = true;
        }

        public void Reset()
        {
            searching = null;
        }
    }
}
