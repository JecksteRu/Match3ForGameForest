using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Match3Game.Node
{
    public abstract class Node : INode
    {

        public event Action<string> OnDelete;
        public event Action<string, string> ChangedName;


        protected String name;
        public String Name
        {
            get => name;
            set
            {
                if (parentNode == null) { name = value; }
                else if (!(parentNode.HasNode(value)))
                {
                    ChangedName?.Invoke(name, value);
                    name = value;
                }

            }
        }

        public List<Node> GetChildList()
        {
            return childNodes.Values.ToList();
        }


        public Node GetParent()
        {
            return parentNode;
        }

        public Rectangle Rectangle;


        protected Node parentNode;
        protected Dictionary<string, Node> childNodes;

        protected Node(string name)
        {
            Name = name;
            childNodes = new Dictionary<string, Node>();
        }

        public virtual void Initialize()
        {


            foreach (Node node in GetChildList())
            {
                node.Initialize();
            }

        }
        public virtual void LoadContent()
        {

            foreach (Node node in GetChildList())
            {
                node.LoadContent();
            }

        }
        public virtual void Update(GameTime gameTime)
        {
            foreach (Node node in GetChildList())
            {
                node.Update(gameTime);
            }
        }
        public virtual void Draw(GameTime gameTime)
        {
            foreach (Node node in GetChildList())
            {
                node.Draw(gameTime);
            }
        }

        public virtual void UnloadContent()
        {
            OnDelete?.Invoke(Name);
            foreach (Node child in GetChildList())
            {
                child.UnloadContent();
            }
        }

        public virtual void Dispose()
        {
            UnloadContent();
        }
        public IEnumerator GetEnumerator()
        {
            return GetChildList().GetEnumerator();
        }

        public void AddChild(Node newChild)
        {
            string newName = newChild.Name;
            if (HasNode(newName))
            {
                ushort index = 1;

                while (HasNode(newName))
                {
                    newName = newChild.Name + "_" + index;
                    index++;
                }
                newChild.Name = newName;
            }
            //tempAddChildList.Add(newChild);
            childNodes.Add(newChild.Name, newChild);
            newChild.parentNode = this;
            newChild.ChangedName = OnChildChangedName;
            newChild.OnDelete = RemoveChild;
            newChild.Initialize();
            newChild.LoadContent();
        }



        private void OnChildChangedName(string oldName, string newName)
        {

            Node node = GetNode(oldName);
            childNodes.Remove(oldName);
            childNodes.Add(newName, node);
        }

        public Node GetNode(string name)
        {
            childNodes.TryGetValue(name, out Node result);
            if (result is null) { throw new Exception("Can't find child node " + name); }
            return result;
        }

        public T GetNode<T>(string name)
            where T : Node
        {
            childNodes.TryGetValue(name, out Node result);
            if (result is null) { throw new Exception("Can't find node " + name); }
            T node = result as T;
            if (node is null) { throw new Exception("This is wrong type" + name); }
            return node;
        }

        public bool HasNode(string name)
        {
            childNodes.TryGetValue(name, out Node result);
            return (result is not null);
        }

        public void RemoveChild(string childName)
        {
            Node node = GetNode(childName);
            node.ChangedName = null;
            childNodes.Remove(childName);

        }



        public void SetPosition(int x, int y)
        {
            if (parentNode is not null)
            {
                x += parentNode.Rectangle.X;
                y += parentNode.Rectangle.Y;
            }
            Rectangle.Location = new Point(x, y);
        }

        public void SetPosition(Point point)
        {
            if (parentNode is not null)
            {
                point.X += parentNode.Rectangle.X;
                point.Y += parentNode.Rectangle.Y;
            }
            Rectangle.Location = point;
        }

        public Point GetPosition()
        {
            if (parentNode is not null)
            {
                return Rectangle.Location - parentNode.Rectangle.Location;
            }
            return Rectangle.Location;
        }
        public Point GetGlobalPosition()
        {
            return Rectangle.Location;
        }

        public void SetGlobalPosition(Point newLocation)
        {
            Rectangle.Location = newLocation;

        }

        public void SetGlobalPosition(int x, int y)
        {
            Rectangle.Location = new Point(x, y);
        }
        public void SetSize(int x, int y)
        {
            Rectangle.Size = new Point(x, y);
        }

        public void SetSize(Point size)
        {
            Rectangle.Size = size;
        }
    }




}

