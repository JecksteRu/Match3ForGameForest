using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Match3Game.Node
{
    public interface INode : IDisposable
    {
        string Name { get; set; }

        event Action<string, string> ChangedName;
        event Action<string> OnDelete;

        void AddChild(Node newChild);
        void Draw(GameTime gameTime);
        List<Node> GetChildList();
        IEnumerator GetEnumerator();
        Point GetGlobalPosition();
        Node GetNode(string name);
        T GetNode<T>(string name) where T : Node;
        Node GetParent();
        Point GetPosition();
        bool HasNode(string name);
        void Initialize();
        void LoadContent();
        void RemoveChild(string childName);
        void SetGlobalPosition(int x, int y);
        void SetGlobalPosition(Point newLocation);
        void SetPosition(int x, int y);
        void SetPosition(Point point);
        void SetSize(int x, int y);
        void SetSize(Point size);
        void UnloadContent();
        void Update(GameTime gameTime);
    }
}