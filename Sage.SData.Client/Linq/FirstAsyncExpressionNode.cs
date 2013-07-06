﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Remotion.Linq.Clauses;
using Remotion.Linq.Parsing.Structure.IntermediateModel;

namespace Sage.SData.Client.Linq
{
    internal class FirstAsyncExpressionNode : FirstExpressionNode
    {
        public new static readonly MethodInfo[] SupportedMethods =
            new[]
                {
                    new Func<IQueryable<object>, Task<object>>(SDataQueryableExtensions.FirstAsync).Method.GetGenericMethodDefinition(),
                    new Func<IQueryable<object>, Task<object>>(SDataQueryableExtensions.FirstOrDefaultAsync).Method.GetGenericMethodDefinition(),
                    new Func<IQueryable<object>, Expression<Func<object, bool>>, Task<object>>(SDataQueryableExtensions.FirstAsync).Method.GetGenericMethodDefinition(),
                    new Func<IQueryable<object>, Expression<Func<object, bool>>, Task<object>>(SDataQueryableExtensions.FirstOrDefaultAsync).Method.GetGenericMethodDefinition()
                };

        public FirstAsyncExpressionNode(MethodCallExpressionParseInfo parseInfo, LambdaExpression optionalPredicate)
            : base(parseInfo, optionalPredicate)
        {
        }

        protected override ResultOperatorBase CreateResultOperator(ClauseGenerationContext clauseGenerationContext)
        {
            return new FirstAsyncResultOperator(ParsedExpression.Method.Name.EndsWith("OrDefaultAsync"));
        }
    }
}