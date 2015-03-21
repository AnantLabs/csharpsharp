
---

**Support**

For anyone wondering about the lack of updates, it is because I am no longer a .NET developer. I believe this plugin can be useful in a wide variety of projects so I will not delete it. If you want to maintain it, feel free to contact me (either fork your own project or continue with this one).

---


CSharpSharp provides common functionalities to C# developers that are not present in the standard libraries. CSharpSharp makes complex tasks that should be simple simple.

## Samples ##
### BooleanSharp ###
```
// Parse unconventional strings like "yes" or "no"
// You can even add your own strings
BooleanSharp.TryParse("yes", out isTrue); // isTrue will be true
BooleanSharp.AddAcceptedTrueString("sure"); // "sure" will now parse as true
```

### CollectionComparer ###
Compares to collection to verify if they contain the exact same items
and not more.
```
new CollectionComparer<int>().AreEqual(firstCollection, secondCollection);

// You can even use your own comparison delegate
new CollectionComparer<DummyClass>(
    delegate(DummyClass foo, DummyClass bar)
    {
        return foo.Value == bar.Value;
    }).AreEqual(firstCollection, secondCollection);
```

### DateTimeSharp ###
```
DateTimeSharp.MillisecondsSinceUNIXTime; // Useful for calculating timings along with JavaScript's new Date().getTime();
20.Seconds()
5.Days()
3.Months().Ago()
2.Years().FromNow()
```

### StackSharp ###
A different Stack, the Stack instance always contains the last pushed item. Each node also has a reference to its parent node.
```
StackSharp<string> stack = new StackSharp<string>();
stack.Push("root"); // stack.Item returns "root"
stack.Push("first level"); // stack.Item returns "first level", stack.Root.Item returns "root"
```

### StringSharp ###
StringSharp provides string conversions methods among other things.
```
"my variable name".SplitWords(); // splits on uppercase characters and spaces
"text with multiple lines".SplitLines(); // splits on new lines

"my variable name".ToCamelCase(); // returns "myVariableName"
"my variable name".ToPascalCase(); // returns "MyVariableName"
"my variable name".ToTitleCase(); // returns "My Variable Name"
"MyVariableName".ToPlainEnglish(); // returns "my variable name"

"word".Capitalize(); // returns "Word"

"thing".Pluralize(); // uses the PluralizationServices of System.Data.Entity
"things".Singularize(); // uses the SingularizationServices of System.Data.Entity

"enum value".ToEnum<EnumType>(); // returns an EnumType object
```

### RepeaterSharp ###
```
repeater.FindControlInHeader("idOfControl");
repeater.FindControlInFooter("idOfControl");
```