using System;
using System.Collections;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Friendly_Wars.Engine.Component;
using Friendly_Wars.Engine.Component.Graphic;
using System.Collections.Generic;

namespace Friendly_Wars.Engine.Object
{

    public class GameObject
    {
        private TransformComponent transformComponent;
        private PhysicsComponent physicsComponent;

        private AudioComponent audioComponent;
        private RenderComponent renderComponent;

        private IList<GameObject> children;
        private String name;
        private String tag;
        private String UID;

        public GameObject(String name, String tag = null)
        {
            this.name = name;
            this.tag = tag;

            transformComponent = new TransformComponent(this);
            physicsComponent = new PhysicsComponent(this);
            audioComponent = new AudioComponent(this);
            renderComponent = new RenderComponent(this);

            children = new List<GameObject>();
            UID = NextUID();
        }

        public void AddChild(GameObject child)
        {
            children.Add(child);
        }

        private static String NextUID()
        {
            return null;
        }


    }
}
