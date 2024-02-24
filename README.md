# nanoid-net
[![Build Status](https://travis-ci.org/codeyu/nanoid-net.svg?branch=master)](https://travis-ci.org/codeyu/nanoid-net) [![Build status](https://ci.appveyor.com/api/projects/status/i1ni7r193fs4t9tq/branch/master?svg=true)](https://ci.appveyor.com/project/codeyu/nanoid-net/branch/master)
[![NuGet Badge](https://buildstats.info/nuget/Nanoid)](https://www.nuget.org/packages/Nanoid/) 
[![License](https://img.shields.io/badge/license-MIT%20License-blue.svg)](LICENSE)

This package is .NET implementation of [ai's](https://github.com/ai) [nanoid](https://github.com/ai/nanoid)!

**Safe.** It uses thread safe cryptographically strong random generator by default.

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
var id = Nanoid.Generate(); //=> "Uakgb_J5m9g-0JDMbcJqLJ"
```

Symbols `-,.()` are not encoded in the URL. If used at the end of a link
they could be identified as a punctuation symbol.

If you want to reduce ID length (and increase collisions probability),
you can pass the size as an argument:

```cs
var id = Nanoid.Generate(size: 10); //=> "IRFa-VaY2b"
```

### Custom Alphabet or Length

If you want to change the ID's alphabet or length
you can pass alphabet and size.

```cs
var id1 = Nanoid.Generate(Nanoid.Alphabets.LowercaseLettersAndDigits, 10); //=> "4f90d13a42"
var id2 = Nanoid.Generate("1234567890abcdef", 5); //=> "2x501"
```

You can find a variety of useful alphabets typically used for
Nanoid generation in `Nanoid.Alphabets`.

To reason about the collision probabilities of a particular
alphabet and id length combination, use the [nanoid collision calculator](https://zelark.github.io/nano-id-cc/).

### Random Bytes Generator and Thread Safety

By default, we use an internal global [ThreadStatic](https://learn.microsoft.com/en-us/dotnet/api/system.threadstaticattribute?view=net-8.0&redirectedfrom=MSDN)
wrapper over `System.Security.Cryptography.RandomNumberGenerator` to generate Nanoids in a cryptographically secure
manner.

The `ThreadStatic` attribute ensures that you can safely generate ids across threads without having
to pass in your own `Random` object, by creating a separate instance of our random number generator per thread.
This method also avoids the need for any locks which ensures the id generation is fast as advertised.

You can replace the default safe random generator using the `System.Random` class.
For instance, to use a seed-based generator you can do this:

```cs
var random = Random(10);
var id = Nanoid.Generate(random, Nanoid.Alphabets.Letters, 10) //=> "fbAeFaaDeb"
```

If you want to use your own **global** random number generator, make sure you are aware of the
thread safety implications.

Also note that the global random number generator is lazily initialized on first usage.
If you need to preload it you can call  the `Nanoid.GlobalRandom` getter to force the
initialization to happen at that moment **on the current thread**.

## Credits

- [ai](https://github.com/ai) - [nanoid](https://github.com/ai/nanoid)

## License

The MIT License (MIT). Please see [License File](LICENSE) for more information.