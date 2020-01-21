# nanoid-net
[![Build Status](https://travis-ci.org/codeyu/nanoid-net.svg?branch=master)](https://travis-ci.org/codeyu/nanoid-net) [![Build status](https://ci.appveyor.com/api/projects/status/i1ni7r193fs4t9tq/branch/master?svg=true)](https://ci.appveyor.com/project/codeyu/nanoid-net/branch/master)
[![NuGet Badge](https://buildstats.info/nuget/Nanoid)](https://www.nuget.org/packages/Nanoid/) 
[![License](https://img.shields.io/badge/license-MIT%20License-blue.svg)](LICENSE)

This package is .NET implementation of [ai's](https://github.com/ai) [nanoid](https://github.com/ai/nanoid)!

**Safe.** It uses cryptographically strong random generator.

**Compact.** It uses more symbols than UUID (`A-Za-z0-9_-`)
and has the same number of unique options in just 22 symbols instead of 36.

**Fast.** Nanoid is as fast as UUID but can be used in URLs.

## Install

Install with nuget:

``` sh
PM> Install-Package Nanoid
```

## Usage

### Normal

The default method uses URL-friendly symbols (`A-Za-z0-9_-`) and returns an ID
with 21 characters (to have a collision probability similar to UUID v4).

```cs
var id = Nanoid.Generate() //=> "Uakgb_J5m9g-0JDMbcJqLJ"
```

Symbols `-,.()` are not encoded in the URL. If used at the end of a link
they could be identified as a punctuation symbol.

If you want to reduce ID length (and increase collisions probability),
you can pass the size as an argument:

```cs
var id = Nanoid.Generate(size:10) //=> "IRFa-VaY2b"
```

### Custom Alphabet or Length

If you want to change the ID's alphabet or length
you can pass alphabet and size.

```cs
var id = Nanoid.Generate('1234567890abcdef', 10) //=> "4f90d13a42"
```

Alphabet must contain 256 symbols or less.
Otherwise, the generator will not be secure.


### Custom Random Bytes Generator

You can replace the default safe random generator using the `System.Random` class.
For instance, to use a seed-based generator.

```cs
var random = Random(10);
var id = Nanoid.Generate(random, "abcdef", 10) //=> "fbaefaadeb"
```

## Credits

- [ai](https://github.com/ai) - [nanoid](https://github.com/ai/nanoid)

## License

The MIT License (MIT). Please see [License File](LICENSE) for more information.