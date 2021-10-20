using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;

namespace MathForGames
{
    class UIText : Actor
    {
        public string Text;
        public int Width;
        public int Height;
        public int FontSize;
        public Font font;

        public UIText(float x, float y, string name, Color color, int width, int height, int fontsize, string text = "") : base('\0', x, y, color, name)
        {
            Text = text;
            Width = width;
            Height = height;
            font = Raylib.LoadFont("resources/Fonts/Alagard.png");
            FontSize = fontsize;
        }


        public override void Draw()
        {
            //Create a new rectablge that will act as trhe borders of the text box
            Rectangle textbox = new Rectangle(Position.x, Position.y, Width, Height);

            //Draw the textbox
            Raylib.DrawTextRec(font, Text, textbox, FontSize, 1, true, Icon.color);
        }
    }
}
