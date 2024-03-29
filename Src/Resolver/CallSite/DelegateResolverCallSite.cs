﻿using FS.DI.Core;
using System;
using System.Linq.Expressions;

namespace FS.DI.Resolver.CallSite
{
    /// <summary>
    /// 委托解析器调用
    /// </summary>
    internal sealed class DelegateResolverCallSite : IResolverCallSite
    {
        public bool PreResolver(IResolverContext context, IDependencyResolver resolver)
        {
            return context.NotComplete() && context.HasImplementationDelegate();
        }

        public void Resolver(IResolverContext context, IDependencyResolver resolver)
        {
            var parameter = Expression.Parameter(typeof(IDependencyResolver), "resolver");           
            var factory = Expression.Invoke(
                     Expression.Constant(context.DependencyEntry.ImplementationDelegate,
                     typeof(Func<IDependencyResolver, Object>)),
                     parameter);
            context.CompleteValue = factory;
        }
    }
}
