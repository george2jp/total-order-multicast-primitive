msbuild Middleware1\Middleware_1.sln
msbuild Middleware2\Middleware_2.sln
msbuild Middleware3\Middleware_3.sln
msbuild Middleware4\Middleware_4.sln
msbuild Middleware5\Middleware_5.sln
csc /out:Network\Network.exe Network\Network.cs 
start Network\Network.exe
start Middleware1\Middleware1\bin\Debug\Middleware1.exe
start Middleware2\Middleware2\bin\Debug\Middleware2.exe
start Middleware3\Middleware3\bin\Debug\Middleware3.exe
start Middleware4\Middleware4\bin\Debug\Middleware4.exe
start Middleware5\Middleware5\bin\Debug\Middleware5.exe