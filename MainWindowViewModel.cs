using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAR
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Observable Properties
        private bool _isPlaying;
        public bool IsPlaying
        {
            get { return this._isPlaying; }
            set
            {
                if(value != this._isPlaying)
                {
                    this._isPlaying = value;
                    OnPropertyChanged("IsPlaying");
                }
            }
        }

        private string _imageCardYOU;
        public string ImageCardYOU
        {
            get { return this._imageCardYOU; }
            set
            {
                if(value != this._imageCardYOU)
                {
                    this._imageCardYOU = value;
                    OnPropertyChanged("ImageCardYOU");
                }
            }
        }

        private string _imageCardCOMPUTER;
        public string ImageCardCOMPUTER
        {
            get { return this._imageCardCOMPUTER; }
            set
            {
                if (value != this._imageCardCOMPUTER)
                {
                    this._imageCardCOMPUTER = value;
                    OnPropertyChanged("ImageCardCOMPUTER");
                }
            }
        }

        private string _gameMessage;
        public string GameMessage
        {
            get { return this._gameMessage; }
            set
            {
                if (value != this._gameMessage)
                {
                    this._gameMessage = value;
                    OnPropertyChanged("GameMessage");
                }
            }
        }

        private int _scoreYOU;
        public int ScoreYOU
        {
            get { return this._scoreYOU; }
            set
            {
                if(value != this._scoreYOU)
                {
                    this._scoreYOU = value;
                    OnPropertyChanged("ScoreYOU");
                }
            }
        }

        private int _scoreCOMPUTER;
        public int ScoreCOMPUTER
        {
            get { return this._scoreCOMPUTER; }
            set
            {
                if (value != this._scoreCOMPUTER)
                {
                    this._scoreCOMPUTER = value;
                    OnPropertyChanged("ScoreCOMPUTER");
                }
            }
        }

        private int _cardsYOU;
        public int CardsYOU
        {
            get { return this._cardsYOU; }
            set
            {
                if (value != this._cardsYOU)
                {
                    this._cardsYOU = value;
                    OnPropertyChanged("CardsYOU");
                }
            }
        }

        private int _cardsCOMPUTER;
        public int CardsCOMPUTER
        {
            get { return this._cardsCOMPUTER; }
            set
            {
                if (value != this._cardsCOMPUTER)
                {
                    this._cardsCOMPUTER = value;
                    OnPropertyChanged("CardsCOMPUTER");
                }
            }
        }
        #endregion

        private Dictionary<int, Tuple<string, int>> deck = null;
        private int[] shuffledDeck = new int[52];
        private List<Tuple<int, string, int>> handYOU = null;
        private List<Tuple<int, string, int>> handCOMPUTER = null;

        public MainWindowViewModel()
        {
            this.handCOMPUTER = new List<Tuple<int, string, int>>();
            this.handYOU = new List<Tuple<int, string, int>>();
            BuildDeck();
            WipeBoard();
            this.IsPlaying = false;
        }

        public string GameRules
        {
            get
            {
                return "Select the New Game button to start a game of War. \n\n" +
                        "A deck of cards will be evenly distributed between you and the computer. \n" +
                        "Click the PLAY button to play a card. \n" +
                        "If your card is higher than the computer's card, you win that round and gain a point. \n" +
                        "If the computer's card is higher than your card, the computer wins that round and it gains a point. \n\n" +
                        "The one to obtain the hightest points after all 52 cards are played wins the game.";
            }
        }
        public void NewGame()
        {
            this.IsPlaying = true;
            WipeBoard();
            ShuffleDeck();
            DealDeck();
            this.CardsCOMPUTER = this.handCOMPUTER.Count;
            this.CardsYOU = this.handYOU.Count;
        }
        public void Play()
        {
            var cardYOU = this.handYOU.Last();
            var cardCOMPUTER = this.handCOMPUTER.Last();
            this.handYOU.RemoveAt(this.handYOU.Count - 1);
            this.handCOMPUTER.RemoveAt(this.handCOMPUTER.Count - 1);
            this.CardsYOU = this.handYOU.Count;
            this.CardsCOMPUTER = this.handCOMPUTER.Count;
            this.ImageCardYOU = cardYOU.Item2;
            this.ImageCardCOMPUTER = cardCOMPUTER.Item2;

            if(cardYOU.Item3 == cardCOMPUTER.Item3) //tie
            {
                this.GameMessage = "A tie! No points awarded.";
            }
            else if(cardYOU.Item3 > cardCOMPUTER.Item3) //YOU wins
            {
                this.ScoreYOU++;
                this.GameMessage = "You won this hand! A point has been added to your score.";
            }
            else //COMPUTER wins
            {
                this.ScoreCOMPUTER++;
                this.GameMessage = "The computer won this hand! A point has been added to its score.";
            }

            if (!this.handYOU.Any())
                EndGame();
        }

        private void BuildDeck()
        {
            deck = new Dictionary<int, Tuple<string, int>>()
            {
                {1, new Tuple<string, int>("2_of_clubs.png", 1) }, {2, new Tuple<string, int>("2_of_diamonds.png", 1) }, {3, new Tuple<string, int>("2_of_hearts.png", 1) }, {4, new Tuple<string, int>("2_of_spades.png", 1) },
                { 5, new Tuple<string, int>("3_of_clubs.png", 2) }, {6, new Tuple<string, int>("3_of_diamonds.png", 2) }, {7, new Tuple<string, int>("3_of_hearts.png", 2) }, {8, new Tuple<string, int>("3_of_spades.png", 2) },
                { 9, new Tuple<string, int>("4_of_clubs.png", 3) }, {10, new Tuple<string, int>("4_of_diamonds.png", 3) }, {11, new Tuple<string, int>("4_of_hearts.png", 3) }, {12, new Tuple<string, int>("4_of_spades.png", 3) },
                {13, new Tuple<string, int>("5_of_clubs.png", 4) }, {14, new Tuple<string, int>("5_of_diamonds.png", 4) }, {15, new Tuple<string, int>("5_of_hearts.png", 4) }, {16, new Tuple<string, int>("5_of_spades.png", 4) },
                {17, new Tuple<string, int>("6_of_clubs.png", 5) }, {18, new Tuple<string, int>("6_of_diamonds.png", 5) }, {19, new Tuple<string, int>("6_of_hearts.png", 5) }, {20, new Tuple<string, int>("6_of_spades.png", 5) },
                {21, new Tuple<string, int>("7_of_clubs.png", 6) }, {22, new Tuple<string, int>("7_of_diamonds.png", 6) }, {23, new Tuple<string, int>("7_of_hearts.png", 6) }, {24, new Tuple<string, int>("7_of_spades.png", 6) },
                {25, new Tuple<string, int>("8_of_clubs.png", 7) }, {26, new Tuple<string, int>("8_of_diamonds.png", 7) }, {27, new Tuple<string, int>("8_of_hearts.png", 7) }, {28, new Tuple<string, int>("8_of_spades.png", 7) },
                {29, new Tuple<string, int>("9_of_clubs.png", 8) }, {30, new Tuple<string, int>("9_of_diamonds.png", 8) }, {31, new Tuple<string, int>("9_of_hearts.png", 8) }, {32, new Tuple<string, int>("9_of_spades.png", 8) },
                {33, new Tuple<string, int>("10_of_clubs.png", 9) }, {34, new Tuple<string, int>("10_of_diamonds.png", 9) }, {35, new Tuple<string, int>("10_of_hearts.png", 9) }, {36, new Tuple<string, int>("10_of_spades.png", 9) },
                {37, new Tuple<string, int>("jack_of_clubs2.png", 10) }, {38, new Tuple<string, int>("jack_of_diamonds2.png", 10) }, {39, new Tuple<string, int>("jack_of_hearts2.png", 10) }, {40, new Tuple<string, int>("jack_of_spades2.png", 10) },
                {41, new Tuple<string, int>("queen_of_clubs2.png", 11) }, {42, new Tuple<string, int>("queen_of_diamonds2.png", 11) }, {43, new Tuple<string, int>("queen_of_hearts2.png", 11) }, {44, new Tuple<string, int>("queen_of_spades2.png", 11) },
                {45, new Tuple<string, int>("king_of_clubs2.png", 12) }, {46, new Tuple<string, int>("king_of_diamonds2.png", 12) }, {47, new Tuple<string, int>("king_of_hearts2.png", 12) }, {48, new Tuple<string, int>("king_of_spades2.png", 12) },
                {49, new Tuple<string, int>("ace_of_clubs.png", 13) }, {50, new Tuple<string, int>("ace_of_diamonds.png", 13) }, {51, new Tuple<string, int>("ace_of_hearts.png", 13) }, {52, new Tuple<string, int>("ace_of_spades2.png", 13) }
            };
    }
        private void WipeBoard()
        {
            this.ScoreCOMPUTER = 0;
            this.ScoreYOU = 0;
            this.CardsCOMPUTER = 0;
            this.CardsYOU = 0;
            this.ImageCardCOMPUTER = null;
            this.ImageCardYOU = null;
            this.GameMessage = null;
        }
        private void ShuffleDeck()
        {
            Random r = new Random();
            int[] t = new int[52];
            for (int i = 0; i < 52; i++)
                t[i] = i;

            for (int i = 51; i > 0; i--)
            {
                int k = r.Next(i + 1);
                int temp = t[i];
                t[i] = t[k];
                t[k] = temp;
            }
            this.shuffledDeck = t;
        }
        private void DealDeck()
        {
            this.handCOMPUTER.Clear();
            this.handYOU.Clear();

            for (int i = 0; i < 52; i++)
            {
                int cardValue = this.shuffledDeck[i];
                Tuple<string, int> card = this.deck[cardValue + 1];
                string cardImage = @"Images/" + card.Item1;
                int cardScore = card.Item2;
                if(i % 2 == 0)
                    this.handYOU.Add(new Tuple<int, string, int>(cardValue, cardImage, cardScore));
                else
                    this.handCOMPUTER.Add(new Tuple<int, string, int>(cardValue, cardImage, cardScore));
            }
        }
        private void EndGame()
        {
            this.IsPlaying = false;
            if (this.ScoreYOU > this.ScoreCOMPUTER)
                this.GameMessage = "Game Over: YOU WIN!!";
            else if(this.ScoreYOU == this.ScoreCOMPUTER)
                this.GameMessage = "Game Over: THERE WAS A TIE!!";
            else
                this.GameMessage = "Game Over: COMPUTER WINs!!";
        }
    }
}
