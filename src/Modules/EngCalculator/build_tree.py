import os
import sys
from collections import OrderedDict

sys.stdout.reconfigure(encoding='utf-8')

formula_dir = r'C:\Users\郭一彬\Documents\Codex\麦豆宝源码\src\Modules\EngCalculator\formulas'

# 分类名称映射 (根据文件命名规则)
category_names = {
    '01': '常用公式计算',
    '02': '工程力学/材料力学',
    '03': '数学计算',
    '04': '其他计算',
    '05': '机械设计计算',
    '07': '其他',
    '08': '单位换算',
    '09': '其他计算',
    '10': '其他',
    '11': '其他计算',
    '12': '其他',
    '13': '其他计算',
}

# 01类子分类
sub01 = {
    '01': '三角形', '02': '四边形', '03': '圆', '04': '梯形',
    '05': '正多边形', '06': '圆弧/弓形', '07': '椭圆', '08': '扇形',
    '09': '菱形', '10': '平行四边形', '11': '五边形', '12': '六边形',
    '13': '不规则图形', '14': '抛物线', '15': '双曲线', '16': '其他',
}

# 02类子分类
sub02 = {
    '01': '截面力学特性', '06': '梁的弯曲', '07': '扭转',
    '08': '压杆稳定', '09': '应力集中', '10': '组合变形',
}

# 05类子分类
sub05 = {
    '01': '螺纹连接', '02': '齿轮设计', '03': '蜗杆传动',
    '04': '带传动', '05': '链传动', '06': '弹簧设计',
}

# 收集所有图片文件
files = sorted([f for f in os.listdir(formula_dir) if f.endswith('.bmp')])

# 构建树结构
tree = OrderedDict()
for f in files:
    parts = f.replace('.bmp', '').split('.')
    if len(parts) >= 2:
        cat1 = parts[0]
        cat2 = parts[1] if len(parts) >= 2 else ''
        cat3 = parts[2] if len(parts) >= 3 else ''
        if cat1 not in tree:
            tree[cat1] = OrderedDict()
        if cat2 not in tree[cat1]:
            tree[cat1][cat2] = []
        tree[cat1][cat2].append(f)

# 输出树结构为C#可用的格式
print("// Formula tree structure")
print("private void BuildFormulaTree()")
print("{")
for cat1 in sorted(tree.keys()):
    cat1_name = category_names.get(cat1, cat1)
    print(f'    var node{cat1} = new TreeNode("{cat1} {cat1_name}");')
    for cat2 in sorted(tree[cat1].keys()):
        # 尝试找子分类名
        sub_name = ''
        if cat1 == '01':
            sub_name = sub01.get(cat2, '')
        elif cat1 == '02':
            sub_name = sub02.get(cat2, '')
        elif cat1 == '05':
            sub_name = sub05.get(cat2, '')
        label = f"{cat1}.{cat2}"
        if sub_name:
            label += f" {sub_name}"
        print(f'    var node{cat1}_{cat2} = new TreeNode("{label}");')
        for img in tree[cat1][cat2]:
            img_name = img.replace('.bmp', '')
            print(f'    node{cat1}_{cat2}.Nodes.Add(new TreeNode("{img_name}") {{ Tag = "{img}" }});')
        print(f'    node{cat1}.Nodes.Add(node{cat1}_{cat2});')
    print(f'    treeFormulas.Nodes.Add(node{cat1});')
print("}")
