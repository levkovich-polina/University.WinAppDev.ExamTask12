using System.Drawing;
using Timer = System.Threading.Timer;


namespace Task12
{
    public partial class Form1 : Form
    {
        public class Square
        {
            public int PositionX { get; set; }
            public int PositionY { get; set; }

            public int Width { get; set; }
            public int Height { get; set; }

            public Color Color { get; set; }
            public Square(int x, int y, int width, int height, Color color)
            {
                PositionX = x;
                PositionY = y;
                Width = width;
                Height = height;
                Color = color;
            }
        }
        public class Basket
        {
            public int PositionX { get; set; }
            public int PositionY { get; set; }

            public int Width { get; set; }
            public int Height { get; set; }

            public Color Color { get; set; }
            public Basket(int x, int y, int width, int height, Color color)
            {
                PositionX = x;
                PositionY = y;
                Width = width;
                Height = height;
                Color = color;
            }
        }

        Timer _timer;
        List<Square> _squares = new List<Square>();
        List<int> _listPositionX = new List<int>();
        List<Basket> _listBasket = new List<Basket>();
        Random _random = new Random();
        int _positionX;
        int _positionY;
        int _positionBasketX;
        int _positionBasketY;

        public Form1()
        {
            InitializeComponent();
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            int width = Panel.ClientSize.Width / 31;
            int height = Panel.ClientSize.Height / 31;
            _positionBasketX = width * 13;
            _positionBasketY = height * 30;
            for (int i = 0; i < 3; i++)
            {
                Basket basket = new Basket(_positionBasketX, _positionBasketY, width, height, Color.Purple);
                _listBasket.Add(basket);
                _positionBasketX+=width;
            }
            TimerCallback tm = new TimerCallback(OnTimerTicked);
            _timer = new Timer(tm, 0, 0, 1000);
        }

        private void OnTimerTicked(object? state)
        {
            _listPositionX.Clear();
            for (int j = 0; j < 31; j++)
            {
                int chance = _random.Next(1, 11);
                if (chance <= 1)
                {
                    _listPositionX.Add(j);
                }
            }

            int width = Panel.ClientSize.Width / 31;
            int height = Panel.ClientSize.Height / 31;
            _positionY = 0;
            if (_squares.Count > 1)
            {
                for (int i = 0; i < _squares.Count; i++)
                {
                    _squares[i].PositionY += height;
                }
            }

            for (int i = 0; i < _listPositionX.Count; i++)
            {
                _positionX = width * _listPositionX[i];
                int randomNumberColor = _random.Next(0, 4);
                if (randomNumberColor == 0)
                {
                    Square square = new Square(_positionX, _positionY, width, height, Color.Blue);
                    _squares.Add(square);
                }
                else if (randomNumberColor == 1)
                {
                    Square square = new Square(_positionX, _positionY, width, height, Color.Red);
                    _squares.Add(square);
                }
                else if (randomNumberColor == 2)
                {
                    Square square = new Square(_positionX, _positionY, width, height, Color.Green);
                    _squares.Add(square);
                }
                else
                {
                    Square square = new Square(_positionX, _positionY, width, height, Color.Yellow);
                    _squares.Add(square);
                }
            }
            Draw();
        }

        private void Draw()
        {
            Graphics g = Panel.CreateGraphics();
            int height = Panel.ClientSize.Height / 31;
            int width = Panel.ClientSize.Width / 31;

            for (int i = 0; i < _squares.Count; i++)
            {
                var dx = _squares[i].PositionX;
                var dy = _squares[i].PositionY;
                var dWidth = _squares[i].Width;
                var dHeight = _squares[i].Height;
                var brush = _squares[i].Color;
                Invoke(() =>
                {
                    g.FillRectangle(new SolidBrush(Color.White), dx, dy - height, dWidth, dHeight);

                    g.FillRectangle(new SolidBrush(brush), dx, dy, dWidth, dHeight);
                });
            }
            for (int i = 0; i < _listBasket.Count; i++)
            {
                g.FillRectangle(new SolidBrush(_listBasket[i].Color), _listBasket[i].PositionX, _listBasket[i].PositionY, _listBasket[i].Width, _listBasket[i].Height);
            }
        }
       
    }
}