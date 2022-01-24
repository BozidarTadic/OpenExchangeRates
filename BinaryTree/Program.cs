// See https://aka.ms/new-console-template for more information
using BinaryTree;

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

Console.WriteLine("Hello, World!");
BinaryTreeService service = new BinaryTreeService();

var reslut  = service.BreadthFirstValues(a);

foreach(var val in reslut)
{
    Console.WriteLine(val);
}
