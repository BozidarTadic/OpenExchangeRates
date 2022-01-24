using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree
{
    public class Node
    {
        public Node(char val)
        {
            this.val = val;
        }
        public char val { get; set; }
        public Node right { get; set; }
        public Node left { get; set; }


        public List<char> BreadthFirstValues(Node root)
        {
            if (root == null)
            {
                return null;
            }
            List<char> values = new List<char>();
            Queue<Node> queue = new Queue<Node>();

            while (queue.Count > 0)
            {
                Node current = queue.Dequeue();
                values.Add(current.val);

                if (current.left != null) { queue.Enqueue(current.left); }
                if (current.right != null) { queue.Enqueue(current.right); }
            }





            return values;
        }


    }
}
