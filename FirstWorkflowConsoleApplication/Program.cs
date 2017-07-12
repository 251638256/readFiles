using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Collections;

namespace FirstWorkflowConsoleApplication {

    class Program {
        static void Main(string[] args) {
            Activity workflow1 = new Workflow1();
            Dictionary<string, object> wfArgs = new Dictionary<string, object>();
            wfArgs.Add("MessageToShow", "WorkFlow Arg");
            WorkflowInvoker.Invoke(workflow1,wfArgs);
            Console.WriteLine("The End");
        }
    }
}
