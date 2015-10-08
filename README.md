# dnformat

## Why

When you're like, "I want this number to have a leading zero but I can't remember how it works" so you look in up in [format strings][1] but then you want to test it. So you open a console and type:

	$ dnformat "{0:D2}" int:3
	03
	
Yaaay. Now you know.

## Usage

For the help text, run

	$ dnformat /?
	
Which displays this:

	Usage:
		dnformat format type1:param1,type2:param2...
	Examples:
		dnformat "hello {0}" string:world
		dnformat "{0:P} glue" float:0.05
		dnformat "{0:X} {0:X08}" int:37295

The first argument is a format string, like you would pass to `String.Format` or to `Console.WriteLine`. It's best to put this in speech marks to group it as one argument. After that, pass your params in the format `type:value` as shown in examples above.

## License

Released into the public domain by means of the [UNLICENSE][2]. No rights reserved.

By [@SteGriff](https://twitter.com/stegriff)

[1]: https://msdn.microsoft.com/en-us/library/26etazsy%28v=vs.110%29.aspx
[2]: http://unlicense.org