unit Ugcph;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, ExtCtrls, StdCtrls, ComCtrls, DataUnit,DB, Buttons, Menus,ShellAPI,
  ToolWin;

type
  Tfrmgcph = class(TForm)
    Label1: TLabel;
    Edit1: TEdit;
    GroupBox1: TGroupBox;
    GroupBox2: TGroupBox;
    RadioButton1: TRadioButton;
    RadioButton2: TRadioButton;
    GroupBox4: TGroupBox;
    Label2: TLabel;
    Label3: TLabel;
    Label4: TLabel;
    Label5: TLabel;
    Label6: TLabel;
    Label7: TLabel;
    Label8: TLabel;
    StatusBar1: TStatusBar;
    Label10: TLabel;
    Label11: TLabel;
    Label12: TLabel;
    Label13: TLabel;
    ComboBox1: TComboBox;
    ComboBox2: TComboBox;
    ComboBox3: TComboBox;
    ComboBox4: TComboBox;
    Label14: TLabel;
    BitBtn1: TBitBtn;
    BitBtn2: TBitBtn;
    BitBtn3: TBitBtn;
    MainMenu1: TMainMenu;
    N1: TMenuItem;
    N2: TMenuItem;
    N3: TMenuItem;
    SaveDialog1: TSaveDialog;
    N4: TMenuItem;
    OpenDialog1: TOpenDialog;
    ToolBar1: TToolBar;
    Label20: TLabel;
    Label19: TLabel;
    Label21: TLabel;
    BitBtn4: TBitBtn;
    Memo3: TMemo;
    Memo2: TMemo;
    Memo1: TMemo;
    Label22: TLabel;
    GroupBox6: TGroupBox;
    RadioButton7: TRadioButton;
    RadioButton6: TRadioButton;
    GroupBox3: TGroupBox;
    Lb: TLabel;
    RadioButton3: TRadioButton;
    RadioButton4: TRadioButton;
    RadioButton5: TRadioButton;
    GroupBox5: TGroupBox;
    Label17: TLabel;
    RadioButton8: TRadioButton;
    RadioButton9: TRadioButton;
    N5: TMenuItem;
    C1: TMenuItem;
    procedure Edit1Change(Sender: TObject);
    procedure RadioButton1Click(Sender: TObject);
    procedure RadioButton2Click(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure BitBtn1Click(Sender: TObject);
    procedure ComboBox1Change(Sender: TObject);
    procedure ComboBox3Change(Sender: TObject);
    procedure ComboBox2Change(Sender: TObject);
    procedure ComboBox4Change(Sender: TObject);
    procedure FormShow(Sender: TObject);
    procedure RadioButton3Click(Sender: TObject);
    procedure RadioButton4Click(Sender: TObject);
    procedure RadioButton5Click(Sender: TObject);
    procedure BitBtn2Click(Sender: TObject);
    procedure BitBtn3Click(Sender: TObject);
    procedure N3Click(Sender: TObject);
    procedure N2Click(Sender: TObject);
    procedure N4Click(Sender: TObject);
    procedure BitBtn4Click(Sender: TObject);
    procedure RadioButton8Click(Sender: TObject);
    procedure RadioButton9Click(Sender: TObject);
    procedure RadioButton6Click(Sender: TObject);
    procedure RadioButton7Click(Sender: TObject);
    procedure C1Click(Sender: TObject);
        
  private
    { Private declarations }
  public
    BasicSize: Single;
    BasicSize1: Single;
    BasicSize2: Single;
    { Public declarations }
  end;

var
  frmgcph: Tfrmgcph;

implementation



{$R *.dfm}

procedure Tfrmgcph.Edit1Change(Sender: TObject);
begin
  if (Edit1.Text >='a') and (Edit1.Text <='z') or
     (Edit1.Text >='A') and (Edit1.Text <='Z') then
  begin
    BasicSize :=0;
    MessageBox (
    HWND (0),
    LPCTSTR ('本查询只限于0到3150之间的基本尺寸'),	// address of text in message box
    LPCTSTR ('提示'),	// address of title of message box
    UINT (MB_OK)
    );
    Edit1.Text :='20';
    Edit1.SetFocus;
    Edit1.SelectAll;
  end

  else if (Edit1.Text = '') or (StrToFloat(Edit1.Text)<0) or (StrToFloat(Edit1.Text)>3150) then
  begin
  BasicSize :=0;
  MessageBox (
    HWND (0),
    LPCTSTR ('本查询只限于0到3150之间的基本尺寸'),	// address of text in message box
    LPCTSTR ('提示'),	// address of title of message box
    UINT (MB_OK)
    );
    Edit1.SetFocus;
    Edit1.SelectAll;
    Label2.Caption :='';
    Label3.Caption :='';
    Label4.Caption :='';
    Label5.Caption :='';
    Label6.Caption :='';
    Label7.Caption :='';
    Label8.Caption :='';
    RadioButton1.Checked :=False;
    RadioButton2.Checked :=False;
  // RadioButton3.Checked :=False;
  end;
end;

procedure Tfrmgcph.RadioButton1Click(Sender: TObject);
begin
  Label21.Visible:=False;
  Label22.Visible:=False;
  if RadioButton1.Checked then
  begin
    if (Edit1.Text ='') or (Edit1.Text =FloatToStr(0)) then
    begin
      MessageBox (
      HWND (0),
      LPCTSTR ('请输入0到3150之间的整数作为基本尺寸'),
      LPCTSTR ('提示'),
      UINT (MB_OK)
      );
      Edit1.Text := FloatToStr(0);
      Edit1.SetFocus;
      Edit1.SelectAll;
      Label2.Caption :='';
      Label3.Caption :='';
      Label4.Caption :='';
      Label5.Caption :='';
      Label6.Caption :='';
      Label7.Caption :='';
      Label8.Caption :='';
      RadioButton1.Checked :=False;
      RadioButton2.Checked :=False;
    {end
    else
    begin
      ComboBox1.Text :='H';
      ComboBox2.SetFocus;
      ComboBox3.Text :='';
      ComboBox4.Text :='';
      Lb.Caption:='';
      Label3.Caption :='';
      Label4.Caption :='';
      Label5.Caption :='';
      Label6.Caption :='';
      Label7.Caption :='';
      Label8.Caption :='';}
    end;
  end;
  if RadioButton1.Checked=true and RadioButton3.Checked=true then
  begin
    ComboBox1.Text:='H';
    ComboBox2.SetFocus;
    ComboBox2.text:='6';
    ComboBox2.Items.Clear;
    ComboBox2.Items.Append('6');
    ComboBox2.Items.Append('7');
    ComboBox2.Items.Append('8');
    ComboBox2.Items.Append('9');
    ComboBox2.Items.Append('10');
    ComboBox2.Items.Append('11');
    ComboBox2.Items.Append('12');
    ComboBox2.Text:='6';
    ComboBox2Change(nil);
    ComboBox3.Text :='';
    ComboBox4.Text :='5';
  end;
  if RadioButton1.Checked and RadioButton4.Checked then
  begin
    ComboBox1.Text:='H';
    ComboBox2.SetFocus;
    ComboBox2.text:='6';
    ComboBox2.Items.Clear;
    ComboBox2.Items.Append('6');
    ComboBox2.Items.Append('7');
    ComboBox2.Items.Append('8');
    ComboBox2.Text:='6';
    ComboBox2Change(nil);
    ComboBox3.Text :='';
    ComboBox4.Text :='5';
  end;
  if RadioButton1.Checked and RadioButton5.Checked then
  begin
    if RadioButton1.Checked and RadioButton5.Checked then
    begin
    ComboBox1.Text:='H';
    ComboBox2.SetFocus;
    ComboBox2.text:='6';
    ComboBox2.Items.Clear;
    ComboBox2.Items.Append('6');
    ComboBox2.Items.Append('7');
    ComboBox2.Items.Append('8');
    ComboBox2.Text:='6';
    ComboBox2Change(nil);
    ComboBox3.Text :='';
    ComboBox4.Text :='5';
    end;
  end;

  Label3.Caption:='';
  Label4.Caption:='';
  Label5.Caption:='';
  Label6.Caption:='';
  Label7.Caption:='';
  Label8.Caption:='';
end;

procedure Tfrmgcph.RadioButton2Click(Sender: TObject);
begin
  Label21.Visible:=False;
  Label22.Visible:=False;
  if RadioButton2.Checked then
  begin
    ComboBox3.Text :='h';
    ComboBox1.Text :='';
    ComboBox1.SetFocus;
  //  RadioButton3.Checked :=False;
 //   RadioButton4.Checked :=False;
  //  RadioButton5.Checked :=False;
    Lb.Caption:='';
    Label3.Caption :='';
    Label4.Caption :='';
    Label5.Caption :='';
    Label6.Caption :='';
    Label7.Caption :='';
    Label8.Caption :='';
  end;
  if RadioButton2.Checked and RadioButton3.Checked then
  begin
    ComboBox3.Text:='h';
    ComboBox4.SetFocus;
    ComboBox4.text:='5';
    ComboBox4.Items.Clear;
    ComboBox4.Items.Append('5');
    ComboBox4.Items.Append('6');
    ComboBox4.Items.Append('7');
    ComboBox4.Items.Append('8');
    ComboBox4.Items.Append('9');
    ComboBox4.Items.Append('10');
    ComboBox4.Items.Append('11');
    ComboBox4.Items.Append('12');
    ComboBox4.Text:='5';
    ComboBox4Change(nil);
    ComboBox1.Text :='';
    ComboBox2.Text :='6';
  end; 
  if RadioButton2.Checked and RadioButton4.Checked then
  begin
    ComboBox3.Text:='h';
    ComboBox4.SetFocus;
    ComboBox4.text:='5';
    ComboBox4.Items.Clear;
    ComboBox4.Items.Append('5');
    ComboBox4.Items.Append('6');
    ComboBox4.Items.Append('7');
    ComboBox4.Text:='5';
    ComboBox4Change(nil);
    ComboBox1.Text :='';
    ComboBox2.Text :='6';
  end;
  if RadioButton2.Checked and RadioButton5.Checked then
  begin
    ComboBox3.Text:='h';
    ComboBox4.SetFocus;
    ComboBox4.text:='5';
    ComboBox4.Items.Clear;
    ComboBox4.Items.Append('5');
    ComboBox4.Items.Append('6');
    ComboBox4.Text:='5';
    ComboBox4Change(nil);
    ComboBox1.Text :='';
    ComboBox2.Text :='6';
  end;

  Label3.Caption:='';
  Label4.Caption:='';
  Label5.Caption:='';
  Label6.Caption:='';
  Label7.Caption:='';
  Label8.Caption:='';
end;

procedure Tfrmgcph.FormCreate(Sender: TObject);
begin
  Edit1.TabOrder :=0;
  RadioButton1.TabOrder :=1;
  ComboBox1.TabOrder :=2;
  ComboBox2.TabOrder :=3;
  ComboBox3.TabOrder :=4;
  ComboBox4.TabOrder :=5;
end;

procedure Tfrmgcph.BitBtn1Click(Sender: TObject);
var
KongGCD:String;       //孔公差带
KongGCDJ:Integer;     //孔公差等级
ZhouGCD:String;       //轴公差带
ZhouGCDJ:Integer;     //轴公差等级

ESK:Double;           //孔上偏差
EIK:Double;           //孔下偏差
esz:Double;           //轴上偏差
eiz:Double;           //轴下偏差

GCDJ:String;          //公差等级
ITN:Double;           //标准公差数值
JianXiMin:Double;     //最小间隙

  //定义一个函数，以确定公差等级GCDJ，并返回其值
  function ConfirmGCDJ(a :Single) :String;
  begin
    GCDJ :='IT'+FloatToStr(a);
  end;

  //定义一个函数，以确定标准公差数值ITN，并返回其值
  function ConfirmITN(b :String) :Double;
  begin
    //确定Tabel1中的BasicSize的值，赋给BasicSize1，用于查询ITN
    Data2.ADOTable1.First;
    while not Data2.ADOTable1.Eof do
    begin
      if  BasicSize <=Data2.ADOTable1.FieldByName('基本尺寸').AsFloat then
      begin
        //Data.ADOTable1.Prior;
        BasicSize1 := Data2.ADOTable1.FieldByName('基本尺寸').AsFloat;
        Break;
      end;
      Data2.ADOTable1.Next;
    end;

    //根据选出的BasicSize的值查出标准公差数值ITN
    Data2.ADOTable1.First;
    if Data2.ADOTable1.Locate('基本尺寸',BasicSize1,[loPartialKey]) then
      begin
        ITN := Data2.ADOTable1.FieldByName(b).AsFloat/1000;
      end;
  end;

  //定义一个过程，用来确定Label4和Label5中数值的符号
  procedure ConfirmPlusSign(a :Double; b :Double);
  begin
  if (Edit1.Text = '') or (StrToFloat(Edit1.Text)<=0) or (StrToFloat(Edit1.Text)>3150) then
    begin
    Label4.Caption :='';
    Label5.Caption :='';
    end
  else
    begin
    if a>0 then
      Label4.Caption:='+'+FloatToStr(a)
    else
      Label4.Caption:=FloatToStr(a);
    if b>0 then
      Label5.Caption:='+'+FloatToStr(b)
    else
      Label5.Caption:=FloatToStr(b);
    end;
  end;

  //定义一个过程，用来确定Label7和Label8中数值的符号
  procedure ConfirmPlusSign2(a :Double; b :Double);
  begin
  if (Edit1.Text = '') or (StrToFloat(Edit1.Text)<=0) or (StrToFloat(Edit1.Text)>3150) then
    begin
    Label7.Caption :='';
    Label8.Caption :='';
    end
  else
    begin
    if a>0 then
      Label7.Caption :='+'+FloatToStr(a)
    else
      Label7.Caption :=FloatToStr(a);
    if b>0 then
      Label8.Caption :='+'+FloatToStr(b)
    else
      Label8.Caption :=FloatToStr(b);
    end;
  end;

  //由输入的BasicSize的值确定在Tabel2和Tabel3中的BasicSize的数值
  function ConfirmBasicSize2(a :Single) :Integer;
  begin
    Data2.ADOTable2.First;
    while not Data2.ADOTable1.Eof do
    begin
      if BasicSize <= Data2.ADOTable2.FieldByName('基本尺寸').AsFloat then
      begin
        //Data.ADOTable2.prior;
        BasicSize2 := Data2.ADOTable2.FieldByName('基本尺寸').AsFloat;
        Break;
      end;
      Data2.ADOTable2.Next;
    end;
  end;

  //定义一个查询过程SearchRule1,适用于孔的第一条通用规则
  procedure SearchRule1(a :string);
  begin
    ConfirmGCDJ(KongGCDJ);
    ConfirmITN(GCDJ);
    ConfirmBasicSize2(BasicSize);
    Data2.ADOTable2.First;
    if Data2.ADOTable2.Locate('基本尺寸',BasicSize2,[loPartialKey]) then
      begin
        EIK := Data2.ADOTable2.FieldByName(a).AsFloat/1000;
      end;
    ESK:=EIK+ITN;
    ConfirmPlusSign(ESK,EIK);
  end;

  //定义一个查询过程SearchRule2,适用于孔的第二条通用规则
  procedure SearchRule2(a :string);
  begin
    ConfirmGCDJ(KongGCDJ);
    ConfirmITN(GCDJ);
    ConfirmBasicSize2(BasicSize);
    Data2.ADOTable2.First;
    if Data2.ADOTable2.Locate('基本尺寸',BasicSize2,[loPartialKey]) then
      begin
        ESK := Data2.ADOTable2.FieldByName(a).AsFloat/1000;
      end;
    EIK:=ESK-ITN;
    ConfirmPlusSign(ESK,EIK);
  end;

  //定义一个查询过程SearchRule3,适用于公差等级为a~h的轴的偏差计算
  procedure SearchRule3(a :string);
  begin
      ConfirmGCDJ(ZhouGCDJ);
      ConfirmITN(GCDJ);
      ConfirmBasicSize2(BasicSize);
      Data2.ADOTable3.First;
      if Data2.ADOTable3.Locate('基本尺寸',BasicSize2,[loPartialKey]) then
        begin
          esz := Data2.ADOTable3.FieldByName(a).AsFloat/1000;
        end;
      eiz :=esz-ITN;
      ConfirmPlusSign2(esz,eiz);
  end;

  //定义一个查询过程SearchRule4,适用于公差等级为j~z的轴的偏差计算
  procedure SearchRule4(a :string);
  begin
      ConfirmGCDJ(ZhouGCDJ);
      ConfirmITN(GCDJ);
      ConfirmBasicSize2(BasicSize);
      Data2.ADOTable3.First;
      if Data2.ADOTable3.Locate('基本尺寸',BasicSize2,[loPartialKey]) then
        begin
          eiz := Data2.ADOTable3.FieldByName(a).AsFloat/1000;
        end;
      esz :=eiz+ITN;
      ConfirmPlusSign2(esz,eiz);
  end;

  //定义一个确定各个CheckBox是否被选中的过程
  procedure ConfirmCheck(a :Double);
  begin
    if  a>=0 then
      begin
        RadioButton3.Checked :=true;
        RadioButton4.Checked :=false;
        RadioButton5.Checked :=false;
      end
    else
      begin
        RadioButton5.Checked :=true;
        RadioButton3.Checked :=false;
        RadioButton4.Checked :=false;
      end;
  end;
begin
  if (Edit1.Text = '') or (StrToFloat(Edit1.Text)<=0) or (StrToFloat(Edit1.Text)>3150) then
  begin
    BasicSize :=0;
    MessageBox (
      HWND (0),
      LPCTSTR ('本查询只限于0到3150之间的基本尺寸'),	// address of text in message box
      LPCTSTR ('提示'),	// address of title of message box
      UINT (MB_OK)
      );
    Edit1.SetFocus;
    Edit1.SelectAll;
    Label2.Caption :='';
  end

  else if (StrToFloat(Edit1.Text)>0) and (StrToFloat(Edit1.Text)<3150) then
  begin
  BasicSize := StrToFloat(Edit1.Text);
  Label2.Caption :=Edit1.Text;
  end

  else if (RadioButton1.Checked=False)  and (RadioButton2.Checked=False) then
  begin
    MessageBox (
      HWND (0),
      LPCTSTR ('请选择基准制'),
      LPCTSTR ('提示'),
      UINT (MB_OK)
    );
  end;

  if (RadioButton3.Checked=False) and (RadioButton4.Checked=False) and (RadioButton5.Checked=False) then
  begin
    MessageBox (
      HWND (0),
      LPCTSTR ('请选择配合方式'),
      LPCTSTR ('提示'),
      UINT (MB_OK)
    );
    RadioButton3.Checked :=true;
  end
  else
  begin
    if (ComboBox1.Text='') then
    begin 
      MessageBox (
      HWND (0),
      LPCTSTR ('请输入孔公差代号'),
      LPCTSTR ('提示'),
      UINT (MB_OK)
    );
    ComboBox1.SetFocus;
    //Label22.Visible:=true;
    //Label22.Caption:='尚未输入孔公差代号';
    end
    else if (ComboBox3.Text='') then
    begin
      MessageBox (
      HWND (0),
      LPCTSTR ('请输入轴公差代号'),
      LPCTSTR ('提示'),
      UINT (MB_OK)
    );
    ComboBox3.SetFocus;
    //Label22.Visible:=true;
    //Label22.Caption:='尚未输入轴公差代号';
    end;
  end;

  Label3.Caption:=ComboBox1.Text+ComboBox2.Text;  //Label3显示上偏差的公差带和公差等级
  Label6.Caption:=ComboBox3.Text+ComboBox4.Text;  //Label6显示下偏差的公差带和公差等级
  KongGCD:=ComboBox1.Text;
  KongGCDJ:=StrtoInt(ComboBox2.Text);
  ZhouGCD:=ComboBox3.Text;
  ZhouGCDJ:=StrtoInt(ComboBox4.Text);

//孔的极限偏差的计算
  //通用规则中的第一种情况
  if (KongGCD>='A') and (KongGCD<='H') then    //孔公差带在A到H之间的情况
  begin
    SearchRule1(KongGCD);
  end

  else if (KongGCD='Js') or (KongGCD='JS') then                  //孔公差带为Js的情况
  begin
    ConfirmGCDJ(KongGCDJ);
    ConfirmITN(GCDJ);

    if (KongGCDJ>=7) and (KongGCDJ<=11) and ((round(ITN*1000) mod 2)=1) then   //JS7到JS11中如果ITn值为奇数，偏差为正负（ITn-1）/2
    begin
      //showmessage(floattostr(ITN*1000));
      ESK :=(ITN*1000-1)/2000;
      EIK :=-(ITN*1000-1)/2000;
    end
    else
    begin
      if ITN>=0 then
      begin
        ESK :=ITN/2;
        EIK :=-ITN/2;
      end
      else
      begin
        ESK :=-ITN/2;
        EIK :=ITN/2;
      end;
    end;
    ConfirmPlusSign(ESK,EIK);
  end

  //通用规则中的第二种情况
  else if (KongGCD='J') and (KongGCDJ<=6) then      //孔公差带为J的情况
    SearchRule2('J6')
  else if (KongGCD='J') and (KongGCDJ=7) then
    SearchRule2('J7')
  else if (KongGCD='J') and (KongGCDJ>=8) then
    SearchRule2('J8')

  else if (KongGCD>='K') and (KongGCD<='N') then    //孔公差带为K~N的情况
  begin
    if (KongGCDJ<=3) then
      SearchRule2(KongGCD+IntToStr(3))
    else if (KongGCDJ>=4) and (KongGCDJ<=8) then
      SearchRule2(KongGCD+IntToStr(KongGCDJ))
    else if (KongGCDJ>=9) then
      SearchRule2(KongGCD+IntToStr(9))
  end

  else if (KongGCD>='P') and (KongGCD<='Z') then    //孔公差带为P~Z的情况
  begin
    if (KongGCDJ<=3) then
      SearchRule2(KongGCD+IntToStr(3))
    else if (KongGCDJ>=4) and (KongGCDJ<=7) then
      SearchRule2(KongGCD+IntToStr(KongGCDJ))
    else if (KongGCDJ>=8) then
      SearchRule2(KongGCD+IntToStr(8))
  end

  else if (KongGCD='ZB') or (KongGCD='ZC') then
  begin
    SearchRule2(KongGCD);
  end

  else if (KongGCD='ZA')then
  begin
    if (KongGCDJ>=8) and (KongGCDJ<=11) then
      SearchRule2(KongGCD)
    else if (KongGCDJ=6) then
      SearchRule2('ZA6')
    else if (KongGCDJ=7) then
      SearchRule2('ZA7')
  end

  //通用规则中的第三种情况
  else if (KongGCD='N') and (BasicSize>3) and (KongGCDJ>8) then
  begin
    ESK :=0;
    ConfirmGCDJ(KongGCDJ);
    EIK :=-ConfirmITN(GCDJ);;
  end;


//轴的极限偏差的计算
  if (ZhouGCD>='a') and (ZhouGCD<='h') then         //轴公差带为a~h的情况
    SearchRule3(ZhouGCD)

  else if (ZhouGCD='js') then                       //轴公差带为js的情况
  begin
    ConfirmGCDJ(ZhouGCDJ);
    ConfirmITN(GCDJ);
    if ITN>=0 then
    begin
      esz :=ITN/2;
      eiz :=-ITN/2;
    end
    else
    begin
      esz :=-ITN/2;
      eiz :=ITN/2;
    end;
    ConfirmPlusSign2(esz,eiz);
  end

  else if (ZhouGCD='j') and (ZhouGCDJ<=5) then      //轴公差带为j的情况
    SearchRule4('j5')
  else if (ZhouGCD='j') and (ZhouGCDJ=6) then
    SearchRule4('j6')
  else if (ZhouGCD='j') and (ZhouGCDJ=7) then
    SearchRule4('j7')
  else if (ZhouGCD='j') and (ZhouGCDJ>7) then
    SearchRule4('j8')
  else if (ZhouGCD='k') and ((ZhouGCDJ>=4) and (ZhouGCDJ<=7)) then   //轴公差带为k的情况
    SearchRule4('k47')
  else if (ZhouGCD='k') and ((ZhouGCDJ<=3) or (ZhouGCDJ>=8)) then
    SearchRule4('k38')
  else if (ZhouGCD>='m') and (ZhouGCD<='z') or (ZhouGCD='za') or (ZhouGCD='zb') or (ZhouGCD='zc') then    //轴公差带为m~z的情况
    SearchRule4(ZhouGCD);

//配合方式的确定
  if ((RadioButton3.Checked=true) or (RadioButton4.Checked=true) or (RadioButton5.Checked=true)) and
      ((ComboBox1.Text<>'') and (ComboBox2.Text<>'') and (ComboBox3.Text<>'') and (ComboBox4.Text<>'')) then
  begin
    if ((EIK-esz)>=0) then               //如果孔的最小尺寸减去轴的最大尺寸都大于0，则为间隙配合
    begin
      if (RadioButton3.Checked=false) then
      begin
        Label21.Caption:='该配合为间隙配合';
        Label21.Visible:=true;
      end
      else
        Label21.Visible:=false;
    end
    else if ((ESK-eiz)<0) then      //如果孔的最大尺寸减去轴的最小尺寸都小于0，则为过盈配合
    begin
      if (RadioButton5.Checked=false) then
      begin
        Label21.Caption:='该配合为过盈配合';
        Label21.Visible:=true;
      end
      else
        Label21.Visible:=false;
    end
    else                          //其他情况为过渡配合
    begin
      if (RadioButton4.Checked=false) then
      begin
        Label21.Caption:='该配合为过渡配合';
        Label21.Visible:=true;
      end
      else
        Label21.Visible:=false;
    end;
  end;

  //基孔制特例
  if ((BasicSize<=3.0) and (KongGCD='H') and (KongGCDJ=6) and (ZhouGCD='n') and (ZhouGCDJ=5)) or
     ((BasicSize<=3.0) and (KongGCD='H') and (KongGCDJ=7) and (ZhouGCD='p') and (ZhouGCDJ=6)) or
     ((BasicSize<=100.0) and (KongGCD='H') and (KongGCDJ=8) and (ZhouGCD='r') and (ZhouGCDJ=7))
  then
    begin
      RadioButton5.Checked :=False;
      StatusBar1.Panels[0].Text :='特例，在此情况下为过渡配合';
    end;

  {if ESK>0 then
    label4.caption:='+'+Format('%3.3f', [ESK])
  else if ESK=0 then
    label4.caption:=' '+Format('%3.3f', [ESK])
  else
    label4.caption:=Format('%3.3f', [ESK]);

  if EIK>0 then
    label5.caption:='+'+Format('%3.3f', [EIK])
  else if EIK=0 then
    label5.caption:=' '+Format('%3.3f', [EIK])
  else
    label5.caption:=Format('%3.3f', [EIK]);

  if esz>0 then
    label7.caption:='+'+Format('%3.3f', [esz])
  else if esz=0 then
    label7.caption:=' '+Format('%3.3f', [esz])
  else
    label7.caption:=Format('%3.3f', [esz]);

  if eiz>0 then
    label8.caption:='+'+Format('%3.3f', [eiz])
  else if eiz=0 then
    label8.caption:=' '+Format('%3.3f', [eiz])
  else
    label8.caption:=Format('%3.3f', [eiz]);}

  Label22.Visible:=false;  

  if Label4.Caption='0' then
  begin
    Label4.Caption:=' 0';
  end;
  if Label5.Caption='0' then
  begin
    Label5.Caption:=' 0';
  end;
  if Label7.Caption='0' then
  begin
    Label7.Caption:=' 0';
  end;
  if Label8.Caption='0' then
  begin
    Label8.Caption:=' 0';
  end;
end;

procedure Tfrmgcph.ComboBox1Change(Sender: TObject);
begin
  if (ComboBox1.Text >= 'a') and (ComboBox1.Text <= 'h') or
     (ComboBox1.Text >= 'j') and (ComboBox1.Text <= 'z') then
  begin
    ComboBox1.Text :=UpperCase(ComboBox1.Text);
  end

  else if (ComboBox1.Text <>'A') and (ComboBox1.Text <>'B') and (ComboBox1.Text <>'C') and (ComboBox1.Text <>'CD') and
     (ComboBox1.Text <>'D') and (ComboBox1.Text <>'E') and (ComboBox1.Text <>'EF') and (ComboBox1.Text <>'F') and
     (ComboBox1.Text <>'FG') and (ComboBox1.Text <>'G') and (ComboBox1.Text <>'H') and (ComboBox1.Text <>'Js') and (ComboBox1.Text <>'JS') and
     (ComboBox1.Text <>'J') and (ComboBox1.Text <>'K') and (ComboBox1.Text <>'M') and
     (ComboBox1.Text <>'N') and (ComboBox1.Text <>'P') and (ComboBox1.Text <>'R') and
     (ComboBox1.Text <>'S') and (ComboBox1.Text <>'T') and (ComboBox1.Text <>'U') and
     (ComboBox1.Text <>'V') and (ComboBox1.Text <>'X') and (ComboBox1.Text <>'Y') and
     (ComboBox1.Text <>'Z') and (ComboBox1.Text <>'ZA') and (ComboBox1.Text <>'ZB') and (ComboBox1.Text <>'ZC') then
  begin
    MessageBox (
    HWND (0),
    LPCTSTR ('请输入A到Z之间的字母作为孔公差带'),	// address of text in message box
    LPCTSTR ('提示'),	// address of title of message box
    UINT (MB_OK)
    );
    ComboBox1.Text :='H';
    ComboBox1.SetFocus;
    ComboBox1.SelectAll;
  end;

  Label3.Caption:='';
  Label4.Caption:='';
  Label5.Caption:='';
  Label6.Caption:='';
  Label7.Caption:='';
  Label8.Caption:='';

  if (ComboBox1.Text='C') or (ComboBox1.Text='E') or (ComboBox1.Text='F') or (ComboBox1.Text='J') or (ComboBox1.Text='Z') then
  begin
    Label22.Visible:=true;
    Label22.Caption:='输入CD/EF/FG/JS/ZA/ZB/ZC时，请用大写';
  end
  else
    Label22.Visible:=false;

end;

procedure Tfrmgcph.ComboBox3Change(Sender: TObject);
begin
  if (ComboBox3.Text >= 'A') and (ComboBox3.Text <= 'H') or
     (ComboBox3.Text >= 'J') and (ComboBox3.Text <= 'Z')  then
  begin
    ComboBox3.Text :=LowerCase(ComboBox3.Text);
  end

  else if (ComboBox3.Text <>'a') and (ComboBox3.Text <>'b') and (ComboBox3.Text <>'c') and (ComboBox3.Text <>'cd') and
     (ComboBox3.Text <>'d') and (ComboBox3.Text <>'e') and (ComboBox3.Text <>'ef') and (ComboBox3.Text <>'f') and
     (ComboBox3.Text <>'fg') and (ComboBox3.Text <>'g') and (ComboBox3.Text <>'h') and (ComboBox3.Text <>'js') and
     (ComboBox3.Text <>'j') and (ComboBox3.Text <>'k') and (ComboBox3.Text <>'m') and
     (ComboBox3.Text <>'n') and (ComboBox3.Text <>'p') and (ComboBox3.Text <>'r') and
     (ComboBox3.Text <>'s') and (ComboBox3.Text <>'t') and (ComboBox3.Text <>'u') and
     (ComboBox3.Text <>'v') and (ComboBox3.Text <>'x') and (ComboBox3.Text <>'y') and
     (ComboBox3.Text <>'z') and (ComboBox3.Text <>'za') and (ComboBox3.Text <>'zb') and (ComboBox3.Text <>'zc') then
  begin
    MessageBox (
    HWND (0),
    LPCTSTR ('请输入a到z之间的字母作为轴公差带'),	// address of text in message box
    LPCTSTR ('提示'),	// address of title of message box
    UINT (MB_OK)
    );
    ComboBox3.Text :='f';
    ComboBox3.SetFocus;
    ComboBox3.SelectAll;
  end;

  Label3.Caption:='';
  Label4.Caption:='';
  Label5.Caption:='';
  Label6.Caption:='';
  Label7.Caption:='';
  Label8.Caption:='';

  if (ComboBox3.Text='c') or (ComboBox3.Text='e') or (ComboBox3.Text='f') or (ComboBox3.Text='j') or (ComboBox3.Text='z') then
  begin
    Label22.Visible:=true;
    Label22.Caption:='输入cd/ef/fg/js/za/zb/zc时，请用小写';
  end
  else
    Label22.Visible:=false;

end;

procedure Tfrmgcph.ComboBox2Change(Sender: TObject);
begin
  if RadioButton1.Checked and RadioButton3.Checked then
    begin
    ComboBox3.Items.Clear;
    ComboBox3.Text := '';
    ComboBox4.Items.Clear;
    if ComboBox2.Text='6' then
      begin
      ComboBox3.Items.Append('f');
      ComboBox3.Items.Append('g');
      ComboBox3.Items.Append('h');
      ComboBox4.Text:='5';
      end
    else if ComboBox2.Text='7' then
      begin
      ComboBox3.Items.Append('f');
      ComboBox3.Items.Append('g');
      ComboBox3.Items.Append('h');
      ComboBox4.Text:='6';
      end
    else if ComboBox2.Text='8' then
      begin
      ComboBox3.Items.Append('d');
      ComboBox3.Items.Append('e');
      ComboBox3.Items.Append('f');
      ComboBox3.Items.Append('g');
      ComboBox3.Items.Append('h');
      ComboBox4.Text:='7';
      ComboBox4.Items.Append('7');
      ComboBox4.Items.Append('8');
      end
    else if ComboBox2.Text='9' then
      begin
      ComboBox3.Items.Append('c');
      ComboBox3.Items.Append('d');
      ComboBox3.Items.Append('e');
      ComboBox3.Items.Append('f');
      ComboBox3.Items.Append('h');
      ComboBox4.Text:='9';
      end
    else if ComboBox2.Text='10' then
      begin
      ComboBox3.Items.Append('c');
      ComboBox3.Items.Append('d');
      ComboBox3.Items.Append('h');
      ComboBox4.Text:='10';
      end
    else if ComboBox2.Text='11' then
      begin
      ComboBox3.Items.Append('a');
      ComboBox3.Items.Append('b');
      ComboBox3.Items.Append('c');
      ComboBox3.Items.Append('d');
      ComboBox3.Items.Append('h');
      ComboBox4.Text:='11';
      end
    else if ComboBox2.Text='12' then
      begin
      ComboBox3.Items.Append('b');
      ComboBox3.Items.Append('h');
      ComboBox4.Text:='12';
      end;
  end;
  if RadioButton1.Checked and RadioButton4.Checked then
    begin
    ComboBox3.Items.Clear;
    ComboBox3.Text := '';
    ComboBox4.Items.Clear;
    if ComboBox2.Text='6' then
      begin
      ComboBox3.Items.Append('js');
      ComboBox3.Items.Append('k');
      ComboBox3.Items.Append('m');
      ComboBox4.Text:='5';
      end
    else if ComboBox2.Text='7' then
      begin
      ComboBox3.Items.Append('js');
      ComboBox3.Items.Append('k');
      ComboBox3.Items.Append('m');
      ComboBox3.Items.Append('n');
      ComboBox4.Text:='6';
      end
    else if ComboBox2.Text='8' then
      begin
      ComboBox3.Items.Append('js');
      ComboBox3.Items.Append('k');
      ComboBox3.Items.Append('m');
      ComboBox3.Items.Append('n');
      ComboBox3.Items.Append('p');
      ComboBox4.Text:='7';
      end
  end;
  if RadioButton1.Checked and RadioButton5.Checked then
    begin
    ComboBox3.Items.Clear;
    ComboBox3.Text := '';
    ComboBox4.Items.Clear;
    if ComboBox2.Text='6' then
      begin
      ComboBox3.Items.Append('n');
      ComboBox3.Items.Append('p');
      ComboBox3.Items.Append('r');
      ComboBox3.Items.Append('s');
      ComboBox3.Items.Append('t');
      ComboBox4.Text:='5';
      end
    else if ComboBox2.Text='7' then
      begin
      ComboBox3.Items.Append('p');
      ComboBox3.Items.Append('r');
      ComboBox3.Items.Append('s');
      ComboBox3.Items.Append('t');
      ComboBox3.Items.Append('u');
      ComboBox3.Items.Append('v');
      ComboBox3.Items.Append('x');
      ComboBox3.Items.Append('y');
      ComboBox3.Items.Append('z');
      ComboBox4.Text:='6';
      end
    else if ComboBox2.Text='8' then
      begin
      ComboBox3.Items.Append('r');
      ComboBox3.Items.Append('s');
      ComboBox3.Items.Append('t');
      ComboBox3.Items.Append('u');
      ComboBox4.Text:='7';
      end
  end;

  if (ComboBox2.Text <>'1') and (ComboBox2.Text <>'2') and (ComboBox2.Text <>'3') and
      (ComboBox2.Text <>'4') and (ComboBox2.Text <>'5') and (ComboBox2.Text <>'6') and
      (ComboBox2.Text <>'7') and (ComboBox2.Text <>'8') and (ComboBox2.Text <>'9') and
      (ComboBox2.Text <>'10') and (ComboBox2.Text <>'11') and (ComboBox2.Text <>'12') and
      (ComboBox2.Text <>'13') and (ComboBox2.Text <>'14') and (ComboBox2.Text <>'15') and
      (ComboBox2.Text <>'16') and (ComboBox2.Text <>'17') and (ComboBox2.Text <>'18') then
  begin
    MessageBox (
    HWND (0),
    LPCTSTR ('请输入1到18之间的整数'),	// address of text in message box
    LPCTSTR ('提示'),	// address of title of message box
    UINT (MB_OK)
    );
    ComboBox2.Text :='6';
    ComboBox2.SetFocus;
    ComboBox2.SelectAll;
  end;

  Label3.Caption:='';
  Label4.Caption:='';
  Label5.Caption:='';
  Label6.Caption:='';
  Label7.Caption:='';
  Label8.Caption:='';
end;

procedure Tfrmgcph.ComboBox4Change(Sender: TObject);
begin
  if RadioButton2.Checked and RadioButton3.Checked then
    begin
    ComboBox1.Items.Clear;
    ComboBox1.Text := '';
    ComboBox2.Items.Clear;
    if ComboBox4.Text='5' then
      begin
      ComboBox1.Items.Append('F');
      ComboBox1.Items.Append('G');
      ComboBox1.Items.Append('H');
      ComboBox2.Text:='6';
      end
    else if ComboBox4.Text='6' then
      begin
      ComboBox1.Items.Append('F');
      ComboBox1.Items.Append('G');
      ComboBox1.Items.Append('H');
      ComboBox2.Text:='7';
      end
    else if ComboBox4.Text='7' then
      begin
      ComboBox1.Items.Append('E');
      ComboBox1.Items.Append('F');
      ComboBox1.Items.Append('H');
      ComboBox2.Text:='8';
      end
    else if ComboBox4.Text='8' then
      begin
      ComboBox1.Items.Append('D');
      ComboBox1.Items.Append('E');
      ComboBox1.Items.Append('F');
      ComboBox1.Items.Append('H');
      ComboBox2.Text:='8';
      end
    else if ComboBox4.Text='9' then
      begin
      ComboBox1.Items.Append('D');
      ComboBox1.Items.Append('E');
      ComboBox1.Items.Append('F');
      ComboBox1.Items.Append('H');
      ComboBox2.Text:='9';
      end
    else if ComboBox4.Text='10' then
      begin
      ComboBox1.Items.Append('D');
      ComboBox1.Items.Append('H');
      ComboBox2.Text:='10';
      end
    else if ComboBox4.Text='11' then
      begin
      ComboBox1.Items.Append('A');
      ComboBox1.Items.Append('B');
      ComboBox1.Items.Append('C');
      ComboBox1.Items.Append('D');
      ComboBox1.Items.Append('H');
      ComboBox2.Text:='11';
      end
    else if ComboBox4.Text='12' then
      begin
      ComboBox1.Items.Append('B');
      ComboBox1.Items.Append('H');
      ComboBox2.Text:='12';
      end
  end;
  if RadioButton2.Checked and RadioButton4.Checked then
    begin
    ComboBox1.Items.Clear;
    ComboBox1.Text := '';
    ComboBox2.Items.Clear;
    if ComboBox4.Text='5' then
      begin
      ComboBox1.Items.Append('Js');
      ComboBox1.Items.Append('K');
      ComboBox1.Items.Append('M');
      ComboBox2.Text:='6';
      end
    else if ComboBox4.Text='6' then
      begin
      ComboBox1.Items.Append('Js');
      ComboBox1.Items.Append('K');
      ComboBox1.Items.Append('M');
      ComboBox1.Items.Append('N');
      ComboBox2.Text:='6';
      ComboBox2.Items.Append('6');
      ComboBox2.Items.Append('7');
      end
    else if ComboBox4.Text='7' then
      begin
      ComboBox1.Items.Append('Js');
      ComboBox1.Items.Append('K');
      ComboBox1.Items.Append('M');
      ComboBox1.Items.Append('N');
      ComboBox2.Text:='8';
      end
  end;
  if RadioButton2.Checked and RadioButton5.Checked then
    begin
    ComboBox1.Items.Clear;
    ComboBox1.Text := '';
    ComboBox2.Items.Clear;
    if ComboBox4.Text='5' then
      begin
      ComboBox1.Items.Append('N');
      ComboBox1.Items.Append('P');
      ComboBox1.Items.Append('R');
      ComboBox1.Items.Append('S');
      ComboBox1.Items.Append('T');
      ComboBox2.Text:='6';
      end
    else if ComboBox4.Text='6' then
      begin
      ComboBox1.Items.Append('P');
      ComboBox1.Items.Append('R');
      ComboBox1.Items.Append('S');
      ComboBox1.Items.Append('T');
      ComboBox1.Items.Append('U');
      ComboBox2.Text:='7';
      end
    end;

  if (ComboBox4.Text <>'1') and (ComboBox4.Text <>'2') and (ComboBox4.Text <>'3') and
      (ComboBox4.Text <>'4') and (ComboBox4.Text <>'5') and (ComboBox4.Text <>'6') and
      (ComboBox4.Text <>'7') and (ComboBox4.Text <>'8') and (ComboBox4.Text <>'9') and
      (ComboBox4.Text <>'10') and (ComboBox4.Text <>'11') and (ComboBox4.Text <>'12') and
      (ComboBox4.Text <>'13') and (ComboBox4.Text <>'14') and (ComboBox4.Text <>'15') and
      (ComboBox4.Text <>'16') and (ComboBox4.Text <>'17') and (ComboBox4.Text <>'18') then
  begin
    MessageBox (
    HWND (0),
    LPCTSTR ('请输入1到18之间的整数'),	// address of text in message box
    LPCTSTR ('提示'),	// address of title of message box
    UINT (MB_OK)
    );
    ComboBox4.Text :='5';
    ComboBox4.SetFocus;
    ComboBox4.SelectAll;
  end;

  Label3.Caption:='';
  Label4.Caption:='';
  Label5.Caption:='';
  Label6.Caption:='';
  Label7.Caption:='';
  Label8.Caption:='';
end;

procedure Tfrmgcph.FormShow(Sender: TObject);
var prj_path : string;
begin
  prj_path:=ExtractFilePath(ParamStr(0));
  {T_Tree.DatabaseName:= prj_path+'database';
  Q_Tree.DatabaseName:= prj_path+'database';
  QF_Tree.DatabaseName:= prj_path+'database';
  Query1.DatabaseName:=prj_path+'database';
  T_History.DatabaseName:=prj_path+'database';
  TreeM.DatabaseName:=prj_path+'database';
  tBook.DatabaseName:=prj_path+'database';
  tblView.DatabaseName:=prj_path+'dat'; }
  data2.ADOConnection1.ConnectionString:='Provider=Microsoft.Jet.OLEDB.4.0;Data Source='+prj_path+'\DATAS\cooper.mdb;Persist Security Info=False';

  data2.ADOConnection1.Connected:=True;
  data2.ADOTable1.Open;
  data2.ADOTable2.Open;
  data2.ADOTable3.Open;
end;



procedure Tfrmgcph.RadioButton3Click(Sender: TObject);
begin
  RadioButton4.Checked :=False;
  RadioButton5.Checked :=False;
  Memo1.Visible:=true;
  Memo2.Visible:=false;
  Memo3.Visible:=false;
  Label21.Visible:=False;
  Label22.Visible:=False;

  if RadioButton1.Checked=true and RadioButton3.Checked=true then
  begin
    ComboBox1.Text:='H';
    ComboBox2.SetFocus;
    ComboBox2.text:='6';
    ComboBox2.Items.Clear;
    ComboBox2.Items.Append('6');
    ComboBox2.Items.Append('7');
    ComboBox2.Items.Append('8');
    ComboBox2.Items.Append('9');
    ComboBox2.Items.Append('10');
    ComboBox2.Items.Append('11');
    ComboBox2.Items.Append('12');
    ComboBox2.Text:='6';
    ComboBox2Change(nil);
    ComboBox3.Text :='';
    ComboBox4.Text :='5';
  end;
  if RadioButton2.Checked and RadioButton3.Checked then
  begin
    ComboBox3.Text:='h';
    ComboBox4.SetFocus;
    ComboBox4.text:='5';
    ComboBox4.Items.Clear;
    ComboBox4.Items.Append('5');
    ComboBox4.Items.Append('6');
    ComboBox4.Items.Append('7');
    ComboBox4.Items.Append('8');
    ComboBox4.Items.Append('9');
    ComboBox4.Items.Append('10');
    ComboBox4.Items.Append('11');
    ComboBox4.Items.Append('12');
    ComboBox4.Text:='5';
    ComboBox4Change(nil);
    ComboBox1.Text :='';
    ComboBox2.Text :='6';
  end;

  Label3.Caption:='';
  Label4.Caption:='';
  Label5.Caption:='';
  Label6.Caption:='';
  Label7.Caption:='';
  Label8.Caption:='';
end;

procedure Tfrmgcph.RadioButton4Click(Sender: TObject);
begin
  RadioButton3.Checked :=False;
  RadioButton5.Checked :=False;
  Memo2.Visible:=true;
  Memo1.Visible:=false;
  Memo3.Visible:=false;
  Label21.Visible:=False;
  Label22.Visible:=False;

  if RadioButton1.Checked and RadioButton4.Checked then
  begin
    ComboBox1.Text:='H';
    ComboBox2.SetFocus;
    ComboBox2.text:='6';
    ComboBox2.Items.Clear;
    ComboBox2.Items.Append('6');
    ComboBox2.Items.Append('7');
    ComboBox2.Items.Append('8');
    ComboBox2.Text:='6';
    ComboBox2Change(nil);
    ComboBox3.Text :='';
    ComboBox4.Text :='5';
  end;
  if RadioButton2.Checked and RadioButton4.Checked then
  begin
    ComboBox3.Text:='h';
    ComboBox4.SetFocus;
    ComboBox4.text:='5';
    ComboBox4.Items.Clear;
    ComboBox4.Items.Append('5');
    ComboBox4.Items.Append('6');
    ComboBox4.Items.Append('7');
    ComboBox4.Text:='5';
    ComboBox4Change(nil);
    ComboBox1.Text :='';
    ComboBox2.Text :='6';
  end;

  Label3.Caption:='';
  Label4.Caption:='';
  Label5.Caption:='';
  Label6.Caption:='';
  Label7.Caption:='';
  Label8.Caption:='';
end;

procedure Tfrmgcph.RadioButton5Click(Sender: TObject);
begin
  RadioButton3.Checked :=False;
  RadioButton4.Checked :=False;
  Memo3.Visible:=true;
  Memo1.Visible:=false;
  Memo2.Visible:=false;
  Label21.Visible:=False;
  Label22.Visible:=False;

  if RadioButton1.Checked and RadioButton5.Checked then
  begin
    ComboBox1.Text:='H';
    ComboBox2.SetFocus;
    ComboBox2.text:='6';
    ComboBox2.Items.Clear;
    ComboBox2.Items.Append('6');
    ComboBox2.Items.Append('7');
    ComboBox2.Items.Append('8');
    ComboBox2.Text:='6';
    ComboBox2Change(nil);
    ComboBox3.Text :='';
    ComboBox4.Text :='5';
  end;
  if RadioButton2.Checked and RadioButton5.Checked then
  begin
    ComboBox3.Text:='h';
    ComboBox4.SetFocus;
    ComboBox4.text:='5';
    ComboBox4.Items.Clear;
    ComboBox4.Items.Append('5');
    ComboBox4.Items.Append('6');
    ComboBox4.Text:='5';
    ComboBox4Change(nil);
    ComboBox1.Text :='';
    ComboBox2.Text :='6';
  end;

  Label3.Caption:='';
  Label4.Caption:='';
  Label5.Caption:='';
  Label6.Caption:='';
  Label7.Caption:='';
  Label8.Caption:='';
end;

procedure Tfrmgcph.BitBtn2Click(Sender: TObject);
var
  MyResultFile: TextFile;
  S1: String;
  S2: String;
  S3: String;
  S4: String;
  S5: String;
  S6: String;
  S7: String;
  S8: String;
  i: integer;
  label LBL_SAVEFILE;
begin
  if RadioButton1.Checked=true then
    S7:='基孔制'
  else if RadioButton2.Checked=true then
    S7:='基轴制';
  if RadioButton3.Checked=true then
    S8:='间隙配合'
  else if RadioButton4.Checked=true then
    S8:='过渡配合'
  else if RadioButton5.Checked=true then
    S8:='过盈配合';

  LBL_SAVEFILE:
  if SaveDialog1.Execute then
  begin
    if FileExists(SaveDialog1.FileName) then
      if MessageDlg('您要覆盖同名文件吗' + ExtractFileName(SaveDialog1.FileName) + '?', mtConfirmation, [mbYes,mbNo],0) = IDNO then
         goto LBL_SAVEFILE;
  end
  else exit;

  if RadioButton7.Checked=true then
  begin
    S1 :='基本尺寸为'+Edit1.Text+'mm，'+S7+S8;
    S2 :='查询结果为： ';
    S3 :='  孔：上偏差：'+Label4.Caption+'；   ';
    S4 :='      下偏差：'+Label5.Caption+'；   ';
    S5 :='  轴：上偏差：'+Label7.Caption+'；   ';
    S6 :='      下偏差：'+Label8.Caption+'；   ';

    AssignFile(MyResultFile,SaveDialog1.FileName);
    Rewrite(MyResultFile);
    try
      for i:=1 to 1 do
      begin
        Writeln(MyResultFile,S1);
      end;
      for i:=2 to 2 do
      begin
        Writeln(MyResultFile,S2);
      end;
      for i:=3 to 3 do
      begin
        Writeln(MyResultFile,S3);
      end;
      for i:=4 to 4 do
      begin
        Writeln(MyResultFile,S4);
      end;
      for i:=5 to 5 do
      begin
        Writeln(MyResultFile,S5);
      end;
      for i:=6 to 6 do
      begin
        Writeln(MyResultFile,S6);
      end;
    finally
      CloseFile(MyResultFile);
    end;
  end

  else if RadioButton6.Checked=true then
  begin
    if RadioButton8.Checked=true then
    begin
      S1 :='基本尺寸为'+Edit1.Text+'mm，';
      S2 :='查询结果为： ';
      S3 :='  孔：上偏差：'+Label4.Caption+'；   ';
      S4 :='      下偏差：'+Label5.Caption+'；   ';

      AssignFile(MyResultFile,SaveDialog1.FileName);
      Rewrite(MyResultFile);
      try
        for i:=1 to 1 do
        begin
          Writeln(MyResultFile,S1);
        end;
        for i:=2 to 2 do
        begin
          Writeln(MyResultFile,S2);
        end;
        for i:=3 to 3 do
        begin
          Writeln(MyResultFile,S3);
        end;
        for i:=4 to 4 do
        begin
          Writeln(MyResultFile,S4);
        end;
      finally
        CloseFile(MyResultFile);
      end;
    end
    else if RadioButton9.Checked=true then
    begin
      S1 :='基本尺寸为'+Edit1.Text+'mm，';
      S2 :='查询结果为： ';
      S5 :='  轴：上偏差：'+Label7.Caption+'；   ';
      S6 :='      下偏差：'+Label8.Caption+'；   ';

      AssignFile(MyResultFile,SaveDialog1.FileName);
      Rewrite(MyResultFile);
      try
        for i:=1 to 1 do
        begin
          Writeln(MyResultFile,S1);
        end;
        for i:=2 to 2 do
        begin
          Writeln(MyResultFile,S2);
        end;
        for i:=3 to 3 do
        begin
          Writeln(MyResultFile,S5);
        end;
        for i:=4 to 4 do
        begin
          Writeln(MyResultFile,S6);
        end;
        finally
          CloseFile(MyResultFile);
      end;
    end;
  end;
end;

procedure Tfrmgcph.BitBtn3Click(Sender: TObject);
begin
  if RadioButton6.Checked=true then
  begin
    Memo1.Visible:=false;
    Memo2.Visible:=false;
    Memo3.Visible:=false;
    {if RadioButton8.Checked=true then
    begin
      Label4.Caption:='';
      Label5.Caption:='';
    end
    else if RadioButton9.Checked=true then
    begin
      Label7.Caption:='';
      Label8.Caption:='';
    end;}
  end
  else if RadioButton7.Checked=true then
  begin
    RadioButton3.Checked:=true;
    RadioButton4.Checked:=false;
    RadioButton5.Checked:=false;
    {Label4.Caption:='0';
    Label5.Caption:='0';
    Label7.Caption:='0';
    Label8.Caption:='0';}
  end;

  Label3.Caption:='';
  Label4.Caption:='';
  Label5.Caption:='';
  Label6.Caption:='';
  Label7.Caption:='';
  Label8.Caption:='';
  Label21.Visible:=false;
end;

procedure Tfrmgcph.N3Click(Sender: TObject);
begin
  close();
end;

procedure Tfrmgcph.N2Click(Sender: TObject);
var
  MyResultFile: TextFile;
  S1: String;
  S2: String;
  S3: String;
  S4: String;
  S5: String;
  S6: String;
  S7: String;
  S8: String;
  i: integer;
  label LBL_SAVEFILE;
begin
  S1 :='基本尺寸为'+Edit1.Text+'mm，'+S7+S8;
  S2 :='查询结果为： ';
  S3 :='  孔：上偏差：'+Label4.Caption+'；   ';
  S4 :='      下偏差：'+Label5.Caption+'；   ';
  S5 :='  轴：上偏差：'+Label7.Caption+'；   ';
  S6 :='      下偏差：'+Label8.Caption+'；   ';
  if RadioButton1.Checked=true then
    S7:='基孔制'
  else if RadioButton2.Checked=true then
    S7:='基轴制';
  if RadioButton3.Checked=true then
    S8:='间隙配合'
  else if RadioButton4.Checked=true then
    S8:='过渡配合'
  else if RadioButton5.Checked=true then
    S8:='过盈配合';

  LBL_SAVEFILE:
  if SaveDialog1.Execute then
  begin
    if FileExists(SaveDialog1.FileName) then
      if MessageDlg('您要覆盖同名文件吗' + ExtractFileName(SaveDialog1.FileName) + '?', mtConfirmation, [mbYes,mbNo],0) = IDNO then
         goto LBL_SAVEFILE;
  end
  else exit;

  AssignFile(MyResultFile,SaveDialog1.FileName);
  Rewrite(MyResultFile);
  try
    for i:=1 to 1 do
    begin
      Writeln(MyResultFile,S1);
    end;
    for i:=2 to 2 do
    begin
      Writeln(MyResultFile,S2);
    end;
    for i:=3 to 3 do
    begin
      Writeln(MyResultFile,S3);
    end;
    for i:=4 to 4 do
    begin
      Writeln(MyResultFile,S4);
    end;
    for i:=5 to 5 do
    begin
      Writeln(MyResultFile,S5);
    end;
    for i:=6 to 6 do
    begin
      Writeln(MyResultFile,S6);
    end;
  finally
    CloseFile(MyResultFile);
  end;
end;

procedure Tfrmgcph.N4Click(Sender: TObject);
begin
  with OpenDialog1 do
  begin
    Filter:='文本文件(*.txt)|*.txt';
    DefaultExt:='txt';
    FileName:='';
    Options:=[ofHideReadOnly,ofFileMustExist,ofPathMustExist];
    if Execute then
      if ofExtensionDifferent in Options then
        MessageDlg('这不是文本文件！',mtError,[mbOk],0)
    else
      ShellExecute(handle, 'open',Pchar(OpenDialog1.FileName), nil, nil, SW_SHOWNORMAL);
    end;
end;

procedure Tfrmgcph.BitBtn4Click(Sender: TObject);
var
KongGCD:String;       //孔公差带
KongGCDJ:Integer;     //孔公差等级
ZhouGCD:String;       //轴公差带
ZhouGCDJ:Integer;     //轴公差等级

ESK:Double;           //孔上偏差
EIK:Double;           //孔下偏差
esz:Double;           //轴上偏差
eiz:Double;           //轴下偏差

GCDJ:String;          //公差等级
ITN:Double;           //标准公差数值
JianXiMin:Double;     //最小间隙

  //定义一个函数，以确定公差等级GCDJ，并返回其值
  function ConfirmGCDJ(a :Single) :String;
  begin
    GCDJ :='IT'+FloatToStr(a);
  end;

  //定义一个函数，以确定标准公差数值ITN，并返回其值
  function ConfirmITN(b :String) :Double;
  begin
    //确定Tabel1中的BasicSize的值，赋给BasicSize1，用于查询ITN
    Data2.ADOTable1.First;
    while not Data2.ADOTable1.Eof do
    begin
      if  BasicSize <=Data2.ADOTable1.FieldByName('基本尺寸').AsFloat then
      begin
        //Data.ADOTable1.Prior;
        BasicSize1 := Data2.ADOTable1.FieldByName('基本尺寸').AsFloat;
        Break;
      end;
      Data2.ADOTable1.Next;
    end;

    //根据选出的BasicSize的值查出标准公差数值ITN
    Data2.ADOTable1.First;
    if Data2.ADOTable1.Locate('基本尺寸',BasicSize1,[loPartialKey]) then
      begin
        ITN := Data2.ADOTable1.FieldByName(b).AsFloat/1000;
      end;
  end;

  //定义一个过程，用来确定Label4和Label5中数值的符号
  procedure ConfirmPlusSign(a :Double; b :Double);
  begin
  if (Edit1.Text = '') or (StrToFloat(Edit1.Text)<=0) or (StrToFloat(Edit1.Text)>3150) then
    begin
    Label4.Caption :='';
    Label5.Caption :='';
    end
  else
    begin
    if a>0 then
      Label4.Caption:='+'+FloatToStr(a)
    else
      Label4.Caption:=FloatToStr(a);
    if b>0 then
      Label5.Caption:='+'+FloatToStr(b)
    else
      Label5.Caption:=FloatToStr(b);
    end;
  end;

  //定义一个过程，用来确定Label7和Label8中数值的符号
  procedure ConfirmPlusSign2(a :Double; b :Double);
  begin
  if (Edit1.Text = '') or (StrToFloat(Edit1.Text)<=0) or (StrToFloat(Edit1.Text)>3150) then
    begin
    Label7.Caption :='';
    Label8.Caption :='';
    end
  else
    begin
    if a>0 then
      Label7.Caption :='+'+FloatToStr(a)
    else
      Label7.Caption :=FloatToStr(a);
    if b>0 then
      Label8.Caption :='+'+FloatToStr(b)
    else
      Label8.Caption :=FloatToStr(b);
    end;
  end;

  //由输入的BasicSize的值确定在Tabel2和Tabel3中的BasicSize的数值
  function ConfirmBasicSize2(a :Single) :Integer;
  begin
    Data2.ADOTable2.First;
    while not Data2.ADOTable1.Eof do
    begin
      //showmessage(Data2.ADOTable2.FieldByName('基本尺寸').AsString);
      if BasicSize <= Data2.ADOTable2.FieldByName('基本尺寸').AsFloat then
      begin
        //Data.ADOTable2.prior;
        BasicSize2 := Data2.ADOTable2.FieldByName('基本尺寸').AsFloat;
        Break;
      end;
      Data2.ADOTable2.Next;
    end;
    //showmessage(floattostr(BasicSize2));
  end;

  //定义一个查询过程SearchRule1,适用于孔的第一条通用规则
  procedure SearchRule1(a :string);
  begin
    ConfirmGCDJ(KongGCDJ);
    ConfirmITN(GCDJ);
    ConfirmBasicSize2(BasicSize);
    Data2.ADOTable2.First;
    if Data2.ADOTable2.Locate('基本尺寸',BasicSize2,[loPartialKey]) then
      begin
        EIK := Data2.ADOTable2.FieldByName(a).AsFloat/1000;
      end;
    ESK:=EIK+ITN;

    if Data2.ADOTable2.FieldByName(a).AsString='' then
    begin
      //showmessage('该处为空值');
      MessageBox (
      HWND (0),
      LPCTSTR ('该处为空值'),
      LPCTSTR ('提示'),
      UINT (MB_OK)
      );
      Label4.Caption:='';
      Label5.Caption:='';
      Label7.Caption:='';
      Label8.Caption:='';
    end
    else
    ConfirmPlusSign(ESK,EIK);
  end;

  //定义一个查询过程SearchRule2,适用于孔的第二条通用规则
  procedure SearchRule2(a :string);
  begin
    ConfirmGCDJ(KongGCDJ);
    ConfirmITN(GCDJ);
    ConfirmBasicSize2(BasicSize);
    Data2.ADOTable2.First;
    if Data2.ADOTable2.Locate('基本尺寸',BasicSize2,[loPartialKey]) then
      begin
        ESK := Data2.ADOTable2.FieldByName(a).AsFloat/1000;
      end;
    EIK:=ESK-ITN;

    if Data2.ADOTable2.FieldByName(a).AsString='' then
    begin
      //showmessage('该处为空值');
      MessageBox (
      HWND (0),
      LPCTSTR ('该处为空值'),
      LPCTSTR ('提示'),
      UINT (MB_OK)
      );
      Label4.Caption:='';
      Label5.Caption:='';
      Label7.Caption:='';
      Label8.Caption:='';
    end
    else
    ConfirmPlusSign(ESK,EIK);
  end;

  //定义一个查询过程SearchRule3,适用于公差等级为a~h的轴的偏差计算
  procedure SearchRule3(a :string);
  begin
      ConfirmGCDJ(ZhouGCDJ);
      ConfirmITN(GCDJ);
      ConfirmBasicSize2(BasicSize);
      Data2.ADOTable3.First;
      if Data2.ADOTable3.Locate('基本尺寸',BasicSize2,[loPartialKey]) then
        begin
          esz := Data2.ADOTable3.FieldByName(a).AsFloat/1000;
        end;
      eiz :=esz-ITN;

      if Data2.ADOTable3.FieldByName(a).AsString='' then
      begin
        //showmessage('该处为空值');
        MessageBox (
        HWND (0),
        LPCTSTR ('该处为空值'),
        LPCTSTR ('提示'),
        UINT (MB_OK)
        );
        Label4.Caption:='';
        Label5.Caption:='';
        Label7.Caption:='';
        Label8.Caption:='';
      end
      else
      ConfirmPlusSign2(esz,eiz);
  end;

  //定义一个查询过程SearchRule4,适用于公差等级为j~z的轴的偏差计算
  procedure SearchRule4(a :string);
  begin
      ConfirmGCDJ(ZhouGCDJ);
      ConfirmITN(GCDJ);
      ConfirmBasicSize2(BasicSize);
      Data2.ADOTable3.First;
      if Data2.ADOTable3.Locate('基本尺寸',BasicSize2,[loPartialKey]) then
        begin
          eiz := Data2.ADOTable3.FieldByName(a).AsFloat/1000;
        end;
      esz :=eiz+ITN;

      if Data2.ADOTable3.FieldByName(a).AsString='' then
      begin
        //showmessage('该处为空值');
        MessageBox (
        HWND (0),
        LPCTSTR ('该处为空值'),
        LPCTSTR ('提示'),
        UINT (MB_OK)
        );
        Label4.Caption:='';
        Label5.Caption:='';
        Label7.Caption:='';
        Label8.Caption:='';
      end
      else
        ConfirmPlusSign2(esz,eiz);
  end;
begin
  Label22.Visible:=false;

  if (Edit1.Text = '') or (StrToFloat(Edit1.Text)<=0) or (StrToFloat(Edit1.Text)>3150) then
  begin
    BasicSize :=0;
    MessageBox (
      HWND (0),
      LPCTSTR ('本查询只限于0到3150之间的基本尺寸'),	// address of text in message box
      LPCTSTR ('提示'),	// address of title of message box
      UINT (MB_OK)
      );
    Edit1.SetFocus;
    Edit1.SelectAll;
    Label2.Caption :='';
  end

  else if (StrToFloat(Edit1.Text)>0) and (StrToFloat(Edit1.Text)<3150) then
  begin
  BasicSize := StrToFloat(Edit1.Text);
  Label2.Caption :=Edit1.Text;
  end;

//查询孔公差的情况
  if RadioButton8.Checked=true then
  begin
    if (ComboBox1.Text='') then
    begin
      MessageBox (
        HWND (0),
        LPCTSTR ('请输入孔基本偏差代号'),
        LPCTSTR ('提示'),
        UINT (MB_OK)
      );
      ComboBox1.SetFocus;
    end
    else if (ComboBox1.Text<>'') and (ComboBox2.Text='') then
    begin
      MessageBox (
        HWND (0),
        LPCTSTR ('请输入孔公差等级'),
        LPCTSTR ('提示'),
        UINT (MB_OK)
      );
      ComboBox2.SetFocus;
    end;

    Label19.Caption:=Edit1.Text;
    Label3.Caption:=ComboBox1.Text+ComboBox2.Text;  //Label3显示上偏差的公差带和公差等级
    KongGCD:=ComboBox1.Text;
    KongGCDJ:=StrtoInt(ComboBox2.Text);

    //孔的极限偏差的计算
    //通用规则中的第一种情况
    if (KongGCD>='A') and (KongGCD<='H') then    //孔公差带在A到H之间的情况
    begin
      SearchRule1(KongGCD);
    end

    else if (KongGCD='Js') then                  //孔公差带为Js的情况
    begin
      ConfirmGCDJ(KongGCDJ);
      ConfirmITN(GCDJ);

      if (KongGCDJ>=7) and (KongGCDJ<=11) and ((round(ITN*1000) mod 2)=1) then   //JS7到JS11中如果ITn值为奇数，偏差为正负（ITn-1）/2
      begin
        //showmessage(floattostr(ITN*1000));
        ESK :=(ITN*1000-1)/2000;
        EIK :=-(ITN*1000-1)/2000;
      end
      else
      begin
        if ITN>=0 then
        begin
          ESK :=ITN/2;
          EIK :=-ITN/2;
        end
        else
        begin
          ESK :=-ITN/2;
          EIK :=ITN/2;
        end;
      end;
      ConfirmPlusSign(ESK,EIK);
    end

    //通用规则中的第二种情况
    else if (KongGCD='J') and (KongGCDJ<=6) then      //孔公差带为J的情况
      SearchRule2('J6')
    else if (KongGCD='J') and (KongGCDJ=7) then
      SearchRule2('J7')
    else if (KongGCD='J') and (KongGCDJ>=8) then
      SearchRule2('J8')

    else if (KongGCD>='K') and (KongGCD<='N') then    //孔公差带为K~N的情况
    begin
      if (KongGCDJ<=3) then
        SearchRule2(KongGCD+IntToStr(3))
      else if (KongGCDJ>=4) and (KongGCDJ<=8) then
        SearchRule2(KongGCD+IntToStr(KongGCDJ))
      else if (KongGCDJ>=9) then
        SearchRule2(KongGCD+IntToStr(9))
    end

    else if (KongGCD>='P') and (KongGCD<='Z') then    //孔公差带为P~Z的情况
    begin
      if (KongGCDJ<=3) then
        SearchRule2(KongGCD+IntToStr(3))
      else if (KongGCDJ>=4) and (KongGCDJ<=7) then
        SearchRule2(KongGCD+IntToStr(KongGCDJ))
      else if (KongGCDJ>=8) then
        SearchRule2(KongGCD+IntToStr(8))
    end

    else if (KongGCD='ZB') or (KongGCD='ZC') then
    begin
      SearchRule2(KongGCD);
    end

    else if (KongGCD='ZA')then
    begin
      if (KongGCDJ>=8) and (KongGCDJ<=11) then
        SearchRule2(KongGCD)
      else if (KongGCDJ=6) then
        SearchRule2('ZA6')
      else if (KongGCDJ=7) then
        SearchRule2('ZA7')
    end

    //通用规则中的第三种情况
    else if (KongGCD='N') and (BasicSize>3) and (KongGCDJ>8) then
    begin
      ESK :=0;
      ConfirmGCDJ(KongGCDJ);
      EIK :=-ConfirmITN(GCDJ);
    end;
    end
//查询轴公差的情况
    else if RadioButton9.Checked=true then
    begin
      if (ComboBox3.Text='') then
      begin
        MessageBox (
          HWND (0),
          LPCTSTR ('请输入轴基本偏差代号'),
          LPCTSTR ('提示'),
          UINT (MB_OK)
        );
        ComboBox3.SetFocus;
      end
      else if (ComboBox3.Text<>'') and (ComboBox4.Text='') then
      begin
        MessageBox (
          HWND (0),
          LPCTSTR ('请输入轴公差等级'),
          LPCTSTR ('提示'),
          UINT (MB_OK)
        );
        ComboBox4.SetFocus;
      end;
      Label20.Caption:=Edit1.Text;
      Label6.Caption:=ComboBox3.Text+ComboBox4.Text;  //Label6显示下偏差的公差带和公差等级
      ZhouGCD:=ComboBox3.Text;
      ZhouGCDJ:=StrtoInt(ComboBox4.Text);

      //轴的极限偏差的计算
      if (ZhouGCD>='a') and (ZhouGCD<='h') then         //轴公差带为a~h的情况
        SearchRule3(ZhouGCD)

      else if (ZhouGCD='js') then                       //轴公差带为js的情况
      begin
        ConfirmGCDJ(ZhouGCDJ);
        ConfirmITN(GCDJ);
        if ITN>=0 then
        begin
          esz :=ITN/2;
          eiz :=-ITN/2;
        end
        else
        begin
          esz :=-ITN/2;
          eiz :=ITN/2;
        end;
        ConfirmPlusSign2(esz,eiz);
      end

      else if (ZhouGCD='j') and (ZhouGCDJ<=5) then      //轴公差带为j的情况
        SearchRule4('j5')
      else if (ZhouGCD='j') and (ZhouGCDJ=6) then
        SearchRule4('j6')
      else if (ZhouGCD='j') and (ZhouGCDJ=7) then
        SearchRule4('j7')
      else if (ZhouGCD='j') and (ZhouGCDJ>7) then
        SearchRule4('j8')
      else if (ZhouGCD='k') and ((ZhouGCDJ>=4) and (ZhouGCDJ<=7)) then   //轴公差带为k的情况
        SearchRule4('k47')
      else if (ZhouGCD='k') and ((ZhouGCDJ<=3) or (ZhouGCDJ>=8)) then
        SearchRule4('k38')
      else if (ZhouGCD>='m') and (ZhouGCD<='z') or (ZhouGCD='za') or (ZhouGCD='zb') or (ZhouGCD='zc') then    //轴公差带为m~z的情况
        SearchRule4(ZhouGCD);
    end;
end;

procedure Tfrmgcph.RadioButton8Click(Sender: TObject);
begin
  BitBtn4.Enabled:=true;
  Label3.Visible:=true;
  Label4.Visible:=true;
  Label5.Visible:=true;
  Label12.Visible:=true;
  Label19.Visible:=true;
  ComboBox1.Visible:=true;
  ComboBox2.Visible:=true;

  Label6.Visible:=false;
  Label7.Visible:=false;
  Label8.Visible:=false;
  Label13.Visible:=false;
  ComboBox3.Visible:=false;
  Label20.Visible:=false;
  Label22.Visible:=false;
  ComboBox4.Visible:=false;
  ComboBox1.Text:='';
  ComboBox2.Text:='6';
  Edit1.SetFocus;

  Label3.Caption:='';
  Label4.Caption:='';
  Label5.Caption:='';
  Label6.Caption:='';
  Label7.Caption:='';
  Label8.Caption:='';
  Label20.Caption:='';
end;

procedure Tfrmgcph.RadioButton9Click(Sender: TObject);
begin
  BitBtn4.Enabled:=true;
  Label6.Visible:=true;
  Label7.Visible:=true;
  Label8.Visible:=true;
  Label20.Visible:=true;
  Label13.Visible:=true;
  ComboBox3.Visible:=true;
  ComboBox4.Visible:=true;

  Label3.Visible:=false;
  Label4.Visible:=false;
  Label5.Visible:=false;
  Label12.Visible:=false;
  Label19.Visible:=false;
  Label22.Visible:=false;
  ComboBox1.Visible:=false;
  ComboBox2.Visible:=false;
  ComboBox3.Text:='';
  ComboBox4.Text:='6';
  Edit1.SetFocus;

  Label3.Caption:='';
  Label4.Caption:='';
  Label5.Caption:='';
  Label6.Caption:='';
  Label7.Caption:='';
  Label8.Caption:='';
  Label19.Caption:='';
end;

procedure Tfrmgcph.RadioButton6Click(Sender: TObject);
begin
  Label2.Visible:=False;
  //Label16.Visible:=False;
  Label21.Visible:=False;
  Label22.Visible:=False;
  RadioButton1.Enabled:=False;
  RadioButton2.Enabled:=False;
  RadioButton3.Visible:=False;
  RadioButton4.Visible:=False;
  RadioButton5.Visible:=False;
  GroupBox2.Enabled:=false;
  GroupBox3.Visible:=false;
  BitBtn1.Visible:=false;
  Memo1.Visible:=false;
  Memo2.Visible:=false;
  Memo3.Visible:=false;

  //Label9.Visible:=true;
  RadioButton8.Visible:=true;
  RadioButton9.Visible:=true;
  GroupBox5.Visible:=true;
  RadioButton8.Checked :=true;
  BitBtn4.Visible:=true;

  Label3.Caption:='';
  Label4.Caption:='';
  Label5.Caption:='';
  Label6.Caption:='';
  Label7.Caption:='';
  Label8.Caption:='';
end;

procedure Tfrmgcph.RadioButton7Click(Sender: TObject);
begin
  RadioButton1.Enabled:=true;
  RadioButton2.Enabled:=true;
  RadioButton3.Visible:=true;
  RadioButton4.Visible:=true;
  RadioButton5.Visible:=true;
  GroupBox2.Enabled:=true;
  GroupBox3.Visible:=true;
  Label2.Visible:=true;
  Label3.Visible:=true;
  Label4.Visible:=true;
  Label5.Visible:=true;
  Label6.Visible:=true;
  Label7.Visible:=true;
  Label8.Visible:=true;
  Label12.Visible:=true;
  Label13.Visible:=true;
  ComboBox1.Visible:=true;
  ComboBox2.Visible:=true;
  ComboBox3.Visible:=true;
  ComboBox4.Visible:=true;
  //Label16.Visible:=true;

  BitBtn4.Visible:=false;
  //Label9.Visible:=false;
  Label19.Visible:=false;
  Label20.Visible:=false;
  Label21.Visible:=False;
  Label22.Visible:=False;
  RadioButton8.Visible:=false;
  RadioButton9.Visible:=false;
  GroupBox5.Visible:=false;
  RadioButton8.Checked:=false;
  RadioButton9.Checked:=false;
  BitBtn1.Visible:=true;

  Label3.Caption:='';
  Label4.Caption:='';
  Label5.Caption:='';
  Label6.Caption:='';
  Label7.Caption:='';
  Label8.Caption:='';

  RadioButton1.Checked:=true;
  RadioButton3.Checked:=true;

  if (RadioButton1.Checked=true) and (RadioButton3.Checked=true) then
  begin
    ComboBox1.Text:='H';
    ComboBox3.Text:='';
  end;
end;

procedure Tfrmgcph.C1Click(Sender: TObject);
var
prj_path: string;
begin
  prj_path:=ExtractFilePath(ParamStr(0));
  ShellExecute(0,'Open',pchar(prj_path+'hlp\公差与配合帮助文件.chm') , Nil,nil, SW_SHOWNORMAL);
end;

end.

