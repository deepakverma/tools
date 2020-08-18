xperf.exe -start AMXperf -on e4b70372-261f-4c54-8fa6-a5a7914d73da+cfeb0608-330e-4410-b00d-56d8da9986e6+8e92deef-5e17-413b-b927-59b2f06a3cfc+751ef305-6c6e-4fed-b847-02ef79d26aef+0A002690-3839-4E3A-B3B6-96D8DF868D99  -f amxperftrace.etl
timeout 20
xperf.exe -stop -stop AMXperf -d Merged.etl
echo "done"
