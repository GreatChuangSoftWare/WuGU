/*--------------------------------------------------------------------------
* linq.js - LINQ for JavaScript
* ver 2.2.0.2 (Jan. 21th, 2011)
*
* created and maintained by neuecc <ils@neue.cc>
* licensed under Microsoft Public License(Ms-PL)
* http://neue.cc/
* http://linqjs.codeplex.com/
*--------------------------------------------------------------------------*/
jQuery.extend({ ENumberable: (function (){
    var ENumberable = function (getENumberator)
    {
        this.GetENumberator = getENumberator;
    }

    // Generator

    ENumberable.Choice = function () // variable argument
    {
        var args = (arguments[0] instanceof Array) ? arguments[0] : arguments;

        return new ENumberable(function ()
        {
            return new IENumberator(
                Functions.Blank,
                function ()
                {
                    return this.Yield(args[Math.floor(Math.random() * args.length)]);
                },
                Functions.Blank);
        });
    }

    ENumberable.Cycle = function () // variable argument
    {
        var args = (arguments[0] instanceof Array) ? arguments[0] : arguments;

        return new ENumberable(function ()
        {
            var index = 0;
            return new IENumberator(
                Functions.Blank,
                function ()
                {
                    if (index >= args.length) index = 0;
                    return this.Yield(args[index++]);
                },
                Functions.Blank);
        });
    }

    ENumberable.Empty = function ()
    {
        return new ENumberable(function ()
        {
            return new IENumberator(
                Functions.Blank,
                function () { return false; },
                Functions.Blank);
        });
    }

    ENumberable.From = function (obj)
    {
        if (obj == null)
        {
            return ENumberable.Empty();
        }
        if (obj instanceof ENumberable)
        {
            return obj;
        }
        if (typeof obj == Types.Number || typeof obj == Types.Boolean)
        {
            return ENumberable.Repeat(obj, 1);
        }
        if (typeof obj == Types.String)
        {
            return new ENumberable(function ()
            {
                var index = 0;
                return new IENumberator(
                    Functions.Blank,
                    function ()
                    {
                        return (index < obj.length) ? this.Yield(obj.charAt(index++)) : false;
                    },
                    Functions.Blank);
            });
        }
        if (typeof obj != Types.Function)
        {
            // array or array like object
            if (typeof obj.length == Types.Number)
            {
                return new ArrayENumberable(obj);
            }

            // JScript's IENumberable
            if (!(obj instanceof Object) && Utils.IsIENumberable(obj))
            {
                return new ENumberable(function ()
                {
                    var isFirst = true;
                    var eNumberator;
                    return new IENumberator(
                        function () { eNumberator = new ENumberator(obj); },
                        function ()
                        {
                            if (isFirst) isFirst = false;
                            else eNumberator.moveNext();

                            return (eNumberator.atEnd()) ? false : this.Yield(eNumberator.item());
                        },
                        Functions.Blank);
                });
            }
        }

        // case function/object : Create KeyValuePair[]
        return new ENumberable(function ()
        {
            var array = [];
            var index = 0;

            return new IENumberator(
                function ()
                {
                    for (var key in obj)
                    {
                        if (!(obj[key] instanceof Function))
                        {
                            array.push({ Key: key, Value: obj[key] });
                        }
                    }
                },
                function ()
                {
                    return (index < array.length)
                        ? this.Yield(array[index++])
                        : false;
                },
                Functions.Blank);
        });
    },

    ENumberable.Return = function (element)
    {
        return ENumberable.Repeat(element, 1);
    }

    // Overload:function(input, pattern)
    // Overload:function(input, pattern, flags)
    ENumberable.Matches = function (input, pattern, flags)
    {
        if (flags == null) flags = "";
        if (pattern instanceof RegExp)
        {
            flags += (pattern.ignoreCase) ? "i" : "";
            flags += (pattern.multiline) ? "m" : "";
            pattern = pattern.source;
        }
        if (flags.indexOf("g") === -1) flags += "g";

        return new ENumberable(function ()
        {
            var regex;
            return new IENumberator(
                function () { regex = new RegExp(pattern, flags) },
                function ()
                {
                    var match = regex.exec(input);
                    return (match) ? this.Yield(match) : false;
                },
                Functions.Blank);
        });
    }

    // Overload:function(start, count)
    // Overload:function(start, count, step)
    ENumberable.Range = function (start, count, step)
    {
        if (step == null) step = 1;
        return ENumberable.ToInfinity(start, step).Take(count);
    }

    // Overload:function(start, count)
    // Overload:function(start, count, step)
    ENumberable.RangeDown = function (start, count, step)
    {
        if (step == null) step = 1;
        return ENumberable.ToNegativeInfinity(start, step).Take(count);
    }

    // Overload:function(start, to)
    // Overload:function(start, to, step)
    ENumberable.RangeTo = function (start, to, step)
    {
        if (step == null) step = 1;
        return (start < to)
            ? ENumberable.ToInfinity(start, step).TakeWhile(function (i) { return i <= to; })
            : ENumberable.ToNegativeInfinity(start, step).TakeWhile(function (i) { return i >= to; })
    }

    // Overload:function(obj)
    // Overload:function(obj, num)
    ENumberable.Repeat = function (obj, num)
    {
        if (num != null) return ENumberable.Repeat(obj).Take(num);

        return new ENumberable(function ()
        {
            return new IENumberator(
                Functions.Blank,
                function () { return this.Yield(obj); },
                Functions.Blank);
        });
    }

    ENumberable.RepeatWithFinalize = function (initializer, finalizer)
    {
        initializer = Utils.CreateLambda(initializer);
        finalizer = Utils.CreateLambda(finalizer);

        return new ENumberable(function ()
        {
            var element;
            return new IENumberator(
                function () { element = initializer(); },
                function () { return this.Yield(element); },
                function ()
                {
                    if (element != null)
                    {
                        finalizer(element);
                        element = null;
                    }
                });
        });
    }

    // Overload:function(func)
    // Overload:function(func, count)
    ENumberable.Generate = function (func, count)
    {
        if (count != null) return ENumberable.Generate(func).Take(count);
        func = Utils.CreateLambda(func);

        return new ENumberable(function ()
        {
            return new IENumberator(
                Functions.Blank,
                function () { return this.Yield(func()); },
                Functions.Blank);
        });
    }

    // Overload:function()
    // Overload:function(start)
    // Overload:function(start, step)
    ENumberable.ToInfinity = function (start, step)
    {
        if (start == null) start = 0;
        if (step == null) step = 1;

        return new ENumberable(function ()
        {
            var value;
            return new IENumberator(
                function () { value = start - step },
                function () { return this.Yield(value += step); },
                Functions.Blank);
        });
    }

    // Overload:function()
    // Overload:function(start)
    // Overload:function(start, step)
    ENumberable.ToNegativeInfinity = function (start, step)
    {
        if (start == null) start = 0;
        if (step == null) step = 1;

        return new ENumberable(function ()
        {
            var value;
            return new IENumberator(
                function () { value = start + step },
                function () { return this.Yield(value -= step); },
                Functions.Blank);
        });
    }

    ENumberable.Unfold = function (seed, func)
    {
        func = Utils.CreateLambda(func);

        return new ENumberable(function ()
        {
            var isFirst = true;
            var value;
            return new IENumberator(
                Functions.Blank,
                function ()
                {
                    if (isFirst)
                    {
                        isFirst = false;
                        value = seed;
                        return this.Yield(value);
                    }
                    value = func(value);
                    return this.Yield(value);
                },
                Functions.Blank);
        });
    }

    // Extension Methods

    ENumberable.prototype =
    {
        /* Projection and Filtering Methods */

        // Overload:function(func)
        // Overload:function(func, resultSelector<element>)
        // Overload:function(func, resultSelector<element, nestLevel>)
        CascadeBreadthFirst: function (func, resultSelector)
        {
            var source = this;
            func = Utils.CreateLambda(func);
            resultSelector = Utils.CreateLambda(resultSelector);

            return new ENumberable(function ()
            {
                var eNumberator;
                var nestLevel = 0;
                var buffer = [];

                return new IENumberator(
                    function () { eNumberator = source.GetENumberator(); },
                    function ()
                    {
                        while (true)
                        {
                            if (eNumberator.MoveNext())
                            {
                                buffer.push(eNumberator.Current());
                                return this.Yield(resultSelector(eNumberator.Current(), nestLevel));
                            }

                            var next = ENumberable.From(buffer).SelectMany(function (x) { return func(x); });
                            if (!next.Any())
                            {
                                return false;
                            }
                            else
                            {
                                nestLevel++;
                                buffer = [];
                                Utils.Dispose(eNumberator);
                                eNumberator = next.GetENumberator();
                            }
                        }
                    },
                    function () { Utils.Dispose(eNumberator); });
            });
        },

        // Overload:function(func)
        // Overload:function(func, resultSelector<element>)
        // Overload:function(func, resultSelector<element, nestLevel>)
        CascadeDepthFirst: function (func, resultSelector)
        {
            var source = this;
            func = Utils.CreateLambda(func);
            resultSelector = Utils.CreateLambda(resultSelector);

            return new ENumberable(function ()
            {
                var eNumberatorStack = [];
                var eNumberator;

                return new IENumberator(
                    function () { eNumberator = source.GetENumberator(); },
                    function ()
                    {
                        while (true)
                        {
                            if (eNumberator.MoveNext())
                            {
                                var value = resultSelector(eNumberator.Current(), eNumberatorStack.length);
                                eNumberatorStack.push(eNumberator);
                                eNumberator = ENumberable.From(func(eNumberator.Current())).GetENumberator();
                                return this.Yield(value);
                            }

                            if (eNumberatorStack.length <= 0) return false;
                            Utils.Dispose(eNumberator);
                            eNumberator = eNumberatorStack.pop();
                        }
                    },
                    function ()
                    {
                        try { Utils.Dispose(eNumberator); }
                        finally { ENumberable.From(eNumberatorStack).ForEach(function (s) { s.Dispose(); }) }
                    });
            });
        },

        Flatten: function ()
        {
            var source = this;

            return new ENumberable(function ()
            {
                var eNumberator;
                var middleENumberator = null;

                return new IENumberator(
                    function () { eNumberator = source.GetENumberator(); },
                    function ()
                    {
                        while (true)
                        {
                            if (middleENumberator != null)
                            {
                                if (middleENumberator.MoveNext())
                                {
                                    return this.Yield(middleENumberator.Current());
                                }
                                else
                                {
                                    middleENumberator = null;
                                }
                            }

                            if (eNumberator.MoveNext())
                            {
                                if (eNumberator.Current() instanceof Array)
                                {
                                    Utils.Dispose(middleENumberator);
                                    middleENumberator = ENumberable.From(eNumberator.Current())
                                        .SelectMany(Functions.Identity)
                                        .Flatten()
                                        .GetENumberator();
                                    continue;
                                }
                                else
                                {
                                    return this.Yield(eNumberator.Current());
                                }
                            }

                            return false;
                        }
                    },
                    function ()
                    {
                        try { Utils.Dispose(eNumberator); }
                        finally { Utils.Dispose(middleENumberator); }
                    });
            });
        },

        Pairwise: function (selector)
        {
            var source = this;
            selector = Utils.CreateLambda(selector);

            return new ENumberable(function ()
            {
                var eNumberator;

                return new IENumberator(
                    function ()
                    {
                        eNumberator = source.GetENumberator();
                        eNumberator.MoveNext();
                    },
                    function ()
                    {
                        var prev = eNumberator.Current();
                        return (eNumberator.MoveNext())
                            ? this.Yield(selector(prev, eNumberator.Current()))
                            : false;
                    },
                    function () { Utils.Dispose(eNumberator); });
            });
        },

        // Overload:function(func)
        // Overload:function(seed,func<value,element>)
        // Overload:function(seed,func<value,element>,resultSelector)
        Scan: function (seed, func, resultSelector)
        {
            if (resultSelector != null) return this.Scan(seed, func).Select(resultSelector);

            var isUseSeed;
            if (func == null)
            {
                func = Utils.CreateLambda(seed); // arguments[0]
                isUseSeed = false;
            }
            else
            {
                func = Utils.CreateLambda(func);
                isUseSeed = true;
            }
            var source = this;

            return new ENumberable(function ()
            {
                var eNumberator;
                var value;
                var isFirst = true;

                return new IENumberator(
                    function () { eNumberator = source.GetENumberator(); },
                    function ()
                    {
                        if (isFirst)
                        {
                            isFirst = false;
                            if (!isUseSeed)
                            {
                                if (eNumberator.MoveNext())
                                {
                                    return this.Yield(value = eNumberator.Current());
                                }
                            }
                            else
                            {
                                return this.Yield(value = seed);
                            }
                        }

                        return (eNumberator.MoveNext())
                            ? this.Yield(value = func(value, eNumberator.Current()))
                            : false;
                    },
                    function () { Utils.Dispose(eNumberator); });
            });
        },

        // Overload:function(selector<element>)
        // Overload:function(selector<element,index>)
        Select: function (selector)
        {
            var source = this;
            selector = Utils.CreateLambda(selector);

            return new ENumberable(function ()
            {
                var eNumberator;
                var index = 0;

                return new IENumberator(
                    function () { eNumberator = source.GetENumberator(); },
                    function ()
                    {
                        return (eNumberator.MoveNext())
                            ? this.Yield(selector(eNumberator.Current(), index++))
                            : false;
                    },
                    function () { Utils.Dispose(eNumberator); })
            });
        },

        // Overload:function(collectionSelector<element>)
        // Overload:function(collectionSelector<element,index>)
        // Overload:function(collectionSelector<element>,resultSelector)
        // Overload:function(collectionSelector<element,index>,resultSelector)
        SelectMany: function (collectionSelector, resultSelector)
        {
            var source = this;
            collectionSelector = Utils.CreateLambda(collectionSelector);
            if (resultSelector == null) resultSelector = function (a, b) { return b; }
            resultSelector = Utils.CreateLambda(resultSelector);

            return new ENumberable(function ()
            {
                var eNumberator;
                var middleENumberator = undefined;
                var index = 0;

                return new IENumberator(
                    function () { eNumberator = source.GetENumberator(); },
                    function ()
                    {
                        if (middleENumberator === undefined)
                        {
                            if (!eNumberator.MoveNext()) return false;
                        }
                        do
                        {
                            if (middleENumberator == null)
                            {
                                var middleSeq = collectionSelector(eNumberator.Current(), index++);
                                middleENumberator = ENumberable.From(middleSeq).GetENumberator();
                            }
                            if (middleENumberator.MoveNext())
                            {
                                return this.Yield(resultSelector(eNumberator.Current(), middleENumberator.Current()));
                            }
                            Utils.Dispose(middleENumberator);
                            middleENumberator = null;
                        } while (eNumberator.MoveNext())
                        return false;
                    },
                    function ()
                    {
                        try { Utils.Dispose(eNumberator); }
                        finally { Utils.Dispose(middleENumberator); }
                    })
            });
        },

        // Overload:function(predicate<element>)
        // Overload:function(predicate<element,index>)
        Where: function (predicate)
        {
            predicate = Utils.CreateLambda(predicate);
            var source = this;

            return new ENumberable(function ()
            {
                var eNumberator;
                var index = 0;

                return new IENumberator(
                    function () { eNumberator = source.GetENumberator(); },
                    function ()
                    {
                        while (eNumberator.MoveNext())
                        {
                            if (predicate(eNumberator.Current(), index++))
                            {
                                return this.Yield(eNumberator.Current());
                            }
                        }
                        return false;
                    },
                    function () { Utils.Dispose(eNumberator); })
            });
        },

        OfType: function (type)
        {
            var typeName;
            switch (type)
            {
                case Number: typeName = Types.Number; break;
                case String: typeName = Types.String; break;
                case Boolean: typeName = Types.Boolean; break;
                case Function: typeName = Types.Function; break;
                default: typeName = null; break;
            }
            return (typeName === null)
                ? this.Where(function (x) { return x instanceof type })
                : this.Where(function (x) { return typeof x === typeName });
        },

        // Overload:function(second,selector<outer,inner>)
        // Overload:function(second,selector<outer,inner,index>)
        Zip: function (second, selector)
        {
            selector = Utils.CreateLambda(selector);
            var source = this;

            return new ENumberable(function ()
            {
                var firstENumberator;
                var secondENumberator;
                var index = 0;

                return new IENumberator(
                    function ()
                    {
                        firstENumberator = source.GetENumberator();
                        secondENumberator = ENumberable.From(second).GetENumberator();
                    },
                    function ()
                    {
                        if (firstENumberator.MoveNext() && secondENumberator.MoveNext())
                        {
                            return this.Yield(selector(firstENumberator.Current(), secondENumberator.Current(), index++));
                        }
                        return false;
                    },
                    function ()
                    {
                        try { Utils.Dispose(firstENumberator); }
                        finally { Utils.Dispose(secondENumberator); }
                    })
            });
        },

        /* Join Methods */

        // Overload:function (inner, outerKeySelector, innerKeySelector, resultSelector)
        // Overload:function (inner, outerKeySelector, innerKeySelector, resultSelector, compareSelector)
        Join: function (inner, outerKeySelector, innerKeySelector, resultSelector, compareSelector)
        {
            outerKeySelector = Utils.CreateLambda(outerKeySelector);
            innerKeySelector = Utils.CreateLambda(innerKeySelector);
            resultSelector = Utils.CreateLambda(resultSelector);
            compareSelector = Utils.CreateLambda(compareSelector);
            var source = this;

            return new ENumberable(function ()
            {
                var outerENumberator;
                var lookup;
                var innerElements = null;
                var innerCount = 0;

                return new IENumberator(
                    function ()
                    {
                        outerENumberator = source.GetENumberator();
                        lookup = ENumberable.From(inner).ToLookup(innerKeySelector, Functions.Identity, compareSelector);
                    },
                    function ()
                    {
                        while (true)
                        {
                            if (innerElements != null)
                            {
                                var innerElement = innerElements[innerCount++];
                                if (innerElement !== undefined)
                                {
                                    return this.Yield(resultSelector(outerENumberator.Current(), innerElement));
                                }

                                innerElement = null;
                                innerCount = 0;
                            }

                            if (outerENumberator.MoveNext())
                            {
                                var key = outerKeySelector(outerENumberator.Current());
                                innerElements = lookup.Get(key).ToArray();
                            }
                            else
                            {
                                return false;
                            }
                        }
                    },
                    function () { Utils.Dispose(outerENumberator); })
            });
        },

        // Overload:function (inner, outerKeySelector, innerKeySelector, resultSelector)
        // Overload:function (inner, outerKeySelector, innerKeySelector, resultSelector, compareSelector)
        GroupJoin: function (inner, outerKeySelector, innerKeySelector, resultSelector, compareSelector)
        {
            outerKeySelector = Utils.CreateLambda(outerKeySelector);
            innerKeySelector = Utils.CreateLambda(innerKeySelector);
            resultSelector = Utils.CreateLambda(resultSelector);
            compareSelector = Utils.CreateLambda(compareSelector);
            var source = this;

            return new ENumberable(function ()
            {
                var eNumberator = source.GetENumberator();
                var lookup = null;

                return new IENumberator(
                    function ()
                    {
                        eNumberator = source.GetENumberator();
                        lookup = ENumberable.From(inner).ToLookup(innerKeySelector, Functions.Identity, compareSelector);
                    },
                    function ()
                    {
                        if (eNumberator.MoveNext())
                        {
                            var innerElement = lookup.Get(outerKeySelector(eNumberator.Current()));
                            return this.Yield(resultSelector(eNumberator.Current(), innerElement));
                        }
                        return false;
                    },
                    function () { Utils.Dispose(eNumberator); })
            });
        },

        /* Set Methods */

        All: function (predicate)
        {
            predicate = Utils.CreateLambda(predicate);

            var result = true;
            this.ForEach(function (x)
            {
                if (!predicate(x))
                {
                    result = false;
                    return false; // break
                }
            });
            return result;
        },

        // Overload:function()
        // Overload:function(predicate)
        Any: function (predicate)
        {
            predicate = Utils.CreateLambda(predicate);

            var eNumberator = this.GetENumberator();
            try
            {
                if (arguments.length == 0) return eNumberator.MoveNext(); // case:function()

                while (eNumberator.MoveNext()) // case:function(predicate)
                {
                    if (predicate(eNumberator.Current())) return true;
                }
                return false;
            }
            finally { Utils.Dispose(eNumberator); }
        },

        Concat: function (second)
        {
            var source = this;

            return new ENumberable(function ()
            {
                var firstENumberator;
                var secondENumberator;

                return new IENumberator(
                    function () { firstENumberator = source.GetENumberator(); },
                    function ()
                    {
                        if (secondENumberator == null)
                        {
                            if (firstENumberator.MoveNext()) return this.Yield(firstENumberator.Current());
                            secondENumberator = ENumberable.From(second).GetENumberator();
                        }
                        if (secondENumberator.MoveNext()) return this.Yield(secondENumberator.Current());
                        return false;
                    },
                    function ()
                    {
                        try { Utils.Dispose(firstENumberator); }
                        finally { Utils.Dispose(secondENumberator); }
                    })
            });
        },

        Insert: function (index, second)
        {
            var source = this;

            return new ENumberable(function ()
            {
                var firstENumberator;
                var secondENumberator;
                var count = 0;
                var isENumberated = false;

                return new IENumberator(
                    function ()
                    {
                        firstENumberator = source.GetENumberator();
                        secondENumberator = ENumberable.From(second).GetENumberator();
                    },
                    function ()
                    {
                        if (count == index && secondENumberator.MoveNext())
                        {
                            isENumberated = true;
                            return this.Yield(secondENumberator.Current());
                        }
                        if (firstENumberator.MoveNext())
                        {
                            count++;
                            return this.Yield(firstENumberator.Current());
                        }
                        if (!isENumberated && secondENumberator.MoveNext())
                        {
                            return this.Yield(secondENumberator.Current());
                        }
                        return false;
                    },
                    function ()
                    {
                        try { Utils.Dispose(firstENumberator); }
                        finally { Utils.Dispose(secondENumberator); }
                    })
            });
        },

        Alternate: function (value)
        {
            value = ENumberable.Return(value);
            return this.SelectMany(function (elem)
            {
                return ENumberable.Return(elem).Concat(value);
            }).TakeExceptLast();
        },

        // Overload:function(value)
        // Overload:function(value, compareSelector)
        Contains: function (value, compareSelector)
        {
            compareSelector = Utils.CreateLambda(compareSelector);
            var eNumberator = this.GetENumberator();
            try
            {
                while (eNumberator.MoveNext())
                {
                    if (compareSelector(eNumberator.Current()) === value) return true;
                }
                return false;
            }
            finally { Utils.Dispose(eNumberator) }
        },

        DefaultIfEmpty: function (defaultValue)
        {
            var source = this;

            return new ENumberable(function ()
            {
                var eNumberator;
                var isFirst = true;

                return new IENumberator(
                    function () { eNumberator = source.GetENumberator(); },
                    function ()
                    {
                        if (eNumberator.MoveNext())
                        {
                            isFirst = false;
                            return this.Yield(eNumberator.Current());
                        }
                        else if (isFirst)
                        {
                            isFirst = false;
                            return this.Yield(defaultValue);
                        }
                        return false;
                    },
                    function () { Utils.Dispose(eNumberator); })
            });
        },

        // Overload:function()
        // Overload:function(compareSelector)
        Distinct: function (compareSelector)
        {
            return this.Except(ENumberable.Empty(), compareSelector);
        },

        // Overload:function(second)
        // Overload:function(second, compareSelector)
        Except: function (second, compareSelector)
        {
            compareSelector = Utils.CreateLambda(compareSelector);
            var source = this;

            return new ENumberable(function ()
            {
                var eNumberator;
                var keys;

                return new IENumberator(
                    function ()
                    {
                        eNumberator = source.GetENumberator();
                        keys = new Dictionary(compareSelector);
                        ENumberable.From(second).ForEach(function (key) { keys.Add(key); });
                    },
                    function ()
                    {
                        while (eNumberator.MoveNext())
                        {
                            var current = eNumberator.Current();
                            if (!keys.Contains(current))
                            {
                                keys.Add(current);
                                return this.Yield(current);
                            }
                        }
                        return false;
                    },
                    function () { Utils.Dispose(eNumberator); })
            });
        },

        // Overload:function(second)
        // Overload:function(second, compareSelector)
        Intersect: function (second, compareSelector)
        {
            compareSelector = Utils.CreateLambda(compareSelector);
            var source = this;

            return new ENumberable(function ()
            {
                var eNumberator;
                var keys;
                var outs;

                return new IENumberator(
                    function ()
                    {
                        eNumberator = source.GetENumberator();

                        keys = new Dictionary(compareSelector);
                        ENumberable.From(second).ForEach(function (key) { keys.Add(key); });
                        outs = new Dictionary(compareSelector);
                    },
                    function ()
                    {
                        while (eNumberator.MoveNext())
                        {
                            var current = eNumberator.Current();
                            if (!outs.Contains(current) && keys.Contains(current))
                            {
                                outs.Add(current);
                                return this.Yield(current);
                            }
                        }
                        return false;
                    },
                    function () { Utils.Dispose(eNumberator); })
            });
        },

        // Overload:function(second)
        // Overload:function(second, compareSelector)
        SequenceEqual: function (second, compareSelector)
        {
            compareSelector = Utils.CreateLambda(compareSelector);

            var firstENumberator = this.GetENumberator();
            try
            {
                var secondENumberator = ENumberable.From(second).GetENumberator();
                try
                {
                    while (firstENumberator.MoveNext())
                    {
                        if (!secondENumberator.MoveNext()
                            || compareSelector(firstENumberator.Current()) !== compareSelector(secondENumberator.Current()))
                        {
                            return false;
                        }
                    }

                    if (secondENumberator.MoveNext()) return false;
                    return true;
                }
                finally { Utils.Dispose(secondENumberator); }
            }
            finally { Utils.Dispose(firstENumberator); }
        },

        Union: function (second, compareSelector)
        {
            compareSelector = Utils.CreateLambda(compareSelector);
            var source = this;

            return new ENumberable(function ()
            {
                var firstENumberator;
                var secondENumberator;
                var keys;

                return new IENumberator(
                    function ()
                    {
                        firstENumberator = source.GetENumberator();
                        keys = new Dictionary(compareSelector);
                    },
                    function ()
                    {
                        var current;
                        if (secondENumberator === undefined)
                        {
                            while (firstENumberator.MoveNext())
                            {
                                current = firstENumberator.Current();
                                if (!keys.Contains(current))
                                {
                                    keys.Add(current);
                                    return this.Yield(current);
                                }
                            }
                            secondENumberator = ENumberable.From(second).GetENumberator();
                        }
                        while (secondENumberator.MoveNext())
                        {
                            current = secondENumberator.Current();
                            if (!keys.Contains(current))
                            {
                                keys.Add(current);
                                return this.Yield(current);
                            }
                        }
                        return false;
                    },
                    function ()
                    {
                        try { Utils.Dispose(firstENumberator); }
                        finally { Utils.Dispose(secondENumberator); }
                    })
            });
        },

        /* Ordering Methods */

        OrderBy: function (keySelector)
        {
            return new OrderedENumberable(this, keySelector, false);
        },

        OrderByDescending: function (keySelector)
        {
            return new OrderedENumberable(this, keySelector, true);
        },

        Reverse: function ()
        {
            var source = this;

            return new ENumberable(function ()
            {
                var buffer;
                var index;

                return new IENumberator(
                    function ()
                    {
                        buffer = source.ToArray();
                        index = buffer.length;
                    },
                    function ()
                    {
                        return (index > 0)
                            ? this.Yield(buffer[--index])
                            : false;
                    },
                    Functions.Blank)
            });
        },

        Shuffle: function ()
        {
            var source = this;

            return new ENumberable(function ()
            {
                var buffer;

                return new IENumberator(
                    function () { buffer = source.ToArray(); },
                    function ()
                    {
                        if (buffer.length > 0)
                        {
                            var i = Math.floor(Math.random() * buffer.length);
                            return this.Yield(buffer.splice(i, 1)[0]);
                        }
                        return false;
                    },
                    Functions.Blank)
            });
        },

        /* Grouping Methods */

        // Overload:function(keySelector)
        // Overload:function(keySelector,elementSelector)
        // Overload:function(keySelector,elementSelector,resultSelector)
        // Overload:function(keySelector,elementSelector,resultSelector,compareSelector)
        GroupBy: function (keySelector, elementSelector, resultSelector, compareSelector)
        {
            var source = this;
            keySelector = Utils.CreateLambda(keySelector);
            elementSelector = Utils.CreateLambda(elementSelector);
            if (resultSelector != null) resultSelector = Utils.CreateLambda(resultSelector);
            compareSelector = Utils.CreateLambda(compareSelector);

            return new ENumberable(function ()
            {
                var eNumberator;

                return new IENumberator(
                    function ()
                    {
                        eNumberator = source.ToLookup(keySelector, elementSelector, compareSelector)
                            .ToENumberable()
                            .GetENumberator();
                    },
                    function ()
                    {
                        while (eNumberator.MoveNext())
                        {
                            return (resultSelector == null)
                                ? this.Yield(eNumberator.Current())
                                : this.Yield(resultSelector(eNumberator.Current().Key(), eNumberator.Current()));
                        }
                        return false;
                    },
                    function () { Utils.Dispose(eNumberator); })
            });
        },

        // Overload:function(keySelector)
        // Overload:function(keySelector,elementSelector)
        // Overload:function(keySelector,elementSelector,resultSelector)
        // Overload:function(keySelector,elementSelector,resultSelector,compareSelector)
        PartitionBy: function (keySelector, elementSelector, resultSelector, compareSelector)
        {

            var source = this;
            keySelector = Utils.CreateLambda(keySelector);
            elementSelector = Utils.CreateLambda(elementSelector);
            compareSelector = Utils.CreateLambda(compareSelector);
            var hasResultSelector;
            if (resultSelector == null)
            {
                hasResultSelector = false;
                resultSelector = function (key, group) { return new Grouping(key, group) }
            }
            else
            {
                hasResultSelector = true;
                resultSelector = Utils.CreateLambda(resultSelector);
            }

            return new ENumberable(function ()
            {
                var eNumberator;
                var key;
                var compareKey;
                var group = [];

                return new IENumberator(
                    function ()
                    {
                        eNumberator = source.GetENumberator();
                        if (eNumberator.MoveNext())
                        {
                            key = keySelector(eNumberator.Current());
                            compareKey = compareSelector(key);
                            group.push(elementSelector(eNumberator.Current()));
                        }
                    },
                    function ()
                    {
                        var hasNext;
                        while ((hasNext = eNumberator.MoveNext()) == true)
                        {
                            if (compareKey === compareSelector(keySelector(eNumberator.Current())))
                            {
                                group.push(elementSelector(eNumberator.Current()));
                            }
                            else break;
                        }

                        if (group.length > 0)
                        {
                            var result = (hasResultSelector)
                                ? resultSelector(key, ENumberable.From(group))
                                : resultSelector(key, group);
                            if (hasNext)
                            {
                                key = keySelector(eNumberator.Current());
                                compareKey = compareSelector(key);
                                group = [elementSelector(eNumberator.Current())];
                            }
                            else group = [];

                            return this.Yield(result);
                        }

                        return false;
                    },
                    function () { Utils.Dispose(eNumberator); })
            });
        },

        BufferWithCount: function (count)
        {
            var source = this;

            return new ENumberable(function ()
            {
                var eNumberator;

                return new IENumberator(
                function () { eNumberator = source.GetENumberator(); },
                function ()
                {
                    var array = [];
                    var index = 0;
                    while (eNumberator.MoveNext())
                    {
                        array.push(eNumberator.Current());
                        if (++index >= count) return this.Yield(array);
                    }
                    if (array.length > 0) return this.Yield(array);
                    return false;
                },
                function () { Utils.Dispose(eNumberator); })
            });
        },

        /* Aggregate Methods */

        // Overload:function(func)
        // Overload:function(seed,func)
        // Overload:function(seed,func,resultSelector)
        Aggregate: function (seed, func, resultSelector)
        {
            return this.Scan(seed, func, resultSelector).Last();
        },

        // Overload:function()
        // Overload:function(selector)
        Average: function (selector)
        {
            selector = Utils.CreateLambda(selector);

            var sum = 0;
            var count = 0;
            this.ForEach(function (x)
            {
                sum += selector(x);
                ++count;
            });

            return sum / count;
        },

        // Overload:function()
        // Overload:function(predicate)
        Count: function (predicate)
        {
            predicate = (predicate == null) ? Functions.True : Utils.CreateLambda(predicate);

            var count = 0;
            this.ForEach(function (x, i)
            {
                if (predicate(x, i)) ++count;
            });
            return count;
        },

        // Overload:function()
        // Overload:function(selector)
        Max: function (selector)
        {
            if (selector == null) selector = Functions.Identity;
            return this.Select(selector).Aggregate(function (a, b) { return (a > b) ? a : b; });
        },

        // Overload:function()
        // Overload:function(selector)
        Min: function (selector)
        {
            if (selector == null) selector = Functions.Identity;
            return this.Select(selector).Aggregate(function (a, b) { return (a < b) ? a : b; });
        },

        MaxBy: function (keySelector)
        {
            keySelector = Utils.CreateLambda(keySelector);
            return this.Aggregate(function (a, b) { return (keySelector(a) > keySelector(b)) ? a : b });
        },

        MinBy: function (keySelector)
        {
            keySelector = Utils.CreateLambda(keySelector);
            return this.Aggregate(function (a, b) { return (keySelector(a) < keySelector(b)) ? a : b });
        },

        // Overload:function()
        // Overload:function(selector)
        Sum: function (selector)
        {
            if (selector == null) selector = Functions.Identity;
            return this.Select(selector).Aggregate(0, function (a, b) { return a + b; });
        },

        /* Paging Methods */

        ElementAt: function (index)
        {
            var value;
            var found = false;
            this.ForEach(function (x, i)
            {
                if (i == index)
                {
                    value = x;
                    found = true;
                    return false;
                }
            });

            if (!found) throw new Error("index is less than 0 or greater than or equal to the number of elements in source.");
            return value;
        },

        ElementAtOrDefault: function (index, defaultValue)
        {
            var value;
            var found = false;
            this.ForEach(function (x, i)
            {
                if (i == index)
                {
                    value = x;
                    found = true;
                    return false;
                }
            });

            return (!found) ? defaultValue : value;
        },

        // Overload:function()
        // Overload:function(predicate)
        First: function (predicate)
        {
            if (predicate != null) return this.Where(predicate).First();

            var value;
            var found = false;
            this.ForEach(function (x)
            {
                value = x;
                found = true;
                return false;
            });

            if (!found) throw new Error("First:No element satisfies the condition.");
            return value;
        },

        // Overload:function(defaultValue)
        // Overload:function(defaultValue,predicate)
        FirstOrDefault: function (defaultValue, predicate)
        {
            if (predicate != null) return this.Where(predicate).FirstOrDefault(defaultValue);

            var value;
            var found = false;
            this.ForEach(function (x)
            {
                value = x;
                found = true;
                return false;
            });
            return (!found) ? defaultValue : value;
        },

        // Overload:function()
        // Overload:function(predicate)
        Last: function (predicate)
        {
            if (predicate != null) return this.Where(predicate).Last();

            var value;
            var found = false;
            this.ForEach(function (x)
            {
                found = true;
                value = x;
            });

            if (!found) throw new Error("Last:No element satisfies the condition.");
            return value;
        },

        // Overload:function(defaultValue)
        // Overload:function(defaultValue,predicate)
        LastOrDefault: function (defaultValue, predicate)
        {
            if (predicate != null) return this.Where(predicate).LastOrDefault(defaultValue);

            var value;
            var found = false;
            this.ForEach(function (x)
            {
                found = true;
                value = x;
            });
            return (!found) ? defaultValue : value;
        },

        // Overload:function()
        // Overload:function(predicate)
        Single: function (predicate)
        {
            if (predicate != null) return this.Where(predicate).Single();

            var value;
            var found = false;
            this.ForEach(function (x)
            {
                if (!found)
                {
                    found = true;
                    value = x;
                }
                else throw new Error("Single:sequence contains more than one element.");
            });

            if (!found) throw new Error("Single:No element satisfies the condition.");
            return value;
        },

        // Overload:function(defaultValue)
        // Overload:function(defaultValue,predicate)
        SingleOrDefault: function (defaultValue, predicate)
        {
            if (predicate != null) return this.Where(predicate).SingleOrDefault(defaultValue);

            var value;
            var found = false;
            this.ForEach(function (x)
            {
                if (!found)
                {
                    found = true;
                    value = x;
                }
                else throw new Error("Single:sequence contains more than one element.");
            });

            return (!found) ? defaultValue : value;
        },

        Skip: function (count)
        {
            var source = this;

            return new ENumberable(function ()
            {
                var eNumberator;
                var index = 0;

                return new IENumberator(
                    function ()
                    {
                        eNumberator = source.GetENumberator();
                        while (index++ < count && eNumberator.MoveNext()) { };
                    },
                    function ()
                    {
                        return (eNumberator.MoveNext())
                            ? this.Yield(eNumberator.Current())
                            : false;
                    },
                    function () { Utils.Dispose(eNumberator); })
            });
        },

        // Overload:function(predicate<element>)
        // Overload:function(predicate<element,index>)
        SkipWhile: function (predicate)
        {
            predicate = Utils.CreateLambda(predicate);
            var source = this;

            return new ENumberable(function ()
            {
                var eNumberator;
                var index = 0;
                var isSkipEnd = false;

                return new IENumberator(
                    function () { eNumberator = source.GetENumberator(); },
                    function ()
                    {
                        while (!isSkipEnd)
                        {
                            if (eNumberator.MoveNext())
                            {
                                if (!predicate(eNumberator.Current(), index++))
                                {
                                    isSkipEnd = true;
                                    return this.Yield(eNumberator.Current());
                                }
                                continue;
                            }
                            else return false;
                        }

                        return (eNumberator.MoveNext())
                            ? this.Yield(eNumberator.Current())
                            : false;

                    },
                    function () { Utils.Dispose(eNumberator); });
            });
        },

        Take: function (count)
        {
            var source = this;

            return new ENumberable(function ()
            {
                var eNumberator;
                var index = 0;

                return new IENumberator(
                    function () { eNumberator = source.GetENumberator(); },
                    function ()
                    {
                        return (index++ < count && eNumberator.MoveNext())
                            ? this.Yield(eNumberator.Current())
                            : false;
                    },
                    function () { Utils.Dispose(eNumberator); }
                )
            });
        },

        // Overload:function(predicate<element>)
        // Overload:function(predicate<element,index>)
        TakeWhile: function (predicate)
        {
            predicate = Utils.CreateLambda(predicate);
            var source = this;

            return new ENumberable(function ()
            {
                var eNumberator;
                var index = 0;

                return new IENumberator(
                    function () { eNumberator = source.GetENumberator(); },
                    function ()
                    {
                        return (eNumberator.MoveNext() && predicate(eNumberator.Current(), index++))
                            ? this.Yield(eNumberator.Current())
                            : false;
                    },
                    function () { Utils.Dispose(eNumberator); });
            });
        },

        // Overload:function()
        // Overload:function(count)
        TakeExceptLast: function (count)
        {
            if (count == null) count = 1;
            var source = this;

            return new ENumberable(function ()
            {
                if (count <= 0) return source.GetENumberator(); // do nothing

                var eNumberator;
                var q = [];

                return new IENumberator(
                    function () { eNumberator = source.GetENumberator(); },
                    function ()
                    {
                        while (eNumberator.MoveNext())
                        {
                            if (q.length == count)
                            {
                                q.push(eNumberator.Current());
                                return this.Yield(q.shift());
                            }
                            q.push(eNumberator.Current());
                        }
                        return false;
                    },
                    function () { Utils.Dispose(eNumberator); });
            });
        },

        TakeFromLast: function (count)
        {
            if (count <= 0 || count == null) return ENumberable.Empty();
            var source = this;

            return new ENumberable(function ()
            {
                var sourceENumberator;
                var eNumberator;
                var q = [];

                return new IENumberator(
                    function () { sourceENumberator = source.GetENumberator(); },
                    function ()
                    {
                        while (sourceENumberator.MoveNext())
                        {
                            if (q.length == count) q.shift()
                            q.push(sourceENumberator.Current());
                        }
                        if (eNumberator == null)
                        {
                            eNumberator = ENumberable.From(q).GetENumberator();
                        }
                        return (eNumberator.MoveNext())
                            ? this.Yield(eNumberator.Current())
                            : false;
                    },
                    function () { Utils.Dispose(eNumberator); });
            });
        },

        IndexOf: function (item)
        {
            var found = null;
            this.ForEach(function (x, i)
            {
                if (x === item)
                {
                    found = i;
                    return true;
                }
            });

            return (found !== null) ? found : -1;
        },

        LastIndexOf: function (item)
        {
            var result = -1;
            this.ForEach(function (x, i)
            {
                if (x === item) result = i;
            });

            return result;
        },

        /* Convert Methods */

        ToArray: function ()
        {
            var array = [];
            this.ForEach(function (x) { array.push(x) });
            return array;
        },

        // Overload:function(keySelector)
        // Overload:function(keySelector, elementSelector)
        // Overload:function(keySelector, elementSelector, compareSelector)
        ToLookup: function (keySelector, elementSelector, compareSelector)
        {
            keySelector = Utils.CreateLambda(keySelector);
            elementSelector = Utils.CreateLambda(elementSelector);
            compareSelector = Utils.CreateLambda(compareSelector);

            var dict = new Dictionary(compareSelector);
            this.ForEach(function (x)
            {
                var key = keySelector(x);
                var element = elementSelector(x);

                var array = dict.Get(key);
                if (array !== undefined) array.push(element);
                else dict.Add(key, [element]);
            });
            return new Lookup(dict);
        },

        ToObject: function (keySelector, elementSelector)
        {
            keySelector = Utils.CreateLambda(keySelector);
            elementSelector = Utils.CreateLambda(elementSelector);

            var obj = {};
            this.ForEach(function (x)
            {
                obj[keySelector(x)] = elementSelector(x);
            });
            return obj;
        },

        // Overload:function(keySelector, elementSelector)
        // Overload:function(keySelector, elementSelector, compareSelector)
        ToDictionary: function (keySelector, elementSelector, compareSelector)
        {
            keySelector = Utils.CreateLambda(keySelector);
            elementSelector = Utils.CreateLambda(elementSelector);
            compareSelector = Utils.CreateLambda(compareSelector);

            var dict = new Dictionary(compareSelector);
            this.ForEach(function (x)
            {
                dict.Add(keySelector(x), elementSelector(x));
            });
            return dict;
        },

        // Overload:function()
        // Overload:function(replacer)
        // Overload:function(replacer, space)
        ToJSON: function (replacer, space)
        {
            return JSON.stringify(this.ToArray(), replacer, space);
        },

        // Overload:function()
        // Overload:function(separator)
        // Overload:function(separator,selector)
        ToString: function (separator, selector)
        {
            if (separator == null) separator = "";
            if (selector == null) selector = Functions.Identity;

            return this.Select(selector).ToArray().join(separator);
        },


        /* Action Methods */

        // Overload:function(action<element>)
        // Overload:function(action<element,index>)
        Do: function (action)
        {
            var source = this;
            action = Utils.CreateLambda(action);

            return new ENumberable(function ()
            {
                var eNumberator;
                var index = 0;

                return new IENumberator(
                    function () { eNumberator = source.GetENumberator(); },
                    function ()
                    {
                        if (eNumberator.MoveNext())
                        {
                            action(eNumberator.Current(), index++);
                            return this.Yield(eNumberator.Current());
                        }
                        return false;
                    },
                    function () { Utils.Dispose(eNumberator); });
            });
        },

        // Overload:function(action<element>)
        // Overload:function(action<element,index>)
        // Overload:function(func<element,bool>)
        // Overload:function(func<element,index,bool>)
        ForEach: function (action)
        {
            action = Utils.CreateLambda(action);

            var index = 0;
            var eNumberator = this.GetENumberator();
            try
            {
                while (eNumberator.MoveNext())
                {
                    if (action(eNumberator.Current(), index++) === false) break;
                }
            }
            finally { Utils.Dispose(eNumberator); }
        },

        // Overload:function()
        // Overload:function(separator)
        // Overload:function(separator,selector)
        Write: function (separator, selector)
        {
            if (separator == null) separator = "";
            selector = Utils.CreateLambda(selector);

            var isFirst = true;
            this.ForEach(function (item)
            {
                if (isFirst) isFirst = false;
                else document.write(separator);
                document.write(selector(item));
            });
        },

        // Overload:function()
        // Overload:function(selector)
        WriteLine: function (selector)
        {
            selector = Utils.CreateLambda(selector);

            this.ForEach(function (item)
            {
                document.write(selector(item));
                document.write("<br />");
            });
        },

        Force: function ()
        {
            var eNumberator = this.GetENumberator();

            try { while (eNumberator.MoveNext()) { } }
            finally { Utils.Dispose(eNumberator); }
        },

        /* Functional Methods */

        Let: function (func)
        {
            func = Utils.CreateLambda(func);
            var source = this;

            return new ENumberable(function ()
            {
                var eNumberator;

                return new IENumberator(
                    function ()
                    {
                        eNumberator = ENumberable.From(func(source)).GetENumberator();
                    },
                    function ()
                    {
                        return (eNumberator.MoveNext())
                            ? this.Yield(eNumberator.Current())
                            : false;
                    },
                    function () { Utils.Dispose(eNumberator); })
            });
        },

        Share: function ()
        {
            var source = this;
            var sharedENumberator;

            return new ENumberable(function ()
            {
                return new IENumberator(
                    function ()
                    {
                        if (sharedENumberator == null)
                        {
                            sharedENumberator = source.GetENumberator();
                        }
                    },
                    function ()
                    {
                        return (sharedENumberator.MoveNext())
                            ? this.Yield(sharedENumberator.Current())
                            : false;
                    },
                    Functions.Blank
                )
            });
        },

        MemoizeAll: function ()
        {
            var source = this;
            var cache;
            var eNumberator;

            return new ENumberable(function ()
            {
                var index = -1;

                return new IENumberator(
                    function ()
                    {
                        if (eNumberator == null)
                        {
                            eNumberator = source.GetENumberator();
                            cache = [];
                        }
                    },
                    function ()
                    {
                        index++;
                        if (cache.length <= index)
                        {
                            return (eNumberator.MoveNext())
                                ? this.Yield(cache[index] = eNumberator.Current())
                                : false;
                        }

                        return this.Yield(cache[index]);
                    },
                    Functions.Blank
                )
            });
        },

        /* Error Handling Methods */

        Catch: function (handler)
        {
            handler = Utils.CreateLambda(handler);
            var source = this;

            return new ENumberable(function ()
            {
                var eNumberator;

                return new IENumberator(
                    function () { eNumberator = source.GetENumberator(); },
                    function ()
                    {
                        try
                        {
                            return (eNumberator.MoveNext())
                               ? this.Yield(eNumberator.Current())
                               : false;
                        }
                        catch (e)
                        {
                            handler(e);
                            return false;
                        }
                    },
                    function () { Utils.Dispose(eNumberator); });
            });
        },

        Finally: function (finallyAction)
        {
            finallyAction = Utils.CreateLambda(finallyAction);
            var source = this;

            return new ENumberable(function ()
            {
                var eNumberator;

                return new IENumberator(
                    function () { eNumberator = source.GetENumberator(); },
                    function ()
                    {
                        return (eNumberator.MoveNext())
                           ? this.Yield(eNumberator.Current())
                           : false;
                    },
                    function ()
                    {
                        try { Utils.Dispose(eNumberator); }
                        finally { finallyAction(); }
                    });
            });
        },

        /* For Debug Methods */

        // Overload:function()
        // Overload:function(message)
        // Overload:function(message,selector)
        Trace: function (message, selector)
        {
            if (message == null) message = "Trace";
            selector = Utils.CreateLambda(selector);

            return this.Do(function (item)
            {
                console.log(message, ":", selector(item));
            });
        }
    }

    // private

    // static functions
    var Functions =
    {
        Identity: function (x) { return x; },
        True: function () { return true; },
        Blank: function () { }
    }

    // static const
    var Types =
    {
        Boolean: typeof true,
        Number: typeof 0,
        String: typeof "",
        Object: typeof {},
        Undefined: typeof undefined,
        Function: typeof function () { }
    }

    // static utility methods
    var Utils =
    {
        // Create anonymous function from lambda expression string
        CreateLambda: function (expression)
        {
            if (expression == null) return Functions.Identity;
            if (typeof expression == Types.String)
            {
                if (expression == "")
                {
                    return Functions.Identity;
                }
                else if (expression.indexOf("=>") == -1)
                {
                    return new Function("$,$$,$$$,$$$$", "return " + expression);
                }
                else
                {
                    var expr = expression.match(/^[(\s]*([^()]*?)[)\s]*=>(.*)/);
                    return new Function(expr[1], "return " + expr[2]);
                }
            }
            return expression;
        },

        IsIENumberable: function (obj)
        {
            if (typeof ENumberator != Types.Undefined)
            {
                try
                {
                    new ENumberator(obj);
                    return true;
                }
                catch (e) { }
            }
            return false;
        },

        Compare: function (a, b)
        {
            return (a === b) ? 0
                : (a > b) ? 1
                : -1;
        },

        Dispose: function (obj)
        {
            if (obj != null) obj.Dispose();
        }
    }

    // IENumberator State
    var State = { Before: 0, Running: 1, After: 2 }

    // name "ENumberator" is conflict JScript's "ENumberator"
    var IENumberator = function (initialize, tryGetNext, dispose)
    {
        var yielder = new Yielder();
        var state = State.Before;

        this.Current = yielder.Current;
        this.MoveNext = function ()
        {
            try
            {
                switch (state)
                {
                    case State.Before:
                        state = State.Running;
                        initialize(); // fall through
                    case State.Running:
                        if (tryGetNext.apply(yielder))
                        {
                            return true;
                        }
                        else
                        {
                            this.Dispose();
                            return false;
                        }
                    case State.After:
                        return false;
                }
            }
            catch (e)
            {
                this.Dispose();
                throw e;
            }
        }
        this.Dispose = function ()
        {
            if (state != State.Running) return;

            try { dispose(); }
            finally { state = State.After; }
        }
    }

    // for tryGetNext
    var Yielder = function ()
    {
        var current = null;
        this.Current = function () { return current; }
        this.Yield = function (value)
        {
            current = value;
            return true;
        }
    }

    // for OrderBy/ThenBy

    var OrderedENumberable = function (source, keySelector, descending, parent)
    {
        this.source = source;
        this.keySelector = Utils.CreateLambda(keySelector);
        this.descending = descending;
        this.parent = parent;
    }
    OrderedENumberable.prototype = new ENumberable();

    OrderedENumberable.prototype.CreateOrderedENumberable = function (keySelector, descending)
    {
        return new OrderedENumberable(this.source, keySelector, descending, this);
    }

    OrderedENumberable.prototype.ThenBy = function (keySelector)
    {
        return this.CreateOrderedENumberable(keySelector, false);
    }

    OrderedENumberable.prototype.ThenByDescending = function (keySelector)
    {
        return this.CreateOrderedENumberable(keySelector, true);
    }

    OrderedENumberable.prototype.GetENumberator = function ()
    {
        var self = this;
        var buffer;
        var indexes;
        var index = 0;

        return new IENumberator(
            function ()
            {
                buffer = [];
                indexes = [];
                self.source.ForEach(function (item, index)
                {
                    buffer.push(item);
                    indexes.push(index);
                });
                var sortContext = SortContext.Create(self, null);
                sortContext.GenerateKeys(buffer);

                indexes.sort(function (a, b) { return sortContext.Compare(a, b); });
            },
            function ()
            {
                return (index < indexes.length)
                    ? this.Yield(buffer[indexes[index++]])
                    : false;
            },
            Functions.Blank
        )
    }

    var SortContext = function (keySelector, descending, child)
    {
        this.keySelector = keySelector;
        this.descending = descending;
        this.child = child;
        this.keys = null;
    }

    SortContext.Create = function (orderedENumberable, currentContext)
    {
        var context = new SortContext(orderedENumberable.keySelector, orderedENumberable.descending, currentContext);
        if (orderedENumberable.parent != null) return SortContext.Create(orderedENumberable.parent, context);
        return context;
    }

    SortContext.prototype.GenerateKeys = function (source)
    {
        var len = source.length;
        var keySelector = this.keySelector;
        var keys = new Array(len);
        for (var i = 0; i < len; i++) keys[i] = keySelector(source[i]);
        this.keys = keys;

        if (this.child != null) this.child.GenerateKeys(source);
    }

    SortContext.prototype.Compare = function (index1, index2)
    {
        var comparison = Utils.Compare(this.keys[index1], this.keys[index2]);

        if (comparison == 0)
        {
            if (this.child != null) return this.child.Compare(index1, index2)
            comparison = Utils.Compare(index1, index2);
        }

        return (this.descending) ? -comparison : comparison;
    }

    // optimize array or arraylike object

    var ArrayENumberable = function (source)
    {
        this.source = source;
    }
    ArrayENumberable.prototype = new ENumberable();

    ArrayENumberable.prototype.Any = function (predicate)
    {
        return (predicate == null)
            ? (this.source.length > 0)
            : ENumberable.prototype.Any.apply(this, arguments);
    }

    ArrayENumberable.prototype.Count = function (predicate)
    {
        return (predicate == null)
            ? this.source.length
            : ENumberable.prototype.Count.apply(this, arguments);
    }

    ArrayENumberable.prototype.ElementAt = function (index)
    {
        return (0 <= index && index < this.source.length)
            ? this.source[index]
            : ENumberable.prototype.ElementAt.apply(this, arguments);
    }

    ArrayENumberable.prototype.ElementAtOrDefault = function (index, defaultValue)
    {
        return (0 <= index && index < this.source.length)
            ? this.source[index]
            : defaultValue;
    }

    ArrayENumberable.prototype.First = function (predicate)
    {
        return (predicate == null && this.source.length > 0)
            ? this.source[0]
            : ENumberable.prototype.First.apply(this, arguments);
    }

    ArrayENumberable.prototype.FirstOrDefault = function (defaultValue, predicate)
    {
        if (predicate != null)
        {
            return ENumberable.prototype.FirstOrDefault.apply(this, arguments);
        }

        return this.source.length > 0 ? this.source[0] : defaultValue;
    }

    ArrayENumberable.prototype.Last = function (predicate)
    {
        return (predicate == null && this.source.length > 0)
            ? this.source[this.source.length - 1]
            : ENumberable.prototype.Last.apply(this, arguments);
    }

    ArrayENumberable.prototype.LastOrDefault = function (defaultValue, predicate)
    {
        if (predicate != null)
        {
            return ENumberable.prototype.LastOrDefault.apply(this, arguments);
        }

        return this.source.length > 0 ? this.source[this.source.length - 1] : defaultValue;
    }

    ArrayENumberable.prototype.Skip = function (count)
    {
        var source = this.source;

        return new ENumberable(function ()
        {
            var index;

            return new IENumberator(
                function () { index = (count < 0) ? 0 : count },
                function ()
                {
                    return (index < source.length)
                        ? this.Yield(source[index++])
                        : false;
                },
                Functions.Blank);
        });
    };

    ArrayENumberable.prototype.TakeExceptLast = function (count)
    {
        if (count == null) count = 1;
        return this.Take(this.source.length - count);
    }

    ArrayENumberable.prototype.TakeFromLast = function (count)
    {
        return this.Skip(this.source.length - count);
    }

    ArrayENumberable.prototype.Reverse = function ()
    {
        var source = this.source;

        return new ENumberable(function ()
        {
            var index;

            return new IENumberator(
                function ()
                {
                    index = source.length;
                },
                function ()
                {
                    return (index > 0)
                        ? this.Yield(source[--index])
                        : false;
                },
                Functions.Blank)
        });
    }

    ArrayENumberable.prototype.SequenceEqual = function (second, compareSelector)
    {
        if ((second instanceof ArrayENumberable || second instanceof Array)
            && compareSelector == null
            && ENumberable.From(second).Count() != this.Count())
        {
            return false;
        }

        return ENumberable.prototype.SequenceEqual.apply(this, arguments);
    }

    ArrayENumberable.prototype.ToString = function (separator, selector)
    {
        if (selector != null || !(this.source instanceof Array))
        {
            return ENumberable.prototype.ToString.apply(this, arguments);
        }

        if (separator == null) separator = "";
        return this.source.join(separator);
    }

    ArrayENumberable.prototype.GetENumberator = function ()
    {
        var source = this.source;
        var index = 0;

        return new IENumberator(
            Functions.Blank,
            function ()
            {
                return (index < source.length)
                    ? this.Yield(source[index++])
                    : false;
            },
            Functions.Blank);
    }

    // Collections

    var Dictionary = (function ()
    {
        // static utility methods
        var HasOwnProperty = function (target, key)
        {
            return Object.prototype.hasOwnProperty.call(target, key);
        }

        var ComputeHashCode = function (obj)
        {
            if (obj === null) return "null";
            if (obj === undefined) return "undefined";

            return (typeof obj.toString === Types.Function)
                ? obj.toString()
                : Object.prototype.toString.call(obj);
        }

        // LinkedList for Dictionary
        var HashEntry = function (key, value)
        {
            this.Key = key;
            this.Value = value;
            this.Prev = null;
            this.Next = null;
        }

        var EntryList = function ()
        {
            this.First = null;
            this.Last = null;
        }
        EntryList.prototype =
        {
            AddLast: function (entry)
            {
                if (this.Last != null)
                {
                    this.Last.Next = entry;
                    entry.Prev = this.Last;
                    this.Last = entry;
                }
                else this.First = this.Last = entry;
            },

            Replace: function (entry, newEntry)
            {
                if (entry.Prev != null)
                {
                    entry.Prev.Next = newEntry;
                    newEntry.Prev = entry.Prev;
                }
                else this.First = newEntry;

                if (entry.Next != null)
                {
                    entry.Next.Prev = newEntry;
                    newEntry.Next = entry.Next;
                }
                else this.Last = newEntry;

            },

            Remove: function (entry)
            {
                if (entry.Prev != null) entry.Prev.Next = entry.Next;
                else this.First = entry.Next;

                if (entry.Next != null) entry.Next.Prev = entry.Prev;
                else this.Last = entry.Prev;
            }
        }

        // Overload:function()
        // Overload:function(compareSelector)
        var Dictionary = function (compareSelector)
        {
            this.count = 0;
            this.entryList = new EntryList();
            this.buckets = {}; // as Dictionary<string,List<object>>
            this.compareSelector = (compareSelector == null) ? Functions.Identity : compareSelector;
        }

        Dictionary.prototype =
        {
            Add: function (key, value)
            {
                var compareKey = this.compareSelector(key);
                var hash = ComputeHashCode(compareKey);
                var entry = new HashEntry(key, value);
                if (HasOwnProperty(this.buckets, hash))
                {
                    var array = this.buckets[hash];
                    for (var i = 0; i < array.length; i++)
                    {
                        if (this.compareSelector(array[i].Key) === compareKey)
                        {
                            this.entryList.Replace(array[i], entry);
                            array[i] = entry;
                            return;
                        }
                    }
                    array.push(entry);
                }
                else
                {
                    this.buckets[hash] = [entry];
                }
                this.count++;
                this.entryList.AddLast(entry);
            },

            Get: function (key)
            {
                var compareKey = this.compareSelector(key);
                var hash = ComputeHashCode(compareKey);
                if (!HasOwnProperty(this.buckets, hash)) return undefined;

                var array = this.buckets[hash];
                for (var i = 0; i < array.length; i++)
                {
                    var entry = array[i];
                    if (this.compareSelector(entry.Key) === compareKey) return entry.Value;
                }
                return undefined;
            },

            Set: function (key, value)
            {
                var compareKey = this.compareSelector(key);
                var hash = ComputeHashCode(compareKey);
                if (HasOwnProperty(this.buckets, hash))
                {
                    var array = this.buckets[hash];
                    for (var i = 0; i < array.length; i++)
                    {
                        if (this.compareSelector(array[i].Key) === compareKey)
                        {
                            var newEntry = new HashEntry(key, value);
                            this.entryList.Replace(array[i], newEntry);
                            array[i] = newEntry;
                            return true;
                        }
                    }
                }
                return false;
            },

            Contains: function (key)
            {
                var compareKey = this.compareSelector(key);
                var hash = ComputeHashCode(compareKey);
                if (!HasOwnProperty(this.buckets, hash)) return false;

                var array = this.buckets[hash];
                for (var i = 0; i < array.length; i++)
                {
                    if (this.compareSelector(array[i].Key) === compareKey) return true;
                }
                return false;
            },

            Clear: function ()
            {
                this.count = 0;
                this.buckets = {};
                this.entryList = new EntryList();
            },

            Remove: function (key)
            {
                var compareKey = this.compareSelector(key);
                var hash = ComputeHashCode(compareKey);
                if (!HasOwnProperty(this.buckets, hash)) return;

                var array = this.buckets[hash];
                for (var i = 0; i < array.length; i++)
                {
                    if (this.compareSelector(array[i].Key) === compareKey)
                    {
                        this.entryList.Remove(array[i]);
                        array.splice(i, 1);
                        if (array.length == 0) delete this.buckets[hash];
                        this.count--;
                        return;
                    }
                }
            },

            Count: function ()
            {
                return this.count;
            },

            ToENumberable: function ()
            {
                var self = this;
                return new ENumberable(function ()
                {
                    var currentEntry;

                    return new IENumberator(
                        function () { currentEntry = self.entryList.First },
                        function ()
                        {
                            if (currentEntry != null)
                            {
                                var result = { Key: currentEntry.Key, Value: currentEntry.Value };
                                currentEntry = currentEntry.Next;
                                return this.Yield(result);
                            }
                            return false;
                        },
                        Functions.Blank);
                });
            }
        }

        return Dictionary;
    })();

    // dictionary = Dictionary<TKey, TValue[]>
    var Lookup = function (dictionary)
    {
        this.Count = function ()
        {
            return dictionary.Count();
        }

        this.Get = function (key)
        {
            return ENumberable.From(dictionary.Get(key));
        }

        this.Contains = function (key)
        {
            return dictionary.Contains(key);
        }

        this.ToENumberable = function ()
        {
            return dictionary.ToENumberable().Select(function (kvp)
            {
                return new Grouping(kvp.Key, kvp.Value);
            });
        }
    }

    var Grouping = function (key, elements)
    {
        this.Key = function ()
        {
            return key;
        }

        ArrayENumberable.call(this, elements);
    }
    Grouping.prototype = new ArrayENumberable();

    // out to global
    return ENumberable;
})()});

// binding for jQuery
// toENumberable / TojQuery

(function ($, ENumberable)
{
    $.fn.toENumberable = function ()
    {
        /// <summary>each contains elements. to ENumberable&lt;jQuery&gt;.</summary>
        /// <returns type="ENumberable"></returns>
        return ENumberable.From(this).Select(function (e) { return $(e) });
    }

    ENumberable.prototype.TojQuery = function ()
    {
        /// <summary>ENumberable to jQuery.</summary>
        /// <returns type="jQuery"></returns>
        return $(this.ToArray());
    }
})(jQuery, this.ENumberable || this.jQuery.ENumberable)
