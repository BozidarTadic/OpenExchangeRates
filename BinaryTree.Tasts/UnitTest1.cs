using System.Collections.Generic;
using Xunit;

namespace BinaryTree.Tasts
{
    public class UnitTest1
        
    {
        private readonly BinaryTreeService _tree;
        public UnitTest1(BinaryTreeService tree)
        {
            _tree = tree;
        }
        [Fact]
        public void Test1()
        {
            var a = new Node('a');
            var b = new Node('b');
            var c = new Node('c');
            var d = new Node('d');
            var e = new Node('e');
            var f = new Node('f');

            a.left = b;
            a.right = c;
            b.left = d;
            b.right = e;
            c.right = f;

            List<char> list = new List<char>();

            list.Add('a');
            list.Add('b');
            list.Add('c');
            list.Add('d');
            list.Add('e');
            list.Add('f');


            var res =  _tree.BreadthFirstValues(a);
            Assert.True(res == list);
        }
    }
}