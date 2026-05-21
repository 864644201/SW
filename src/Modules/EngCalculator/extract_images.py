import zlib
import os
import sys

sys.stdout.reconfigure(encoding='utf-8')

src_dir = r'E:\Program Files (x86)\机械设计3.0\工程计算器\DATA'
dst_dir = r'C:\Users\郭一彬\Documents\Codex\麦豆宝源码\src\Modules\EngCalculator\formulas'
os.makedirs(dst_dir, exist_ok=True)

count = 0
errors = 0
for fname in os.listdir(src_dir):
    if not fname.endswith('.bmp.wh'):
        continue
    src_path = os.path.join(src_dir, fname)
    try:
        with open(src_path, 'rb') as f:
            data = f.read()
        if len(data) < 6:
            continue
        # bytes 0-3: uncompressed size (little-endian uint32)
        # bytes 4-5: zlib header (78 9C)
        # bytes 6+: deflate compressed data
        compressed = data[4:]  # include zlib header
        bmp_data = zlib.decompress(compressed)
        out_name = fname.replace('.bmp.wh', '.bmp')
        out_path = os.path.join(dst_dir, out_name)
        with open(out_path, 'wb') as f:
            f.write(bmp_data)
        count += 1
    except Exception as e:
        errors += 1

print(f"Extracted {count} images, {errors} errors")
