using JangBoGo.Info.Object;
using Spring.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TqoonFranchise.Model;
using TqoonFranchise.Service;
using Yusurun.Spring.Core;

namespace TqoonFranchise
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //args[0] model joiner id
            //args[1] target database name
            //args[2] target joiner id

            if (!argsValidate(args))
            {
                Console.WriteLine("파라메터 불일치:" + args.ToString());
                return;
            }
            
            CreateService service = ContextManager.GetContextObject("CreateService") as CreateService;
            SetTargetDbProvider(args[1]);
            service.DoIt(GetFranchiseParam(args));
        }

        private static void SetTargetDbProvider(string databaseName)
        {
            IDbProvider dbProvider = ContextManager.GetContextObject("TargetDbProvider") as IDbProvider;
            dbProvider.ConnectionString = "";
        }

        private static FranchiseParam GetFranchiseParam(string[] args)
        {            
            return new FranchiseParam
            {
                ModelJoinerId = args[0].ParseInt()
            ,
                TargetJoinerId = args[2].ParseInt()
            };
        }
        private static bool argsValidate(string[] args)
        {
            if (args.Length != 3) return false;
            if (!args[0].IsNumeric()) return false;
            if (!args[2].IsNumeric()) return false;
            return true;
        }
    }
}
