using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Entities.Services
{
    // ContinueOnScope extension
    public static class TaskExt
    {
        public static SimpleAwaiter<TResult> ContinueOnScope<TResult>(this Task<TResult> @this, FlowingOperationContextScope scope)
        {
            return new SimpleAwaiter<TResult>(@this, scope.BeforeAwait, scope.AfterAwait);
        }

        // awaiter
        public class SimpleAwaiter<TResult> :
            System.Runtime.CompilerServices.INotifyCompletion
        {
            readonly Task<TResult> _task;

            readonly Action _beforeAwait;
            readonly Action _afterAwait;

            public SimpleAwaiter(Task<TResult> task, Action beforeAwait, Action afterAwait)
            {
                _task = task;
                _beforeAwait = beforeAwait;
                _afterAwait = afterAwait;
            }

            public SimpleAwaiter<TResult> GetAwaiter()
            {
                return this;
            }

            public bool IsCompleted
            {
                get
                {
                    // don't do anything if the task completed synchronously
                    // (we're on the same thread)
                    if (_task.IsCompleted)
                        return true;
                    _beforeAwait();
                    return false;
                }

            }

            public TResult GetResult()
            {
                return _task.Result;
            }

            // INotifyCompletion
            public void OnCompleted(Action continuation)
            {
                _task.ContinueWith(task =>
                {
                    _afterAwait();
                    continuation();
                },
                CancellationToken.None,
                TaskContinuationOptions.ExecuteSynchronously,
                SynchronizationContext.Current != null ?
                    TaskScheduler.FromCurrentSynchronizationContext() :
                    TaskScheduler.Current);
            }
        }
    }
}
