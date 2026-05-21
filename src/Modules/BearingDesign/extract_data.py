import pyodbc
import sys

sys.stdout.reconfigure(encoding='utf-8')

conn = pyodbc.connect(r'DRIVER={Microsoft Access Driver (*.mdb, *.accdb)};DBQ=C:\temp\Bearing.mdb;')
cursor = conn.cursor()

def read_table(name, top=0):
    print(f"\n=== {name} ===")
    if top > 0:
        sql = f"SELECT TOP {top} * FROM [{name}]"
    else:
        sql = f"SELECT * FROM [{name}]"
    cursor.execute(sql)
    cols = [d[0] for d in cursor.description]
    print('|'.join(cols))
    count = 0
    for row in cursor.fetchall():
        vals = [str(v) if v is not None else '' for v in row]
        print('|'.join(vals))
        count += 1
    print(f"Total: {count}")

# Get table names
tables = [t[2] for t in cursor.tables() if t[2] not in ['MSysAccessObjects','MSysAccessXML','MSysACEs','MSysObjects','MSysQueries','MSysRelationships']]
print("=== ALL TABLES ===")
for t in tables:
    print(t)

# Read all bearing tables
bearing_tables = [t for t in tables if t.startswith('圆') or t.startswith('深') or t.startswith('角') or t.startswith('推') or t.startswith('调')]
print(f"\nBearing tables found: {bearing_tables}")

# Read each table
for t in bearing_tables:
    read_table(t, 60)

# Read coefficient tables
for t in ['SGQXY', 'JQa15XY', 'JQa10XY', 'JQa5XY', 'JQ20a45XY', 'TCJXY', 'TLGXY', 'TLQXY']:
    read_table(t)

conn.close()
