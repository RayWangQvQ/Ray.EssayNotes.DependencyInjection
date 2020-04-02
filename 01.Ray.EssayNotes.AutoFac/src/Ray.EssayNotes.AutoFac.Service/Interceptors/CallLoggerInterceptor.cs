using System.IO;
using System.Linq;
using Castle.DynamicProxy;

namespace Ray.EssayNotes.AutoFac.Service.Interceptors
{
    public class CallLoggerInterceptor : Castle.DynamicProxy.IInterceptor
    {
        private readonly TextWriter _output;

        public CallLoggerInterceptor(TextWriter output)
        {
            _output = output;
        }

        public void Intercept(IInvocation invocation)
        {
            //进入被拦截对象的方法前，做些什么
            string paras = string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()).ToArray());
            _output.WriteLine($"Calling method 【{invocation.Method.Name}】 with parameters 【{paras}】... ");

            //调用目标
            invocation.Proceed();

            //目标执行后，做些什么
            _output.WriteLine("Done: result was 【{0}】.", invocation.ReturnValue);
        }
    }
}
