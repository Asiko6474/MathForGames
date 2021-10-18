using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    struct Icon
    {
        public  char Symbol;
        public  Color color;
    }

    class Actor
    {
        private Icon _icon;
        private string _name;
        private Vector2 _position;
        private bool _started;


        /// <summary>
        /// True if the start function has been called for this actor.
        /// </summary>
        public bool Started
        {
            get { return _started; }
        }

        public Vector2 Position
        {
            get { return _position;  }
            set { _position = value;  }
        }

        public Actor(char icon, float x, float y, Color color, string name = "Arthurd") :
            this(icon, new Vector2 { x = x, y = y }, color, name) {}

        public Actor(char icon, Vector2 position, Color color, string name = "Arthurd")
        {
            _icon = new Icon { Symbol = icon, color = color };
            _position = position;
            _name = name;
        }

        public virtual void OnCollision(Actor actor)
        {
            Console.WriteLine("Collision occured");
        }

        public virtual void Start()
        {
            _started = true;
        }

        public virtual void Update(float deltaTime)
        {
            Console.WriteLine(_name = "Jake ",Position.x + ", " + Position.y);
        }

        public virtual void Draw()
        {
            Raylib.DrawText(_icon.Symbol.ToString(), (int)Position.x, (int)Position.y, 50, _icon.color);
        }

        public virtual void End()
        {

        }
    }
}
