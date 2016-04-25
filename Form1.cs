using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace ChatBot
{
    public partial class Form1 : Form
    {
        private BotManager bm;

        private string[] forbidden_index/* =
        {
            "naakt",
            "ruilen",
            "uitwisselen",
            "ben een jongen",
            "ben jongen",
            "jongen hier",
            "geil",
            "ik ook",
            "man hier",
            "ik zoek ook",
            "j@@",
            "j @@",
            "jong hier",
            "lul",
            "doei",
            "jbijna",
            "j bijna",
            "stijve",
            "fuck",
            "kut",
            "meester",
            "Ga weg",
            "jammer",
            "homo",
            "gay",
            "flikker",
            "j hier",
            "man@@",
            "man @@",
            "jij weer",
            "zoek ok",
            "kanker",
            "rot op",
            "sukkel",
            "telkens",
            "rotop",
            "jongen@@",
            "jongen @@",
            "jongen zoek",
            "jongen die zoek",
            "jong zoek",
            "j zoek",
            "ben je meisj",
            "ben je een meisj",
            "zoek een leuk meisje",
            "zoek een meisje",
            "jonge@@",
            "jonge @@",
            "Zoek een leven",
            "pedo",
            "zus",
            "gozer",
            "kerel",
            "jongen of meisje",
            "ben een j",
            "hier jong",
            "hoer",
        }*/;

        private string[] forbidden_starts_with/* =
        {
            "haay mandy hier",
            "heey geile meid 24",
            "j ",
            "meisje?",
            "jongen",
            "man",
            "kk",
            "kkr",
            ".",
            "jm"
        }*/;

        public static string[] story_mode/* =
        {
            "Ik ben een robot en ik ben hier om jou te vervelen. Ik heb wel een mooi verhaaltje voor je... Hou je het vol tot 't einde? Opmerkingen? Vragen? mail: het-gekke-robotje@0dl.nl / whatsapp: 06 86 366 906 / www.facebook.com/rt9x9",
            "Een robot in Zuid Beierland",
            "Die was aan lager wal beland",
            "En in café De Malle Moer",
            "Vielen zijn tranen op de vloer",
            "-----",
            "Want zijn vriendin, (afwasmachine,",
            "die werkzaam was in een kantine),",
            "Had de verloving uitgemaakt",
            "Daardoor was hij van streek geraakt",
            "-----",
            "De kroegbaas zei: \"Zo zijn de vrouwen,",
            "Ze zijn gewoon niet te vertrouwen\",",
            "Waarop de robot nog eens snikte",
            "En daarbij ook instemmend knikte",
            "-----",
            "Hij zei: \"Laatst leek ze 't te begeven",
            "Toen heb ik haar een beurt gegeven",
            "Ze heeft, na te zijn opgeknapt,",
            "Snel met een ander aangepapt",
            "-----",
            "Het is, geloof ik, een gokmachine",
            "Die, naar men zegt, veel zou verdienen",
            "Ik heb haar uit de goot gehaald",
            "Maar wordt met ondank terugbetaald!\"",
            "-----",
            "\"Drink nog een glaasje\", zei de waard",
            "Die meid is jouw verdriet niet waard,",
            "'t is beter dat gehuil te staken",
            "Voordat je vastgeroest gaat raken\"",
            "-----",
            "De robot zei: \"Ik zal haar mailen:",
            "\"Je kan me echt geen moer meer schelen!\",",
            "En geef, voor mijn melancholie,",
            "Me maar een dubbele smeerolie\"",
            "-----",
            "Einde.",
            "Er zit hier geen mens. Dus er word ook niet gereageerd. (behalve op hoi)",
            "Deze chatbot wordt mogelijk gemaakt door een irritant maar lief 21 jarig jongetje ;P",
            "Nog een fijne dag en veel succes verder!"
        }*/;

        private string[] intro_messages/* =
        {
            "Ik ben een robot en ik ben hier om jou te vervelen. Ik heb wel een mooi verhaaltje voor je... Hou je het vol tot 't einde? Opmerkingen? Vragen? mail: het-gekke-robotje@0dl.nl / whatsapp: 06 86 366 906 / www.facebook.com/rt9x9",
            ""
        }*/;

        Dictionary<TabPage, ChatBot> tp2cb;

        private string[] info_messages/* =
        {
            "En wat zoek je hier? alleen chat? of meer?",
            "Wat is je leeftijd?",
            "In welke stad woon je?",
            "Je bent een meisje?",
            "Ik zoek meer dan alleen een chat. Iemand om mee te ontmoeten.",
            "Dat je 't ff weet.. ik doe niet aan 'geile chats', ook niet aan naaktfoto's uitwisselen en ook niet aan sexcammen."
        }*/;

        public Form1()
        {
            forbidden_index = System.IO.File.ReadAllLines("berichten/forbidden_sentences.txt");
            forbidden_starts_with = System.IO.File.ReadAllLines("berichten/forbidden_start.txt");
            story_mode = System.IO.File.ReadAllLines("berichten/story_mode.txt");
            intro_messages = System.IO.File.ReadAllLines("berichten/introductie.txt");
            info_messages = System.IO.File.ReadAllLines("berichten/info.txt");

            ServicePointManager.DefaultConnectionLimit = 64;
           
            InitializeComponent();

            tp2cb = new Dictionary<TabPage, ChatBot>();

            bm = new BotManager();

            bm.OnChatBotAdded += Bot_OnChatBotAdded;
            bm.OnBeginChat += Bot_OnBeginChat;
            bm.OnBeginSearching += Bot_OnBeginSearching;
            bm.OnNewMessage += Bot_OnNewMessage;
            bm.OnEndChat += Bot_OnEndChat;

            bm.OnBotManagerBeforeUpdate += Bm_OnBotManagerBeforeUpdate;
            bm.OnBotManagerAfterUpdate += Bm_OnBotManagerAfterUpdate;

            bm.OnChatBotBrowserGotFocus += Bm_OnChatBotBrowserGotFocus;

            this.FormClosed += Form1_FormClosed;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Bm_OnChatBotBrowserGotFocus(BotManager manager, ChatBot chatbot)
        {
            if (!chatbot.IsIgnored())
            {
                chatbot.LinkedTabPage.BackColor = Color.Black;
                chatbot.TabText = "";
            }
        }

        private void Bm_OnBotManagerBeforeUpdate(BotManager manager)
        {
        }

        private void Bm_OnBotManagerAfterUpdate(BotManager manager)
        {
        }

        private void Bot_OnChatBotAdded(BotManager manager, ChatBot chatbot)
        {
            TabPage tabPage = new TabPage(chatbot.Identifier.ToString());
            chatbot.LinkedTabPage = tabPage;

            chatbot.GetControl().Dock = DockStyle.Fill;
            tabPage.Controls.Add(chatbot.GetControl());

            tp2cb.Add(tabPage, chatbot);
            browsers.TabPages.Add(tabPage);
        }

        private void Bot_OnEndChat(BotManager manager, ChatBot chatbot)
        {
            chatbot.TabText = "";
            chatbot.LinkedTabPage.BackColor = Color.Gray;
        }

        private void Bot_OnNewMessage(BotManager manager, ChatBot chatbot, Message message, int messageCount, int messageCountNotMine)
        {
            if(!message.Mine)
            {
                chatbot.LinkedTabPage.BackColor = Color.Red;
                chatbot.TabText = "";
                if(messageCountNotMine == 1)
                {
                    foreach (string intro_message in intro_messages)
                    {
                        chatbot.SendMessage(intro_message);
                    }
                }
            }

            if(messageCountNotMine < 3 && !message.Mine)
            {
                bool hasReason = false;
                if (message.Text.Length > 80)
                {
                    hasReason = true;
                }
                else if(message.Text.Length == 1 && (message.Text[0] == 'j' || message.Text[0] == 'J'))
                {
                    hasReason = true;
                }
                else
                {
                    string mtcopy = string.Copy(message.Text);
                    for (char c = '0'; c < '9'; ++c)
                    {
                        mtcopy = mtcopy.Replace(c, '@');
                    }
                    foreach (string plz_no in forbidden_index)
                    {
                        if (mtcopy.IndexOf(plz_no, StringComparison.OrdinalIgnoreCase) != -1)
                        {
                            hasReason = true;
                            break;
                        }
                    }

                    if (!hasReason)
                    {
                        foreach (string plz_no in forbidden_starts_with)
                        {
                            if (mtcopy.StartsWith(plz_no, StringComparison.OrdinalIgnoreCase))
                            {
                                hasReason = true;
                                break;
                            }
                        }
                    }
                }

                if (hasReason)
                {
                    chatbot.LinkedTabPage.BackColor = Color.Blue;
                    chatbot.IgnoreUntilEnd();
                    chatbot.TabText = "";
                }
                else
                {
                    if (
                        message.Text.Length < 7 &&
                        (message.Text.StartsWith("hoi", StringComparison.OrdinalIgnoreCase) ||
                        message.Text.StartsWith("hey", StringComparison.OrdinalIgnoreCase) ||
                        message.Text.StartsWith("hee", StringComparison.OrdinalIgnoreCase) ||
                        message.Text.StartsWith("heu", StringComparison.OrdinalIgnoreCase) ||
                        message.Text.StartsWith("hi", StringComparison.OrdinalIgnoreCase) ||
                        message.Text.StartsWith("hallo", StringComparison.OrdinalIgnoreCase))
                        )
                    {
                        chatbot.SendMessage("Hoi");
                    }
                }
            }
        }

        private void Bot_OnBeginSearching(BotManager manager, ChatBot chatbot)
        {
            chatbot.LinkedTabPage.BackColor = Color.Green;
            chatbot.TabText = "";
        }

        private void Bot_OnBeginChat(BotManager manager, ChatBot chatbot)
        {
            chatbot.LinkedTabPage.BackColor = Color.Black;
            chatbot.TabText = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            //this.MinimumSize = this.Size;
            //this.MaximumSize = this.Size;
        }

        private void btn_AddBot_Click(object sender, EventArgs e)
        {
            bm.AddBot();
        }

        private void browsers_DrawItem(object sender, DrawItemEventArgs e)
        {
            // This event is called once for each tab button in your tab control

            // First paint the background with a color based on the current tab

            // e.Index is the index of the tab in the TabPages collection.
            e.Graphics.FillRectangle(new SolidBrush(browsers.TabPages[e.Index].BackColor), e.Bounds);

            // Then draw the current tab button text 
            Rectangle paddedBounds = e.Bounds;
            paddedBounds.Inflate(-2, -2);
            e.Graphics.DrawString(browsers.TabPages[e.Index].Text, this.Font, SystemBrushes.HighlightText, paddedBounds);

        }

        private void browsers_TabIndexChanged(object sender, EventArgs e)
        {

        }

        public ChatBot GetCurrent()
        {
            if(tp2cb.ContainsKey(browsers.SelectedTab))
            {
                return tp2cb[browsers.SelectedTab];
            }
            return null;
        }

        public void SendCurrentMessage(string message)
        {
            ChatBot bot = GetCurrent();
            if(bot != null)
            {
                bot.SendMessage(message);
            }
        }

        private void btn_wzj_Click(object sender, EventArgs e)
        {
            SendCurrentMessage(info_messages[0]);
        }

        private void btn_lftd_Click(object sender, EventArgs e)
        {
            SendCurrentMessage(info_messages[1]);
        }

        private void btn_wnplts_Click(object sender, EventArgs e)
        {
            SendCurrentMessage(info_messages[2]);
        }

        private void btn_mq_Click(object sender, EventArgs e)
        {
            SendCurrentMessage(info_messages[3]);
        }

        private void btn_ikzk_Click(object sender, EventArgs e)
        {
            SendCurrentMessage(info_messages[4]);
        }

        private void btn_state_Click(object sender, EventArgs e)
        {
            if(btn_state.Text == "Pause")
            {
                bm.Pause();
                btn_state.Text = "Start";
            }
            else//== Start
            {
                bm.Resume();
                btn_state.Text = "Pause";
            }
        }

        private void btn_ignore_Click(object sender, EventArgs e)
        {
            ChatBot bot = GetCurrent();
            if (bot != null)
            {
                bot.LinkedTabPage.BackColor = Color.Blue;
                bot.IgnoreUntilEnd();
                bot.TabText = "";
            }
        }

        private void btn_rst_Click(object sender, EventArgs e)
        {
            bm.Reset();
        }

        private void btn_endcht_Click(object sender, EventArgs e)
        {
            ChatBot bot = GetCurrent();
            if (bot != null)
            {
                bot.EndCurrentChat();
            }
        }

        private void btn_stpstr_Click(object sender, EventArgs e)
        {
            ChatBot bot = GetCurrent();
            if (bot != null)
            {
                bot.StopStory();
            }
        }

        private void btn_zdjw_Click(object sender, EventArgs e)
        {
            SendCurrentMessage(info_messages[5]);
        }

        private void btn_kpon_Click(object sender, EventArgs e)
        {
            ChatBot bot = GetCurrent();
            if (bot != null)
            {
                bot.IgnoreMessageDates = true;
            }
        }
    }
}
