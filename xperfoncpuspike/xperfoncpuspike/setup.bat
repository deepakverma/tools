xperf.exe -on Base+Diag+Latency+FileIO+DISPATCHER+REGISTRY+PERF_COUNTER -stackWalk %STACK% -f Kernel.etl -BufferSize 1024 2> out.txt
