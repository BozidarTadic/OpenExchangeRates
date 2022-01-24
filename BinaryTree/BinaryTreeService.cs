using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree
{
    public class BinaryTreeService
    {
        public List<char> BreadthFirstValues(Node root)
        {
            if (root == null)
            {
                return null;
            }
            List<char> values = new List<char>();
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(root);
            
            while (queue.Count > 0)
            {
                Node current = queue.Dequeue();
                values.Add(current.val);
                
                if(current.left != null) { queue.Enqueue(current.left); }
                if(current.right != null) { queue.Enqueue(current.right); }
            }




            
            return values;
        }
    }
}
