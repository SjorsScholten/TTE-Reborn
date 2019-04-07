# TTE-Reborn

Please use English in all code and documentation.

Don't just push everything to the master branch, make your own.
Unsure how to do this? Ask a colleague.

Variables should be in camelCase: int camelCase = 0;

Methods: private int TestMethod()

Please provide all methods of a Summary.

Try to make your code reusable. Don't make code specifically for one thing.

Inheritance is a thing that is useful, use it.

We are using Unity, so:
- Variables you want to assign in the editor and should not be accessed by other scripts should be private with the [SerializeField] attribute.
- Try to use the [Tooltip] attribute to explain your variables. This makes things easier for everyone.
- Does your script have a lot of variables? Group them using the [Header] attribute.
- Do you need to access a variable in another script? Try to make it a private set.
- Do you use the same data in a lot of different scripts? Please make use of Scriptable Objects.
- Avoid spaghetti, it is evil. Make use of Events when you can.
- Use regions when necessary.

# Versions

Ink: https://github.com/inkle/inky/releases/tag/0.10.0b

Unity: 2018.3.0f2
