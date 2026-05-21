import pyodbc
import sys

sys.stdout.reconfigure(encoding='utf-8')

conn = pyodbc.connect(r'DRIVER={Microsoft Access Driver (*.mdb, *.accdb)};DBQ=C:\temp\Bearing.mdb;')
cursor = conn.cursor()

def dump_all(name):
    cursor.execute(f"SELECT * FROM [{name}]")
    cols = [d[0] for d in cursor.description]
    print(f"=== {name} === ({len(cols)} cols)")
    print('|'.join(cols))
    count = 0
    for row in cursor.fetchall():
        vals = [str(v) if v is not None else '' for v in row]
        print('|'.join(vals))
        count += 1
    print(f"Total: {count}")

dump_all('深沟球轴承')
dump_all('角接触球轴承')
dump_all('圆锥滚子轴承')
dump_all('推力球轴承')
dump_all('推力滚子轴承')
dump_all('调心球轴承')

conn.close()
