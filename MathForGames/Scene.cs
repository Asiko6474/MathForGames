using System;
using System.Collections.Generic;
using System.Text;

namespace MathForGames
{
    class Scene
    {
        /// <summary>
        /// Array that contains all actors in the scene
        /// </summary>
        private Actor[] _actors;
        private Actor[] _UIElements;

        public Scene()
        {
            _actors = new Actor[0];
            _UIElements = new Actor[0];
        }

        /// <summary>
        /// Calls start for all actors in the actors array
        /// </summary>
        public virtual void Start()
        {
            for (int i = 0; i < _actors.Length; i++)
                _actors[i].Start();
        }

        /// <summary>
        /// Calls update for every actor in the scene. Calls start for the actor if
        /// it hasn't already been called.
        /// </summary>
        public virtual void Update(float deltaTime)
        {
            for (int i = 0; i < _actors.Length; i++)
            {
                if (!_actors[i].Started)
                    _actors[i].Start();

                _actors[i].Update(deltaTime);

                for (int j = 0; j < _actors.Length; j++)
                {
                    if (_actors[i].CheckForCollision(_actors[j]) && j != i)
                        _actors[i].OnCollision(_actors[j]);
                }
            }
        }
        /// <summary>
        /// Calls draw for every actor in the array
        /// </summary>
        public virtual void Draw()
        {
            for (int i = 0; i < _actors.Length; i++)
            {
                _actors[i].Draw();
            }
        }
        
        public virtual void DrawUI()
        {
            for (int i = 0; i < _UIElements.Length; i++)
            {
                _UIElements[i].Draw();
            }
        }

        public virtual void End()
        {
            for (int i = 0; i < _actors.Length; i++)
            {
                _actors[i].End();
            }
        }
        /// <summary>
        /// Adds an actor to the scenes list of actors.
        /// </summary>
        /// <param name="actor">The actor to add to the scene.</param>
        public virtual void AddActor(Actor actor)
        {
            //Creat a temp array larger than the original
            Actor[] tempArray = new Actor[_actors.Length + 1];

            //Copy all values from the original array into the temp array
            for (int i = 0; i < _actors.Length; i++)
            {
                tempArray[i] = _actors[i];
            }
            //Add the new Actor to the end of the new Array
            tempArray[_actors.Length] = actor;

            //set the old array to be the new aray
            _actors = tempArray;
        }


        /// <summary>
        /// Adds an actor to the scenes list of actors.
        /// </summary>
        /// <param name="UI">The actor to add to the scene</param>
        public virtual void AddUIElement(Actor UI)
        {
            //create a temp array larger than the original
            Actor[] tempArray = new Actor[_UIElements.Length + 1];

            //cop all values from the original array into the temp array
            for (int i = 0; i < _actors.Length; i++)
            {
                tempArray[i] = _UIElements[i];
            }

            //add the new actor to the end of the new array
            tempArray[_UIElements.Length] = UI;

            //set the old array to be the new array
            _UIElements = tempArray;
        }


        /// <summary>
        /// Removes the actor from the scene
        /// </summary>
        /// <param name="actor">The actor to remove</param>
        /// <returns>False if the actor was not in the scene array</returns>
        public virtual bool RemoveActor(Actor actor)
        {
            //create a variable to store if the removal was successful
            bool actorRemoved = false;
            
            //Create a new array that is smaller than the original
            Actor[] tempArray = new Actor[_actors.Length - 1];
            //Copy all values except the actor we don't want into the new array
            int j = 0;
          for (int i = 0; i < tempArray.Length; i++)
            {
                //if the actor taht the loop is on is not the one to remove...
                if (_actors[i] != actor)
                {
                    //...add the actor into the new array and increment the temp array counter
                    tempArray[j] = _actors[j];
                    j++;
                }
                //otherwise if this actor is the one to remove...
                else
                {
                    //...set actorRemoved to be true
                    actorRemoved = true;
                }
            }
          //if the actor removal was successful...
          if (actorRemoved)
                //...set the old array to be the new array
                _actors = tempArray;

          return actorRemoved;
            
            
        }


    }
}
