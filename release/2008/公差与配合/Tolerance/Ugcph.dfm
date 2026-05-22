object frmgcph: Tfrmgcph
  Left = 218
  Top = 168
  BorderIcons = [biSystemMenu, biMinimize]
  BorderStyle = bsSingle
  Caption = #20844#24046#19982#37197#21512#26597#35810
  ClientHeight = 421
  ClientWidth = 717
  Color = clBtnFace
  Constraints.MaxHeight = 800
  Constraints.MaxWidth = 900
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  Menu = MainMenu1
  OldCreateOrder = False
  OnCreate = FormCreate
  OnShow = FormShow
  PixelsPerInch = 96
  TextHeight = 13
  object Label1: TLabel
    Left = 159
    Top = 19
    Width = 73
    Height = 22
    AutoSize = False
    Caption = #22522#26412#23610#23544
    Font.Charset = ANSI_CHARSET
    Font.Color = clWindowText
    Font.Height = -16
    Font.Name = #23435#20307
    Font.Style = []
    ParentFont = False
  end
  object Label14: TLabel
    Left = 241
    Top = 45
    Width = 25
    Height = 21
    AutoSize = False
    Caption = 'mm'
  end
  object Label21: TLabel
    Left = 335
    Top = 128
    Width = 49
    Height = 14
    Caption = 'Label21'
    Font.Charset = GB2312_CHARSET
    Font.Color = clRed
    Font.Height = -14
    Font.Name = #23435#20307
    Font.Style = []
    ParentFont = False
    Visible = False
  end
  object Label22: TLabel
    Left = 462
    Top = 128
    Width = 49
    Height = 14
    Caption = 'Label22'
    Font.Charset = GB2312_CHARSET
    Font.Color = clRed
    Font.Height = -14
    Font.Name = #23435#20307
    Font.Style = []
    ParentFont = False
    Visible = False
  end
  object Edit1: TEdit
    Left = 160
    Top = 40
    Width = 81
    Height = 21
    MaxLength = 5
    TabOrder = 0
    Text = '20'
    OnChange = Edit1Change
  end
  object GroupBox1: TGroupBox
    Left = 470
    Top = 16
    Width = 209
    Height = 104
    Caption = #20844#24046#24102
    Font.Charset = ANSI_CHARSET
    Font.Color = clWindowText
    Font.Height = -14
    Font.Name = #23435#20307
    Font.Style = []
    ParentFont = False
    TabOrder = 3
    object Label10: TLabel
      Left = 42
      Top = 20
      Width = 57
      Height = 17
      AutoSize = False
      Caption = #20844#24046#24102
      Font.Charset = ANSI_CHARSET
      Font.Color = clWindowText
      Font.Height = -14
      Font.Name = #23435#20307
      Font.Style = []
      ParentFont = False
    end
    object Label11: TLabel
      Left = 115
      Top = 20
      Width = 65
      Height = 17
      AutoSize = False
      Caption = #20844#24046#31561#32423
      Font.Charset = ANSI_CHARSET
      Font.Color = clWindowText
      Font.Height = -14
      Font.Name = #23435#20307
      Font.Style = []
      ParentFont = False
    end
    object Label12: TLabel
      Left = 21
      Top = 44
      Width = 14
      Height = 14
      Caption = #23380
    end
    object Label13: TLabel
      Left = 21
      Top = 75
      Width = 14
      Height = 14
      Caption = #36724
      Visible = False
    end
    object ComboBox1: TComboBox
      Left = 42
      Top = 40
      Width = 65
      Height = 24
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      ItemHeight = 16
      ParentFont = False
      TabOrder = 0
      OnChange = ComboBox1Change
    end
    object ComboBox2: TComboBox
      Left = 115
      Top = 40
      Width = 65
      Height = 24
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      ItemHeight = 16
      ParentFont = False
      TabOrder = 1
      Text = '6'
      OnChange = ComboBox2Change
      Items.Strings = (
        '')
    end
    object ComboBox3: TComboBox
      Left = 42
      Top = 71
      Width = 65
      Height = 24
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      ItemHeight = 16
      ParentFont = False
      TabOrder = 2
      Visible = False
      OnChange = ComboBox3Change
    end
    object ComboBox4: TComboBox
      Left = 115
      Top = 71
      Width = 65
      Height = 24
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      ItemHeight = 16
      ParentFont = False
      TabOrder = 3
      Text = '5'
      Visible = False
      OnChange = ComboBox4Change
    end
  end
  object GroupBox2: TGroupBox
    Left = 159
    Top = 71
    Width = 161
    Height = 49
    Caption = #22522#20934#21046
    Font.Charset = ANSI_CHARSET
    Font.Color = clWindowText
    Font.Height = -14
    Font.Name = #23435#20307
    Font.Style = []
    ParentFont = False
    TabOrder = 1
    object RadioButton1: TRadioButton
      Left = 8
      Top = 20
      Width = 65
      Height = 25
      Caption = #22522#23380#21046
      Checked = True
      TabOrder = 0
      TabStop = True
      OnClick = RadioButton1Click
    end
    object RadioButton2: TRadioButton
      Left = 84
      Top = 20
      Width = 65
      Height = 25
      Caption = #22522#36724#21046
      TabOrder = 1
      OnClick = RadioButton2Click
    end
  end
  object GroupBox4: TGroupBox
    Left = 311
    Top = 151
    Width = 209
    Height = 106
    Caption = #26597#35810#32467#26524
    Font.Charset = ANSI_CHARSET
    Font.Color = clWindowText
    Font.Height = -14
    Font.Name = #23435#20307
    Font.Style = []
    ParentFont = False
    TabOrder = 2
    object Label2: TLabel
      Left = 16
      Top = 46
      Width = 58
      Height = 23
      Alignment = taRightJustify
      AutoSize = False
      BiDiMode = bdRightToLeft
      Font.Charset = ANSI_CHARSET
      Font.Color = clBlue
      Font.Height = -19
      Font.Name = #23435#20307
      Font.Style = []
      ParentBiDiMode = False
      ParentFont = False
    end
    object Label3: TLabel
      Left = 79
      Top = 26
      Width = 29
      Height = 20
      Alignment = taRightJustify
      AutoSize = False
      Font.Charset = ANSI_CHARSET
      Font.Color = clBlue
      Font.Height = -19
      Font.Name = #23435#20307
      Font.Style = []
      ParentFont = False
    end
    object Label4: TLabel
      Left = 114
      Top = 14
      Width = 76
      Height = 20
      AutoSize = False
      Font.Charset = ANSI_CHARSET
      Font.Color = clBlue
      Font.Height = -19
      Font.Name = #23435#20307
      Font.Style = []
      ParentFont = False
    end
    object Label5: TLabel
      Left = 114
      Top = 35
      Width = 76
      Height = 20
      AutoSize = False
      Font.Charset = ANSI_CHARSET
      Font.Color = clBlue
      Font.Height = -19
      Font.Name = #23435#20307
      Font.Style = []
      ParentFont = False
    end
    object Label6: TLabel
      Left = 60
      Top = 68
      Width = 48
      Height = 20
      Alignment = taRightJustify
      AutoSize = False
      Font.Charset = ANSI_CHARSET
      Font.Color = clBlue
      Font.Height = -19
      Font.Name = #23435#20307
      Font.Style = []
      ParentFont = False
    end
    object Label7: TLabel
      Left = 114
      Top = 57
      Width = 68
      Height = 20
      AutoSize = False
      Font.Charset = ANSI_CHARSET
      Font.Color = clBlue
      Font.Height = -19
      Font.Name = #23435#20307
      Font.Style = []
      ParentFont = False
    end
    object Label8: TLabel
      Left = 114
      Top = 78
      Width = 76
      Height = 20
      AutoSize = False
      Font.Charset = ANSI_CHARSET
      Font.Color = clBlue
      Font.Height = -19
      Font.Name = #23435#20307
      Font.Style = []
      ParentFont = False
    end
    object Label20: TLabel
      Left = 16
      Top = 68
      Width = 58
      Height = 20
      Alignment = taRightJustify
      AutoSize = False
      Font.Charset = ANSI_CHARSET
      Font.Color = clBlue
      Font.Height = -19
      Font.Name = #23435#20307
      Font.Style = []
      ParentFont = False
      Visible = False
    end
    object Label19: TLabel
      Left = 16
      Top = 26
      Width = 58
      Height = 20
      Alignment = taRightJustify
      AutoSize = False
      Font.Charset = ANSI_CHARSET
      Font.Color = clBlue
      Font.Height = -19
      Font.Name = #23435#20307
      Font.Style = []
      ParentFont = False
      Visible = False
    end
  end
  object StatusBar1: TStatusBar
    Left = 0
    Top = 394
    Width = 717
    Height = 27
    Panels = <
      item
        Text = #23380#12289#36724#19979#25289#33756#21333#20013#20026#20248#20808#21450#24120#29992#37197#21512#12290#20063#21487#30452#25509#36755#20837#20844#24046#24102#21450#20844#24046#31561#32423#36827#34892#26597#35810#12290
        Width = 50
      end>
  end
  object BitBtn1: TBitBtn
    Left = 185
    Top = 154
    Width = 90
    Height = 25
    Caption = #26597#35810#37197#21512
    TabOrder = 5
    Visible = False
    OnClick = BitBtn1Click
    Kind = bkOK
  end
  object BitBtn2: TBitBtn
    Left = 185
    Top = 194
    Width = 90
    Height = 25
    Caption = #32467#26524#21478#23384#20026
    Default = True
    ModalResult = 6
    TabOrder = 6
    OnClick = BitBtn2Click
    Glyph.Data = {
      DE010000424DDE01000000000000760000002800000024000000120000000100
      0400000000006801000000000000000000001000000000000000000000000000
      80000080000000808000800000008000800080800000C0C0C000808080000000
      FF0000FF000000FFFF00FF000000FF00FF00FFFF0000FFFFFF00333333333333
      3333333333333333333333330000333333333333333333333333F33333333333
      00003333344333333333333333388F3333333333000033334224333333333333
      338338F3333333330000333422224333333333333833338F3333333300003342
      222224333333333383333338F3333333000034222A22224333333338F338F333
      8F33333300003222A3A2224333333338F3838F338F33333300003A2A333A2224
      33333338F83338F338F33333000033A33333A222433333338333338F338F3333
      0000333333333A222433333333333338F338F33300003333333333A222433333
      333333338F338F33000033333333333A222433333333333338F338F300003333
      33333333A222433333333333338F338F00003333333333333A22433333333333
      3338F38F000033333333333333A223333333333333338F830000333333333333
      333A333333333333333338330000333333333333333333333333333333333333
      0000}
    NumGlyphs = 2
  end
  object BitBtn3: TBitBtn
    Left = 185
    Top = 234
    Width = 90
    Height = 25
    Caption = #21047#26032
    Default = True
    ModalResult = 4
    TabOrder = 7
    OnClick = BitBtn3Click
    Glyph.Data = {
      DE010000424DDE01000000000000760000002800000024000000120000000100
      0400000000006801000000000000000000001000000000000000000000000000
      80000080000000808000800000008000800080800000C0C0C000808080000000
      FF0000FF000000FFFF00FF000000FF00FF00FFFF0000FFFFFF00333333444444
      33333333333F8888883F33330000324334222222443333388F3833333388F333
      000032244222222222433338F8833FFFFF338F3300003222222AAAAA22243338
      F333F88888F338F30000322222A33333A2224338F33F8333338F338F00003222
      223333333A224338F33833333338F38F00003222222333333A444338FFFF8F33
      3338888300003AAAAAAA33333333333888888833333333330000333333333333
      333333333333333333FFFFFF000033333333333344444433FFFF333333888888
      00003A444333333A22222438888F333338F3333800003A2243333333A2222438
      F38F333333833338000033A224333334422224338338FFFFF8833338000033A2
      22444442222224338F3388888333FF380000333A2222222222AA243338FF3333
      33FF88F800003333AA222222AA33A3333388FFFFFF8833830000333333AAAAAA
      3333333333338888883333330000333333333333333333333333333333333333
      0000}
    NumGlyphs = 2
  end
  object ToolBar1: TToolBar
    Left = 0
    Top = 0
    Width = 717
    Height = 2
    Caption = 'ToolBar1'
    TabOrder = 8
    Transparent = False
  end
  object BitBtn4: TBitBtn
    Left = 185
    Top = 154
    Width = 90
    Height = 25
    Caption = #26597#35810#20844#24046
    TabOrder = 9
    OnClick = BitBtn4Click
    Kind = bkOK
  end
  object Memo3: TMemo
    Left = 50
    Top = 280
    Width = 609
    Height = 105
    Color = clBtnFace
    Lines.Strings = (
      '    '#36807#30408#37197#21512#23601#26159#22312#23380#19982#36724#30340#37197#21512#20013#65292#23380#30340#23610#23544#20943#21435#30456#37197#21512#30340#36724#30340#23610#23544#65292#20854#24046#20540#20026#36127#26102#30340#37197#21512#12290
      '    '#36807#30408#37197#21512#26102#36724#30340#22522#26412#20559#24046#36873#29992#35828#26126#65306
      '    (1) p'#65306#19982'H6'#25110'H7'#37197#21512#26102#26159#36807#30408#37197#21512#65292#19982'H8'#37197#21512#26102#21017#20026#36807#28193#37197#21512#12290#23545#20110#38750#38081#31867#38646#20214#65292#20026#36739#36731#30340#21387#20837#37197#21512#65292
      #24403#38656#35201#26102#26131#20110#25286#21368#12290#23545#38050#12289#38136#38081#25110#38108#12289#38050#32452#20214#35013#37197#26159#26631#20934#21387#20837#37197#21512#12290
      '    (2) r'#65306#23545#38081#31867#38646#20214#20026#20013#31561#25171#20837#37197#21512#65292#23545#38750#38081#31867#38646#20214#20026#36731#25171#20837#37197#21512#65292#24403#38656#35201#26102#21487#20197#25286#21368#12290#19982'H8'#23380#37197#21512#65292#30452
      #24452#22312'100mm'#20197#19978#26102#20026#36807#30408#37197#21512#65292#30452#24452#23567#26102#20026#36807#28193#37197#21512#12290
      '    (3) s'#65306#29992#20110#38050#21644#38081#21046#38646#20214#30340#27704#20037#24615#21644#21322#27704#20037#24615#35013#37197#65292#21487#20135#29983#30456#24403#22823#30340#32467#21512#21147#12290#24403#29992#24377#24615#26448#26009#65292#22914#36731#21512#37329
      #26102#65292#37197#21512#24615#36136#19982#38081#31867#38646#20214#30340'p'#36724#30456#24403#65292#22914#22871#29615#21387#35013#22312#36724#19978#12289#38400#24231#31561#30340#37197#21512#12290#23610#23544#36739#22823#26102#65292#20026#36991#20813#25439#20260#37197#21512#34920#38754
      #65292#38656#29992#28909#32960#25110#20919#32553#27861#35013#37197#12290
      '    (4) t'#65306#36807#30408#36739#22823#30340#37197#21512#12290#23545#38050#21644#38136#38081#31867#38646#20214#36866#20110#20316#27704#20037#24615#32467#21512#65292#19981#29992#38190#21487#20256#36882#21147#30697#65292#38656#29992#28909#32960#25110#20919#32553#27861
      #35013#37197#65292#22914#36830#36724#33410#19982#36724#30340#37197#21512#12290
      '    (5) u'#65306#36825#31181#37197#21512#36807#30408#22823#65292#19968#33324#24212#39564#31639#22312#26368#22823#36807#30408#26102#65292#24037#20214#26448#26009#26159#21542#25439#22351#65292#35201#29992#28909#32960#25110#20919#32553#27861#35013#37197#12290#20363#22914
      #28779#36710#36718#27586#21644#36724#30340#37197#21512#12290
      '    (6) v'#12289'x'#12289'y'#12289'z'#65306#36825#20123#22522#26412#20559#24046#25152#32452#25104#30340#37197#21512#36807#30408#37327#26356#22823#65292#39035#32463#35797#39564#21518#25165#33021#24212#29992#65292#19968#33324#19981#25512#33616#12290)
    ReadOnly = True
    ScrollBars = ssVertical
    TabOrder = 10
    Visible = False
  end
  object Memo2: TMemo
    Left = 50
    Top = 280
    Width = 609
    Height = 105
    Color = clBtnFace
    Lines.Strings = (
      '    '#36807#28193#37197#21512#23601#26159#22312#37197#21512#20013#65292#23380#19982#36724#30340#20844#24046#24102#30456#20114#20132#36845#65292#20219#21462#20854#20013#19968#23545#23380#21644#36724#30456#37197#65292#21487#33021#20855#26377#38388#38553#65292#20063#21487#33021#20855
      #26377#36807#30408#30340#37197#21512#12290
      '    '#36807#28193#37197#21512#26102#36724#30340#22522#26412#20559#24046#36873#29992#35828#26126#65306
      '    (1) js'#65306#20559#24046#23436#20840#23545#31216#65288'+IT/2'#21644'-IT/2'#65289#65292#24179#22343#38388#38553#36739#23567#30340#37197#21512#65292#22810#29992#20110'IT4-7'#32423#65292#35201#27714#38388#38553#27604'h'#36724#23567#65292#24182
      #20801#35768#30053#26377#36807#30408#30340#23450#20301#37197#21512#65292#22914#36830#36724#33410#12289#40831#22280#19982#38050#21046#36718#27586#31561#12290#21487#29992#26408#38180#35013#37197#12290
      '    (2) k'#65306#24179#22343#38388#38553#25509#36817#20110#38646#30340#37197#21512#65292#36866#29992#20110'IT4-7'#32423#65292#25512#33616#29992#20110#31245#26377#36807#30408#30340#23450#20301#37197#21512#65292#22914#20026#28040#38500#38663#21160#25152#20351#29992
      #30340#37197#21512#12290#19968#33324#29992#26408#38180#35013#37197#12290
      '    (3) m'#65306#24179#22343#36807#30408#37197#21512#36739#23567#30340#37197#21512#65292#36866#29992#20110'IT4-7'#32423#65292#19968#33324#21487#29992#26408#38180#35013#37197#65292#20294#22312#26368#22823#36807#30408#26102#65292#35201#27714#30456#24403#30340#21387
      #20837#21147#12290
      '    (4) n'#65306#24179#22343#36807#30408#27604'm'#31245#22823#65292#24456#23569#24471#21040#38388#38553#65292#36866#29992#20110'IT4-7'#32423#65292#29992#38180#25110#21387#20837#26426#35013#37197#65292#36890#24120#25512#33616#29992#20110#32039#23494#30340#32452
      #20214#37197#21512#12290'H6/n5'#37197#21512#26102#20026#36807#30408#37197#21512#12290)
    ReadOnly = True
    ScrollBars = ssVertical
    TabOrder = 11
    Visible = False
  end
  object Memo1: TMemo
    Left = 50
    Top = 280
    Width = 609
    Height = 105
    Color = clBtnFace
    Lines.Strings = (
      '    '#22312#23380#19982#36724#30340#37197#21512#20013#65292#23380#30340#23610#23544#20943#21435#30456#37197#21512#36724#30340#23610#23544#65292#20854#24046#20540#20026#27491#26102#20026#38388#38553#12290#38388#38553#37197#21512#23601#26159#23380#20844#24046#24102#22312#36724#20844
      #24046#24102#20043#19978#65292#20855#26377#38388#38553#30340#37197#21512#65288#21253#25324#26368#23567#38388#38553#20026#38646#30340#37197#21512#65289#12290
      '    '#38388#38553#37197#21512#26102#36724#30340#22522#26412#20559#24046#36873#29992#35828#26126#65306
      '    (1) a'#12289'b'#65306#21487#24471#21040#29305#21035#22823#30340#38388#38553#65292#24212#29992#24456#23569#12290
      '    (2) c'#65306#21487#24471#21040#24456#22823#30340#38388#38553#65292#19968#33324#36866#29992#20110#32531#24930#12289#26494#24347#30340#21160#37197#21512#12290#29992#20110#24037#20316#26465#20214#36739#24046#65288#22914#20892#19994#26426#26800#31561#65289#12289#21463
      #21147#21464#24418#12289#25110#20026#20415#20110#35013#37197#32780#24517#39035#20445#35777#26377#36739#22823#30340#38388#38553#26102#65292#25512#33616#37197#21512#20026'H11/c11'#65292#36739#39640#31561#32423#30340'H8/c7'#37197#21512#65292#36866#29992#20110#36724#22312
      #39640#28201#24037#20316#30340#32039#23494#21160#37197#21512#65292#22914#20869#29123#26426#25490#27668#38400#21644#23548#31649#31561#12290
      '    (3) d'#65306#19968#33324#29992#20110'IT7-IT11'#32423#65292#36866#29992#20110#26494#30340#36716#21160#37197#21512#65292#22914#23494#23553#30422#12289#28369#36718#12289#31354#36716#30382#24102#36718#31561#19982#36724#30340#37197#21512#65292#20063#36866
      #29992#20110#22823#30452#24452#28369#21160#36724#25215#37197#21512#65292#22914#36879#24179#26426#12289#29699#30952#26426#12289#36711#36746#25104#22411#21644#37325#22411#24367#26354#26426#20197#21450#20854#23427#37325#22411#26426#26800#20013#30340#19968#20123#28369#21160#36724#25215#12290
      '    (4) e'#65306#22810#29992#20110'IT7'#12289'8'#12289'9'#32423#65292#36890#24120#29992#20110#35201#27714#26377#26126#26174#38388#38553#65292#26131#20110#36716#21160#30340#36724#25215#37197#21512#65292#22914#22823#36328#36317#36724#25215#12289#22810#25903#28857#36724
      #25215#31561#37197#21512#65292#39640#31561#32423#30340'e'#36724#36866#29992#20110#22823#30340#12289#39640#36895#12289#37325#36733#25903#25215#65292#22914#28065#36718#21457#21160#26426#12289#22823#22411#30005#21160#26426#21450#20869#29123#26426#20027#35201#36724#25215#12289#20984#36718
      #36724#36724#25215#31561#37197#21512#12290
      '    (5) f'#65306#22810#29992#20110'IT6'#12289'7'#12289'8'#32423#30340#19968#33324#20256#21160#37197#21512#65292#24403#28201#24230#24433#21709#19981#22823#26102#65292#34987#24191#27867#29992#20110#26222#36890#28070#28369#27833#65288#25110#28070#28369#33026#65289#28070
      #28369#30340#25903#25215#65292#22914#40831#36718#31665#12289#23567#30005#21160#26426#12289#27893#31561#30340#36716#36724#19982#28369#21160#36724#25215#30340#37197#21512#12290
      '    (6) g'#65306#37197#21512#38388#38553#24456#23567#65292#21046#36896#25104#26412#39640#65292#38500#24456#36731#36127#33655#30340#31934#23494#35013#32622#22806#65292#19981#25512#33616#29992#20110#36716#21160#37197#21512#12290#22810#29992#20110'IT5'#12289'6'#12289'7'
      #32423#65292#26368#36866#21512#19981#22238#36716#30340#31934#23494#28369#21160#37197#21512#65292#20063#29992#20110#25554#38144#31561#23450#20301#37197#21512#65292#22914#31934#23494#36830#26438#36724#25215#12289#27963#22622#21450#28369#38400#12289#36830#26438#38144#31561#12290
      '    (7) h'#65306#22810#29992#20110'IT4-IT11'#32423#12290#20316#20026#19968#33324#30340#23450#20301#37197#21512#65292#24191#27867#29992#20110#26080#30456#23545#36716#21160#30340#38646#20214#12290#33509#27809#26377#28201#24230#12289#21464#24418#24433#21709
      #65292#20063#21487#29992#20110#31934#23494#28369#21160#37197#21512#12290)
    ReadOnly = True
    ScrollBars = ssVertical
    TabOrder = 12
    Visible = False
  end
  object GroupBox6: TGroupBox
    Left = 16
    Top = 16
    Width = 121
    Height = 105
    Caption = #26597#35810#36873#39033
    Font.Charset = ANSI_CHARSET
    Font.Color = clWindowText
    Font.Height = -14
    Font.Name = #23435#20307
    Font.Style = []
    ParentFont = False
    TabOrder = 13
    object RadioButton7: TRadioButton
      Left = 19
      Top = 69
      Width = 80
      Height = 17
      Caption = #37197#21512#26597#35810
      Font.Charset = GB2312_CHARSET
      Font.Color = clWindowText
      Font.Height = -14
      Font.Name = #23435#20307
      Font.Style = []
      ParentFont = False
      TabOrder = 0
      OnClick = RadioButton7Click
    end
    object RadioButton6: TRadioButton
      Left = 19
      Top = 29
      Width = 80
      Height = 17
      Caption = #20844#24046#26597#35810
      Checked = True
      Font.Charset = GB2312_CHARSET
      Font.Color = clWindowText
      Font.Height = -14
      Font.Name = #23435#20307
      Font.Style = []
      ParentFont = False
      TabOrder = 1
      TabStop = True
      OnClick = RadioButton6Click
    end
  end
  object GroupBox3: TGroupBox
    Left = 335
    Top = 16
    Width = 116
    Height = 104
    Caption = #37197#21512#26041#24335
    Font.Charset = ANSI_CHARSET
    Font.Color = clWindowText
    Font.Height = -14
    Font.Name = #23435#20307
    Font.Style = []
    ParentFont = False
    TabOrder = 14
    Visible = False
    object Lb: TLabel
      Left = 14
      Top = 40
      Width = 8
      Height = 16
      Font.Charset = ANSI_CHARSET
      Font.Color = clBlue
      Font.Height = -16
      Font.Name = #23435#20307
      Font.Style = []
      ParentFont = False
    end
    object RadioButton3: TRadioButton
      Left = 17
      Top = 23
      Width = 81
      Height = 17
      Caption = #38388#38553#37197#21512
      TabOrder = 0
      Visible = False
      OnClick = RadioButton3Click
    end
    object RadioButton4: TRadioButton
      Left = 17
      Top = 49
      Width = 81
      Height = 17
      Caption = #36807#28193#37197#21512
      TabOrder = 1
      Visible = False
      OnClick = RadioButton4Click
    end
    object RadioButton5: TRadioButton
      Left = 17
      Top = 75
      Width = 81
      Height = 17
      Caption = #36807#30408#37197#21512
      TabOrder = 2
      Visible = False
      OnClick = RadioButton5Click
    end
  end
  object GroupBox5: TGroupBox
    Left = 335
    Top = 16
    Width = 116
    Height = 104
    Caption = #26597#35810#23545#35937
    Font.Charset = ANSI_CHARSET
    Font.Color = clWindowText
    Font.Height = -14
    Font.Name = #23435#20307
    Font.Style = []
    ParentFont = False
    TabOrder = 15
    object Label17: TLabel
      Left = 14
      Top = 40
      Width = 8
      Height = 16
      Font.Charset = ANSI_CHARSET
      Font.Color = clBlue
      Font.Height = -16
      Font.Name = #23435#20307
      Font.Style = []
      ParentFont = False
    end
    object RadioButton8: TRadioButton
      Left = 12
      Top = 36
      Width = 96
      Height = 17
      Caption = #23380#20844#24046#26597#35810
      Checked = True
      TabOrder = 0
      TabStop = True
      OnClick = RadioButton8Click
    end
    object RadioButton9: TRadioButton
      Left = 12
      Top = 73
      Width = 96
      Height = 17
      Caption = #36724#20844#24046#26597#35810
      TabOrder = 1
      OnClick = RadioButton9Click
    end
  end
  object MainMenu1: TMainMenu
    Left = 25
    Top = 186
    object N1: TMenuItem
      Caption = #25991#20214
      object N4: TMenuItem
        Caption = #25171#24320
        OnClick = N4Click
      end
      object N2: TMenuItem
        Caption = #26597#35810#32467#26524#21478#23384#20026
        OnClick = N2Click
      end
      object N3: TMenuItem
        Caption = #20851#38381
        OnClick = N3Click
      end
    end
    object N5: TMenuItem
      Caption = #24110#21161
      object C1: TMenuItem
        Caption = #24110#21161#20027#39064'(&C)'
        ShortCut = 112
        OnClick = C1Click
      end
    end
  end
  object SaveDialog1: TSaveDialog
    DefaultExt = 'txt'
    Filter = '*.txt|*.txt'
    Left = 24
    Top = 144
  end
  object OpenDialog1: TOpenDialog
    Left = 24
    Top = 224
  end
end
