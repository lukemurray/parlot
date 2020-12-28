﻿using System;

namespace Parlot.Fluent
{
    public sealed class DecimalLiteral : Parser<decimal>
    {
        private readonly bool _skipWhiteSpace;

        public DecimalLiteral(bool skipWhiteSpace = true)
        {
            _skipWhiteSpace = skipWhiteSpace;
        }

        public override bool Parse(Scanner scanner, ref ParseResult<decimal> result)
        {
            if (_skipWhiteSpace)
            {
                scanner.SkipWhiteSpace();
            }

            var start = scanner.Cursor.Position;

            if (scanner.ReadDecimal())
            {
                var end = scanner.Cursor.Position;

                if (decimal.TryParse(scanner.Buffer.AsSpan(start.Offset, end - start), out var value))
                {
                    result.Set(scanner.Buffer, start, end, value);
                    return true;
                }
            }
         
            return false;
        }
    }
}
