﻿using System;

using System.Drawing;
using System.IO;
using System.Windows.Forms;
using cat_and_mouse.Domain;

namespace cat_and_mouse
{
    public sealed partial class TypeOfGameForm : Form
    {
        private Timer timer = new();
        public const int ElementSize = 40;//???
        public static readonly string MainPath = new DirectoryInfo
            (Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.ToString();

        public Cat CatPlayer;
        public Mouse MousePlayer;
        private bool isFirstLeft = true;
        private bool isFirstRight = true;
        private bool realFirst = true;
        public readonly Image Cat = Image.FromFile(MainPath + @"\Pictures\cat.png");
        public readonly Image Mouse = Image.FromFile(MainPath + @"\Pictures\mouse.png");
        private readonly Image cheese = Image.FromFile(MainPath + @"\Pictures\cheese.png");
        public TypeOfGameForm()
        {
            InitializeComponent();
            DoubleBuffered = true;
            timer.Interval = 20;
            timer.Tick += Update;
            LoadLevels();
            MaximizeBox = false;
            ClientSize = new Size((Map.MapWidth) * ElementSize, (Map.MapHeight) * ElementSize);
            Initialize();
            KeyDown += OnPress;
        }
        
        public void OnPress(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    CatPlayer.Move(0, -1, CatPlayer);
                    CatPlayer.StateCheck(CatPlayer, MousePlayer);
                    break;
                case Keys.S:
                    CatPlayer.Move(0, 1, CatPlayer);
                    CatPlayer.StateCheck(CatPlayer, MousePlayer);
                    break;
                case Keys.A:
                    CatPlayer.Move(-1, 0, CatPlayer);
                    CatPlayer.StateCheck(CatPlayer, MousePlayer);
                    isFirstRight = true;
                    realFirst = false;
                    if (isFirstLeft && !realFirst)
                    {
                        Cat.RotateFlip(RotateFlipType.Rotate180FlipY);
                        isFirstLeft = false;
                    }
                    break;
                case Keys.D:
                    CatPlayer.Move(1, 0, CatPlayer);
                    CatPlayer.StateCheck(CatPlayer, MousePlayer);
                    isFirstLeft = true;
                    if (isFirstRight && !realFirst)
                    {
                        Cat.RotateFlip(RotateFlipType.Rotate180FlipY);
                        isFirstRight = false;
                        realFirst = false;
                    }
                    break;
                case Keys.Up:
                    MousePlayer.Move(0, -1, MousePlayer);
                    MousePlayer.StateCheck(MousePlayer);
                    CatPlayer.StateCheck(CatPlayer, MousePlayer);
                    break;
                case Keys.Down:
                    MousePlayer.Move(0, 1, MousePlayer);
                    MousePlayer.StateCheck(MousePlayer);
                    CatPlayer.StateCheck(CatPlayer, MousePlayer);
                    break;
                case Keys.Left:
                    MousePlayer.Move(-1, 0, MousePlayer);
                    MousePlayer.StateCheck(MousePlayer);
                    CatPlayer.StateCheck(CatPlayer, MousePlayer);
                    isFirstRight = true;
                    realFirst = false;
                    if (isFirstLeft && !realFirst)
                    {
                        Mouse.RotateFlip(RotateFlipType.Rotate180FlipY);
                        isFirstLeft = false;
                    }
                    realFirst = false;
                    break;
                case Keys.Right:
                    MousePlayer.Move(1, 0, MousePlayer);
                    MousePlayer.StateCheck(MousePlayer);
                    CatPlayer.StateCheck(CatPlayer, MousePlayer);
                    isFirstLeft = true;
                    if (isFirstRight && !realFirst)
                    {
                        Mouse.RotateFlip(RotateFlipType.Rotate180FlipY);
                        isFirstRight = false;
                        realFirst = false;
                    }
                    break;
            }
        }
        private static void LoadLevels()
        {
            var text = new StreamReader(MainPath + @"\Levels\Level1.txt").ReadToEnd();
            var lines = text.Split(new[] {"\r", "\n"}, StringSplitOptions.RemoveEmptyEntries);
            Map.FromLines(lines);
        }

        public void Initialize()
        {
            CatPlayer = new Cat(Map.CatPosition.X, Map.CatPosition.Y);
            MousePlayer = new Mouse(Map.MousePosition.X, Map.MousePosition.Y);
            
            timer.Start();
        }

        public void Update(object sender, EventArgs e)
        {
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Map.DrawMap(e.Graphics);
            e.Graphics.DrawImage(cheese,Map.CheesePosition.X * ElementSize,Map.CheesePosition.Y * ElementSize);
            e.Graphics.DrawImage(Cat, CatPlayer.Position.X * ElementSize, CatPlayer.Position.Y * ElementSize);
            e.Graphics.DrawImage(Mouse,MousePlayer.Position.X * ElementSize,MousePlayer.Position.Y * ElementSize);

        }
    }
}