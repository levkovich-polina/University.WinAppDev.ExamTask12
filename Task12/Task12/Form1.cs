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
        Random _random = new Random();
        int _positionX;
        int _positionY;
        int _positionBasketX;
        int _positionBasketY;
        int _countBlue = 0;
        int _countGreen = 0;
        int _countRed = 0;
        Basket _basket;
        private const int _gameFieldSize = 30;
        public Form1()
        {
            InitializeComponent();
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            Panel.CreateGraphics().Clear(Color.White);
            _squares.Clear();
            int width = Panel.ClientSize.Width / _gameFieldSize;
            int height = Panel.ClientSize.Height / _gameFieldSize;
            _positionBasketX = 13;
            _positionBasketY = 30;

            _basket = new Basket(_positionBasketX, _positionBasketY, 3, 1, Color.Purple);

            TimerCallback tm = new TimerCallback(OnTimerTicked);
            _timer = new Timer(tm, 0, 0, 500);
        }

        private void OnTimerTicked(object? state)
        {
            _listPositionX.Clear();
            for (int j = 0; j < _gameFieldSize; j++)
            {
                int chance = _random.Next(1, 11);
                if (chance <= 1)
                {
                    _listPositionX.Add(j);
                }
            }
            _positionY = 0;
            if (_squares.Count > 1)
            {
                for (int i = 0; i < _squares.Count; i++)
                {
                    if (_basket.PositionY == _squares[i].PositionY && _basket.PositionX <= _squares[i].PositionX && _squares[i].PositionX <= (_basket.PositionX + _basket.Width - 1))
                    {
                        _squares.RemoveAt(i);

                        if (_squares[i].Color == Color.Blue)
                        {
                            _countBlue++;
                            Invoke(() =>
                            {
                                BlueTextBox.Text = Convert.ToString(_countBlue);
                            });
                        }
                        else if (_squares[i].Color == Color.Red)
                        {
                            _countRed++;
                            Invoke(() =>
                            {
                                RedTextBox.Text = Convert.ToString(_countRed);
                            });

                        }
                        else if (_squares[i].Color == Color.Green)
                        {
                            _countGreen++;
                            Invoke(() =>
                            {
                                GreenTextBox.Text = Convert.ToString(_countGreen);
                            });

                        }
                    }
                }
            }
            if (_squares.Count > 1)
            {
                for (int i = 0; i < _squares.Count; i++)
                {
                    _squares[i].PositionY++;
                }
            }

            for (int i = 0; i < _listPositionX.Count; i++)
            {
                _positionX = _listPositionX[i];

                int randomColor = _random.Next(0, 3);
                if (randomColor == 0)
                {
                    Square square = new Square(_positionX, _positionY, 1, 1, Color.Red);
                    _squares.Add(square);
                }
                else if (randomColor == 1)
                {
                    Square square = new Square(_positionX, _positionY, 1, 1, Color.Blue);
                    _squares.Add(square);
                }
                else if (randomColor == 2)
                {
                    Square square = new Square(_positionX, _positionY, 1, 1, Color.Green);
                    _squares.Add(square);
                }
            }

            Draw();

        }

        public void Draw()
        {
            Graphics g = Panel.CreateGraphics();
            g.Clear(Color.White);
            int height = Panel.ClientSize.Height / _gameFieldSize;
            int width = Panel.ClientSize.Width / _gameFieldSize;

            for (int i = 0; i < _squares.Count; i++)
            {
                var dx = _squares[i].PositionX * width;
                var dy = _squares[i].PositionY * height;
                var dWidth = _squares[i].Width * width;
                var dHeight = _squares[i].Height * height;
                var brush = _squares[i].Color;
                Invoke(() =>
                {
                    g.FillRectangle(new SolidBrush(brush), dx, dy, dWidth, dHeight);
                });
            }
            g.FillRectangle(new SolidBrush(_basket.Color), _basket.PositionX * width, _basket.PositionY * height, _basket.Width * width, _basket.Height * height);

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                if (_basket.PositionX > 0)
                {
                    _basket.PositionX--;
                }
            }
            if (e.KeyCode == Keys.D)
            {
                if (_basket.PositionX < (_gameFieldSize - _basket.Width))
                {
                    _basket.PositionX++;
                }
            }
        }
    }
}