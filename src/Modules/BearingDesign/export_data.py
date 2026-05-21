import pyodbc
import sys
import os

sys.stdout.reconfigure(encoding='utf-8')

conn = pyodbc.connect(r'DRIVER={Microsoft Access Driver (*.mdb, *.accdb)};DBQ=C:\temp\Bearing.mdb;')
cursor = conn.cursor()

out_dir = r'C:\Users\郭一彬\Documents\Codex\麦豆宝源码\src\Modules\BearingDesign\data'
os.makedirs(out_dir, exist_ok=True)

def export_table(name, filename):
    cursor.execute(f"SELECT * FROM [{name}]")
    cols = [d[0] for d in cursor.description]
    with open(os.path.join(out_dir, filename), 'w', encoding='utf-8') as f:
        f.write('|'.join(cols) + '\n')
        for row in cursor.fetchall():
            vals = [str(v) if v is not None else '' for v in row]
            f.write('|'.join(vals) + '\n')
    print(f"Exported {name} -> {filename}")

export_table('深沟球轴承', 'deep_groove.txt')
export_table('角接触球轴承', 'angular_contact.txt')
export_table('圆锥滚子轴承', 'tapered_roller.txt')
export_table('推力球轴承', 'thrust_ball.txt')
export_table('推力滚子轴承', 'thrust_roller.txt')
export_table('调心球轴承', 'self_aligning.txt')
export_table('SGQXY', 'sgqxy.txt')
export_table('JQa5XY', 'jqa5xy.txt')
export_table('JQa10XY', 'jqa10xy.txt')
export_table('JQa15XY', 'jqa15xy.txt')
export_table('JQ20a45XY', 'jq20a45xy.txt')
export_table('TCJXY', 'tcjxy.txt')
export_table('TLGXY', 'tlgxy.txt')
export_table('TLQXY', 'tlqxy.txt')

conn.close()
print("All done!")
