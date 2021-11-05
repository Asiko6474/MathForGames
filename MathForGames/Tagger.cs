using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    
    //class Tagger : Actor
    //{
    //    private Actor _target;
    //    private float _speed;
    //    private Vector2 _velocity;
    //    public float _maxViewAngle;
    //    public float _maxSightDistance;
    //    public UIText Speech;
      

    //    public float Speed
    //    {
    //        get { return _speed; }
    //        set { _speed = value; }
    //    }

    //    public Vector2 Velocity
    //    {
    //        get { return _velocity; }
    //        set { _velocity = value; }
    //    }
        
    //    public Tagger( float x, float y, float speed, float maxViewAngle, float maxSightDistance, Actor target, string name = "Tagger", string path = "")
    //        : base( x, y, name, path)
    //    {
    //        _target = target;
    //        _speed = speed;
    //        _maxSightDistance = maxSightDistance;
    //        _maxViewAngle = maxViewAngle;
    //    }

    //    public override void Update(float deltaTime)
    //    {
    //        Speech.Text = "Enemy";
    //        Speech.LocalPosition = LocalPosition + new Vector2(25, -25);

    //        //create a vector that stores the move input
    //        Vector2 moveDirection = ( _target.LocalPosition - LocalPosition).Normalized;

    //        Velocity = moveDirection * Speed * deltaTime;

    //        if (GetTargetInsight())
    //            LocalPosition += Velocity;

    //        base.Update(deltaTime);
    //    }

    //    public bool GetTargetInsight()
    //    {
    //        Vector3 directionOfTarget = ( _target.LocalPosition - LocalPosition).Normalized;
    //        float distanceToTarget = Vector3.Distance(_target.LocalPosition, LocalPosition);

    //        float dotProduct = Vector3.DotProduct(directionOfTarget, Forward);

    //        return Math.Acos(dotProduct) < _maxViewAngle && distanceToTarget < _maxSightDistance;
    //    }



    //    public virtual void OnCollision(Actor actor)
    //    {

    //    }
    //    public override void Draw()
    //    {
    //        base.Draw();
    //        if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT_SHIFT))
    //            Collider.Draw();
    //    }
    //}
}
