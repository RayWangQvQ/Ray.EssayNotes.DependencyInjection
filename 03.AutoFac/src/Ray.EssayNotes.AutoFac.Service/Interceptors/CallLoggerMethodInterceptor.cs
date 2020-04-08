using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Castle.DynamicProxy;

namespace Ray.EssayNotes.AutoFac.Service.Interceptors
{
    public class CallLoggerMethodInterceptor : Castle.DynamicProxy.IInterceptor
    {
        private readonly TextWriter _output;

        public CallLoggerMethodInterceptor(TextWriter output)
        {
            _output = output;
        }

        public void Intercept(IInvocation invocation)
        {
            MethodInfo mi = invocation.MethodInvocationTarget
                            ?? invocation.Method;

            //如果目标函数没有添加指定的特性，则直接执行目标函数
            if (mi.GetCustomAttributes<CallLoggerAttribute>(true).FirstOrDefault() == null)
            {
                invocation.Proceed();
                return;
            }

            //如果添加了指定的特性，则执行拦截
            try
            {
                PrintRequestInfo(invocation);
                invocation.Proceed();
                PrintResponseInfo(invocation);
            }
            catch (System.Exception ex)
            {
                //todo:记录日志
                throw;
            }
        }

        /// <summary>
        /// 打印对目标函数的请求信息
        /// </summary>
        /// <param name="invocation"></param>
        private void PrintRequestInfo(IInvocation invocation)
        {
            string paras = string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()).ToArray());
            _output.WriteLine($"Calling method 【{invocation.Method.Name}】 with parameters 【{paras}】... ");
        }

        /// <summary>
        /// 打印目标函数返回结果
        /// </summary>
        /// <param name="invocation"></param>
        private void PrintResponseInfo(IInvocation invocation)
        {
            _output.WriteLine("Done: result was 【{0}】.", invocation.ReturnValue);
        }
    }
}
