using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RegulareExp
{
    class Program
    {
        static void Main(string[] args)
        {

            var value = @"apiVersion: v1
clusters:
- cluster:
    certificate-authority-data: LS0tLS1CRUdJTiBDRVJUSUZJQ0FURS0tLS0tCk1JSUV4ekNDQXErZ0F3SUJBZ0lRUGIvRU03R0tSNU1sMmIzSFZlbXgrVEFOQmdrcWhraUc5dzBCQVFzRkFEQU4KTVFzd0NRWURWUVFERXdKallUQWVGdzB4T1RBMk1UQXdOelF3TkRsYUZ3MDBPVEEyTURJd056VXdORGxhTUEweApDekFKQmdOVkJBTVRBbU5oTUlJQ0lqQU5CZ2txaGtpRzl3MEJBUUVGQUFPQ0FnOEFNSUlDQ2dLQ0FnRUF1Q1BHCmVGdEdEemZjSzFVV2hFcm9qTkVKcDR4VlZ5U0svalRJU0NTTm84SGJXQ3RQcFEvbGgwMDQzbFRqVVVzWWJBanIKVDVWbEZobmpTbGZUeWl4ME9FT2pCWUhKMmM3K0FlbnU1S3FvekRDei8vMXlCMW9nQzl5M2ZWRzhubTJaV2FmcApHQVVpNzhGTHpTQ2lvclVmVksxc0tKd0RVcG5DT1JrQ1lUL044YkRlMWczU3Z4VDhyM1o5OGxldU4vREwwS0hLCkVjTWUvUk5pSjl1aGx6MkZ5ZXZOR3VIZ1k2YWQvc2tVV2IwQkNrNFk4cHFXTDdqdnZMK2tieWZIY1oxVS9LT0UKbmI1UzdHcTdtdGdRT3kwb25SUHJXLy81NEhzMmdDR1A3TUQwTW1nSXpWZ3ovdjJjNEtZWGFySDV4c05vRk9xUgpXMmVod0xJZENDNHAxL1M4VnVaQnpYRHp3YVdNbXQ5RzVnbmtqTWRnZ3pFR1BCdXRUWDUwajZPS0V0NDY3Z2FhClBXWUg5UnV5OERXa3ZwQ1FGQzUzR3RxaEsrUjZhbHd4ejk2dmllT0l4V2lsUWNQdE9QNE5ZNEMxSXRxYTMzbkoKbE4yZDNHeW9wMHZGdWZjaURJN1BJTUpOUGJPeFdMaGdVeFVtdzFqS0pPaEVUbUE3YWxyYmk2LzVBdFJhdk1waApIUU52bCtUeTZOUDFoM0lxR0EzNElYcy9ma0NSMW1EeFY3dGlTTEdEMG9YYXMxTEVPa1ZjY1FQS25obkpBNWRBCmcyYVFZL3p0ajJVSkkvWmg3OGRxQW5kUGdWdnJBVEJFampZQk9qNndVc3d5SmllRWlJQ2dmcHQyODR4bzFCUUkKMkg5SGpYUkR0T1ZwY2tnTElucmJGQnF3NHBxQW12VW5INDhjZGlVQ0F3RUFBYU1qTUNFd0RnWURWUjBQQVFILwpCQVFEQWdLa01BOEdBMVVkRXdFQi93UUZNQU1CQWY4d0RRWUpLb1pJaHZjTkFRRUxCUUFEZ2dJQkFJVVAwMXZwCnZXdElKUnBEOGtJQW1SdUpvcWVITWRUZGgrZlh3Y25Kdk5SZitMQ2Q2Rzd3TUlhNmdibkVlMTJiK1NnVUlEK2wKeFV0THlQdFdNaTl3R1AvbGdDZ0hER04ybi82QjI0YUM3SnFlZzRrNFRwQllNc1FSYmFFREhNTXdYRUhFVmV3NQpvc0xHSmhjdUU0QldwbjdVTHBqVnZkTzMwdXBJS3RJWlpadjhYRnRyNngvYmhuaXBEenpJcjJyNVRteGVPUGZxClkyZThyU3c5MVIwb2hjZUxkT2R6QUhuajF6SFZCeSt4MGY1V0pjeWVNVUw1VjJKbERyNTI3ZDEzeStIZjdUY2IKUHBYUG53cXhzcXB5aUJhdFZrYmcwaytHZWVYZ0sxdnUvcUlOS3VIdFd2cFZJVmRMZS9EcytVdXRYLzRib2tSMgpIWnVTWnc2aDY0K0MxVWZJR2lXZE9HZXZXcU56OEpqbWFyL0h1aTNGNFJjcVRQYkRVaWgvWWJ1VzZ2bXlTaW9jCitMODRkMCtNWmpkZ0hneFJjQUFhUTNyR1BaV084S05XTVR0bGNEYVdlcXlQVlRyL1FKZ0xIYXdQU28ranoxNGQKdElaa3FPWVFFSUhmSkZkWTNjYkRNWVhlc1ZKWHdDZU91eE85M3VlTUN0VWMxOUZlL1puZlZNZmtBVURyV1IrZApHQnRMTEFYSUxSVUpGK0RSZkpoaTR6ZkdGTXpLcy90QWt2WktKeDJ2ZSt0cWVUUEFBTXhmS01OU1NJemJScFlXCmhyOVFERUJ6QVFvbjFkVDBDRlNheXdETEpsVWVrVi9kbDBKYzFyeVdvZnplKzNTZ3BJWnhLR0hWS1RFNUF5TDEKOU40a05wTzFCdGhiNDJZYTRnWWhpK0x5bWVpcGNhMTMvRWxFCi0tLS0tRU5EIENFUlRJRklDQVRFLS0tLS0K
    server: https://rndaksclus-rndakscluster-9020de-8816d6cc.hcp.northeurope.azmk8s.io:443
  name: RndAKSCluster
- cluster:
    certificate-authority-data: LS0tLS1CRUdJTiBDRVJUSUZJQ0FURS0tLS0tCk1JSUV4ekNDQXErZ0F3SUJBZ0lRVzl3QmxVaVNzTk13TlVOV3RTaXRQREFOQmdrcWhraUc5dzBCQVFzRkFEQU4KTVFzd0NRWURWUVFERXdKallUQWVGdzB4T1RBMk1UUXhNakU1TVRWYUZ3MDBPVEEyTURZeE1qSTVNVFZhTUEweApDekFKQmdOVkJBTVRBbU5oTUlJQ0lqQU5CZ2txaGtpRzl3MEJBUUVGQUFPQ0FnOEFNSUlDQ2dLQ0FnRUF4YmpNCnRzMTFnS1BLQjY0V0NOUnB3OUhkOStWVFJJRWx3ZlR4bVNtV3ZNUThLbnIyVFN3bzNvT1dBSTU4M0k3VUpHNHoKc2c0Q3hJazNsaUQ2VkpCQUsyMERrWmFHR0hrN1pBM2N3WWloVkt4QmhpNXV5VVpvUmRwbW0wTmI2alZBK25OSwpwN3VTUW8vNWtmSkhTdXhXZ3gyY21rK3pXYzg0a2FmVUhraHVrWE95VGNzWEdvRzJxM0hVVTExeFZPdUp5dHVzCmg2NWExdmN5MUdxeGxUa1o1K2NETjNhRk9aWE9ONzZaTG42Wm5ZTk1xVGJCTEpFUno0bzhUT05xdStidnhRNUwKKzhUYW9LVkp5S0hWQjJ1OWdvd21UazdPNXE3NHV1M0FqRWNRN0Q2am8zRG9YTm8zVzRoVkhtSnZ1dVhjdy9rUApRODBvSG9ST0M1SmtVQW9ibW5OeTNYeDlWQjljVDNWb3FTS2ZtL3g2ZlFwNlV2eHEwa21CV3NKZXlsc3VTaHpVCkwzQlErc1pRVEtVd2JnS0o0QXU2ZXk1dXFjRHN0SXR6VnYwazBYQTArSWxTMHoxcllWVEY2aW9MMW41dDg5TkwKRWxmaWV1R2FtY0EvTHN5VmNvbDF0bm5jN3ZvVUxYU1R5aWVucnJlcGtWZm0vbDhCZG9MS2NBMFdsOWRsN3dtVwpHbDZOVHNsUWRBS3BBTFNIdHhpVk9iQmNFSFI2RndTU2dPQWwyZ0ZxWFBFMzNJNlZwcmRDZXppd1JGWm84aHRDCkJGbi9qVVpWeTRLTWZ0dmlleS91ditlamZZanlKQ3I1ZHdLOXg5Rks2ZFNuZzF1SUlKeVBzSHFRemYrQ0ZFSmkKY2VHaTcvN2NGSHBIbzZYR1dmczhZUjNtZDBYaWxUdlFvaE1sTTFVQ0F3RUFBYU1qTUNFd0RnWURWUjBQQVFILwpCQVFEQWdLa01BOEdBMVVkRXdFQi93UUZNQU1CQWY4d0RRWUpLb1pJaHZjTkFRRUxCUUFEZ2dJQkFGbWNUSW9hClJVWXc3RFlIR1R0UVh3RjBEZS9pUzdiS2ZNb0JuMFhBbHJkWm53ZjBtR0t3MU1VVFlZejA3Y2tMVThWZmNFYnkKTEVRRFZWY3MzaHBiMTVhK2QvU3MrRS95Q1RmMUtEYkRLSGRxL2dELzRzQjVINFFEWmc5b0l6YUFaS1I5T0RyQQpyUlh6dFRJVXZqaHR3Z1l3SVBlTkplQjBvLzNCOFBrby9kcnhJcFlSZ3pJenNKcHFUbXZ1M2dTdTJnbHB1OThQCnZ1TmVKekEvMks0K3UvZ2dKQ243V2t3MzRQNmErODg5dkhrZndFc1JkZms3MStTMmlWN09tUlVPSnVGNGNINXgKMm5Pc2VuWFIwWlRnNzVybStDWW5XUklIRXdIWjNaV3JLbDFJOXcwcTBXank4cjl0Mm90YUNoMmlzTk1UZGs1NwphKzJXS1hqRG83dVJJUCtQU3dib1h0dkJLM3VXakMxNk5jb0owbWdMQWFPaCt1MS9CcTREZVpUNldHUHNmUXNDClQ0dnNvcW9VMEhKWmVKSGV0QlYwNkg2aEIySlFxUzhGVjBaRGVpKzh1UmpYM3RSS0JjNWpvTHFjUDlSUHB0UGYKQmJJa0RJd0g3NzAraGFRZHVUenpLemYxamlwYklaZXAvRi9wYnlpcFMrTFZCNFNGLzJkUXNGS080VlJCVE1kUwplaXM3YTU5bVk3UDlZUml6NS9GU0VUNTlFa3ZLYWtkSExSMHhEbVBNNmN3TkR5bXlLQU9uakgvZXEvMzVMcVk1CkgxcnpnajdndVVzMnZGTnVGRFRRZVRiUVNYcTFLbWNzR3JkRXNOQW9PZEhiS2FDOUZkZzRlYklVa1JUc1plMmwKZmNLUnp1akx0bzRHcWpUNEpLUU9UWm9zZkE0enA3eGhXUVc3Ci0tLS0tRU5EIENFUlRJRklDQVRFLS0tLS0K
    server: https://asadaksclu-asadaksclusterrg-8b2ebf-994659db.hcp.eastus.azmk8s.io:443
  name: AsadAKSCluster";




            var pattern = @"name:(.*)";
            MatchCollection matches = Regex.Matches(value, pattern);


            // The code provided will print ‘Hello World’ to the console.
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
            Console.WriteLine("Hello World!");
            Console.ReadKey();

            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
        }
    }
}
