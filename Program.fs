(* Conciseness
   ================================================================================================

   F# is not cluttered up with coding “noise” such as curly brackets, semicolons and so on.

   You almost never have to specify the type of an object, thanks to a powerful type inference system. And, compared with C#, it generally takes fewer lines of code to solve the same problem. *)

// one-liners
[1..100] |> List.sum |> printfn "sum=%d"

// no curly braces, semicolons or parentheses
let square x = x * x
let sq = square 42

// simple types in one line
type Person1 = {First:string; Last:string}

// complex types in a few lines
type Employee1 =
  | Worker of Person1
  | Manager of Employee1 list

// type inference
let jdoe = {First="John"; Last="Doe"}
let worker = Worker jdoe

(* Convenience
   ================================================================================================

   Many common programming tasks are much simpler in F#. 
    
   This includes things like creating and using complex type definitions, doing list processing, 
   comparison and equality, state machines, and much more.

   And because functions are first class objects, it is very easy to create powerful and reusable code
   by creating functions that have other functions as parameters, or that combine existing functions
   to create new functionality. *)

// automatic equality and comparison
type Person = {First:string; Last:string}
let person1 = {First="john"; Last="Doe"}
let person2 = {First="john"; Last="Doe"}
printfn "Equal? %A"  (person1 = person2)

// easy IDisposable logic with "use" keyword
use reader = new StreamReader(..)

// easy composition of functions
let add2times3 = (+) 2 >> (*) 3
let result = add2times3 5

(*  Correctness
    ================================================================================================

    F# has a powerful type system which prevents many common errors such as null reference exceptions.

    Values are immutable by default, which prevents a large class of errors.

    In addition, you can often encode business logic using the type system itself in such a way that it is
    actually impossible to write incorrect code or mix up units of measure, greatly reducing the need for unit tests. *)

// strict type checking
printfn "print string %s" 123 //compile error

// all values immutable by default
person1.First <- "new name"  //assignment error

// never have to check for nulls
let makeNewString str =
   //str can always be appended to safely
   let newString = str + " new!"
   newString

// embed business logic into types
emptyShoppingCart.remove   // compile error!

// units of measure
let distance = 10<m> + 10<ft> // error!

(*  Concurrency
    ================================================================================================

    F# has a number of built-in libraries to help when more than one thing at a time is happening.
    Asynchronous programming is very easy, as is parallelism. F# also has a built-in actor model,
    and excellent support for event handling and functional reactive programming.

    And of course, because data structures are immutable by default, sharing state and avoiding locks is much easier. *)

// easy async logic with "async" keyword
let! result = async {something}

// easy parallelism
Async.Parallel [ for i in 0..40 ->
    async {
        return fib(i) 
    }
]

// message queues
MailboxProcessor.Start(fun inbox-> async{
    let! msg = inbox.Receive()
    printfn "message is: %s" msg
})

(*  Completeness
    ================================================================================================

    Although it is a functional language at heart, F# does support other styles which are not 100% pure, which makes it much easier to interact with the non-pure world of web sites, databases, other applications, and so on. In particular, F# is designed as a hybrid functional/OO language, so it can do virtually everything that C# can do.
    Of course, F# is part of the .NET ecosystem, which gives you seamless access to all the third party .NET libraries and tools. It runs on most platforms, including Linux and smart phones (via Mono).

    Finally, it is well integrated with Visual Studio, which means you get a great IDE with IntelliSense support, a debugger, and many plug-ins for unit tests, source control, and other development tasks. Or on Linux, you can use the MonoDevelop IDE instead.
*)

// impure code when needed
let mutable counter = 0

// create C# compatible classes and interfaces
type IEnumerator<'a> =
    abstract member Current : 'a
    abstract MoveNext : unit -> bool

// extension methods
type System.Int32 with
    member this.IsEven = this % 2 = 0

let i=20
if i.IsEven then printfn "'%i' is even" i

// UI code
open System.Windows.Forms
let form = new Form(Width = 400, Height = 300,
   Visible = true, Text = "Hello World")

form.TopMost <- true
form.Click.Add (fun args -> printfn "clicked!")
form.Show()
