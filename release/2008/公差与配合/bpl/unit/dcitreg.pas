{*******************************************************}
{                                                       }
{  Copyright (c) 1997-2001 Altium Limited               }
{                                                       }
{  http://www.dream-com.com                             }
{  contact@dream-com.com                                }
{                                                       }
{*******************************************************}
Unit dcITReg;
Interface
{.$I dc.inc}
{$I dcprod.inc}

{$IFDEF DREAMINFOTREEGRID}
{$R dcInfoGrid.dcr}
{$R dcTreeGrid.dcr}
{$ENDIF}
Procedure Register;

Implementation
Uses
  Windows, Classes, SysUtils, {$IFDEF D5}Contnrs,{$ENDIF}

  {$IFDEF USEOWNDSGNSTUFF}
  dcdsgnstuff,
  {$ELSE}
    {$IFDEF D6}
    DesignIntf,DesignEditors,VCLEditors,
    {$ELSE}
    dsgnintf,
    {$ENDIF}
  {$ENDIF}

     DB, TypInfo,
     dcntree, 
     dcTrEd,
     dcinfotree,
     dcInfoDBRel,
     dcInfoRel,
     dcJoinInfoSet,
     {$IFDEF DREAMINFOTREEGRID}
     dcitgrid,
     dctreegrid,
     dcTrGridEd,
     DCTreeGridEditors,
     DCInfoGridEditors,
     DCTreeGridEditorsReg,
     DCInfoGridEditorsReg,
     {$ENDIF}

     dcInfoLib,
     dcInfoDB,
     dcISet,
     dcdreamlib,
     dcconsts,
     dcDTree,
     dchook,
     dcVCLLib;

type
  {$IFDEF D6}
  TDCDsgnProp=IProperty;
  {$ELSE}
  TDCDsgnProp=TPropertyEditor;
  {$ENDIF}

  _TRelation=Class(TCustomDCRelation);
//  TSRInfoSet=Class(TCustomDCSRInfoSet);

  TCompIntfProperty=Class(TLimitComponentProperty)
  Protected
    Function GetMustSupportIntf:TGuid;virtual;
  Public
    Function AcceptComponent(Component:TComponent):Boolean;override;
  End;

  TInfoSetProperty=Class(TCompIntfProperty)
  Protected
    Function GetMustSupportIntf:TGuid;override;
  End;

  TInfoFieldProperty = class (TStringProperty)
  protected
    function GetInfoSet:TPersistent;
    function GetInfoSetName:string;virtual;
    Function GetInterface:IUnknown;virtual;
  public
    function GetAttributes:TPropertyAttributes; override;
    procedure GetValues(Proc:TGetStrProc); override;
  end;

  {$IFDEF DREAMINFOTREEGRID}
  TInfoMultiFieldProperty = class( TDCMultiFieldsProperty )
  protected
    function GetInfoSet:TPersistent;
    function GetInfoSetName:string;virtual;
    Function GetInterface : IUnknown; virtual;

    Procedure GetFieldsList( AList : TStrings ); override;
  end;
  {$ENDIF}

  TDCInfoMultiFieldsProperty=Class(TDCMultiFieldsProperty)
    Procedure GetFieldsList(AList:TStrings );override;
    Function GetInterface:IUnknown;virtual;
  End;

  TDetailFieldProperty=Class(TDCInfoMultiFieldsProperty)
  Protected
    Function GetInterface:IUnknown;override;
  End;

  TMasterFieldProperty=Class(TDCInfoMultiFieldsProperty)
  Protected
    Function GetInterface:IUnknown;override;
  End;

  TDCDetailDisplayFieldProperty=Class(TDCInfoMultiFieldsProperty)
  protected
    Function GetInterface:IUnknown;override;
  end;

  TMasterControlProperty = class(TLimitComponentProperty)
    function AcceptComponent(Component : TComponent) : boolean; override;
  end;

  TInfoSetDataSetProperty=Class(TInfoSetProperty)
  Public
    Function AcceptComponent(Component:TComponent):Boolean;override;
  End;

  TDCInfoTreeVersionEditor = class(TDCVersionEditor)
    Function GetProductName:String;override;
    Function GetValue:String;override;
  end;

  TDCTreeEditor=Class(TComponentEditor)
  Public
    Procedure Edit;override;
    Procedure ExecuteVerb(Index:Integer);override;
    Function GetVerb(Index:Integer):String;override;
    Function GetVerbCount:Integer;override;
  End;

  TDCTreeItemsProperty=Class(TClassProperty)
  Public
    Function GetAttributes:TPropertyAttributes;override;
    Procedure Edit;override;
  End;

  TDCDBRelInfoSetEditor = class(TComponentEditor)
  public
    procedure Edit; override;
    procedure ExecuteVerb(Index: Integer); override;
    function GetVerb(Index: Integer): string; override;
    function GetVerbCount: Integer; override;
  end;
  
  TMultiInfoSetsDisplayFieldProperty = Class(TDCInfoMultiFieldsProperty)
  Protected
    Function GetInterface:IUnknown;override;
  End;

  {$IFDEF DREAMINFOTREEGRID}
  TDCTreeGridItemsProperty = class(TClassProperty)
  public
    function GetAttributes: TPropertyAttributes; override;
    procedure Edit; override;
  end;

  TDCGridEditor = class(TComponentEditor)
  private
    {$IFDEF D6}
    procedure FindProperty(const Prop: TDCDsgnProp);
    {$ELSE}
    procedure FindProperty(PropertyEditor: TDCDsgnProp);
    {$ENDIF}
  public
    procedure ExecuteVerb(Index: Integer); override;
  end;

  TDCTreeGridEditor = class(TDCGridEditor)
  public
    procedure Edit; override;
    procedure ExecuteVerb(Index: Integer); override;
    function GetVerb(Index: Integer): string; override;
    function GetVerbCount: Integer; override;
  end;

  TDCInfoGridEditor = class(TDCGridEditor)
  public
    procedure Edit; override;
    function GetVerb(Index: Integer): string; override;
    function GetVerbCount: Integer; override;
  end;

  TEditorClassProperty = class(TStringProperty)
  public
    function GetAttributes: TPropertyAttributes; override;
    procedure GetValues(Proc: TGetStrProc); override;
    procedure SetValue(const Value: string); override;
  end;

  TInfoEditorClassProperty = class(TEditorClassProperty)
  public
    procedure GetValues(Proc: TGetStrProc); override;
  end;

  TDCLookupFieldsProperty = class(TDCMultiFieldsProperty)
  protected
    procedure GetFieldsList(AList: TStrings); override;
  end;

  TIGColumnDataFieldProperty = Class(TDCInfoMultiFieldsProperty)
  Protected
    Function GetInterface:IUnknown;override;
  End;

{---------------------------}

procedure TDCLookupFieldsProperty.GetFieldsList(AList: TStrings);
begin
  with TDCLookupEditorProps(GetComponent(0)) do
    if LookupDataset <> nil then
      LookupDataset.GetFieldNames(AList);
end;

{---------------------------}

function TEditorClassProperty.GetAttributes: TPropertyAttributes;
begin
  Result := [paMultiSelect, paValueList, paSortList, paRevertable];
end;

{---------------------------}

procedure TEditorClassProperty.GetValues(Proc: TGetStrProc);
begin
  EnumTreeGridEditors(Proc);
end;

{---------------------------}

procedure TEditorClassProperty.SetValue(const Value: string);
var
  obj : TPersistent;
begin
  obj := GetComponent(0);
  inherited;
  with Designer do
  begin
    SelectComponent(nil);
    SelectComponent(obj);
  end;
end;

{---------------------------}

procedure TInfoEditorClassProperty.GetValues(Proc: TGetStrProc);
begin
  EnumInfoGridEditors(Proc);
end;

{---------------------------}

function TDCTreeGridItemsProperty.GetAttributes: TPropertyAttributes;
begin
  Result := [paDialog];
end;

{---------------------------}

procedure TDCTreeGridItemsProperty.Edit;
var
  TreeView: TDCTreeGrid;
begin
  TreeView := GetComponent(0) as TDCTreeGrid;
  with TfrmTreeGridNodesEditor.Create2(nil, TDCCustomTreeViewClass(TreeView.ClassType)) do
  try
    if Execute(TreeView) or ApplyWasClicked then
      Modified;
  finally
    Free;
  end;
end;

{---------------------------}

{$IFDEF D6}
procedure TDCGridEditor.FindProperty(const Prop: TDCDsgnProp);
begin
  if Prop.GetName = 'Columns' then //don't resource
    Prop.Edit;
end;
{$ELSE}
procedure TDCGridEditor.FindProperty(PropertyEditor: TDCDsgnProp);
begin
  if PropertyEditor.GetName = 'Columns' then //don't resource
    PropertyEditor.Edit;

  PropertyEditor.Free;
end;
{$ENDIF}

{---------------------------}

procedure TDCGridEditor.ExecuteVerb(Index: Integer);
var
  ComponentList:
    {$IFDEF D6}
    IDesignerSelections
    {$ELSE}
    {$IFDEF D5}
    TDesignerSelectionList
    {$ELSE}
    TComponentList
    {$ENDIF}
    {$ENDIF}
  ;
begin
  if Index = 0 then
  begin
    ComponentList :=
      {$IFDEF D6}
      CreateSelectionList;
      {$ELSE}
      {$IFDEF D5}
      TDesignerSelectionList.Create;
      {$ELSE}
      TComponentList.Create;
      {$ENDIF}
      {$ENDIF}
    try
      ComponentList.Add(Component);
      GetComponentProperties(ComponentList, [tkClass], Designer, FindProperty);
    finally
      {$IFNDEF D6}
      ComponentList.Free;
      {$ENDIF}
    end;
  end;
end;

{---------------------------}

procedure TDCTreeGridEditor.Edit;
var
  TreeView: TDCTreeGrid;
  frmEditor: TfrmTreeGridNodesEditor;
begin
  TreeView := Component as TDCTreeGrid;
  frmEditor := TfrmTreeGridNodesEditor.Create2(nil, TDCTreeGrid);
  try
    if frmEditor.Execute(TreeView) or frmEditor.ApplyWasClicked then
      Designer.Modified;
  finally
    frmEditor.Free;
  end;
end;

{---------------------------}

procedure TDCTreeGridEditor.ExecuteVerb(Index: Integer);
begin
  if Index = 1 then
    Edit
  else
    inherited;
end;

{---------------------------}

function TDCTreeGridEditor.GetVerb(Index: Integer): string;
begin
  case Index of
    0: Result := 'Columns Editor...';
    1: Result := 'Items Editor...';
    else
      Result := '';
  end;
end;

{---------------------------}

function TDCTreeGridEditor.GetVerbCount: Integer;
begin
  Result := 2;
end;

{---------------------------}

procedure TDCInfoGridEditor.Edit;
begin
  ExecuteVerb(0);
end;

{---------------------------}

function TDCInfoGridEditor.GetVerb(Index: Integer): string;
begin
  if Index = 0 then
    Result := 'Columns Editor...'
  else
    Result := '';
end;

{---------------------------}

function TDCInfoGridEditor.GetVerbCount: Integer;
begin
  Result := 1;
end;

{$ENDIF}
{------------------------------------------------------------------}

function TInfoFieldProperty.GetAttributes: TPropertyAttributes;
begin
  result := inherited GetAttributes + [paValueList];
end;

{------------------------------------------------------------------}

function TInfoFieldProperty.GetInfoSetName : string;
begin
  result := 'InfoSet'; // don't resource
end;

{------------------------------------------------------------------}

function TInfoFieldProperty.GetInfoSet : TPersistent;
var
  Instance : TPersistent;
  PropInfo : PPropInfo;
begin
  Instance := GetComponent(0);
  PropInfo := TypInfo.GetPropInfo(Instance.ClassInfo, GetInfoSetName);
  if (PropInfo <> nil) and (PropInfo^.PropType^.Kind = tkClass) then
    result := TPersistent(GetOrdProp(Instance, PropInfo))
  else
    result := nil;
end;

{------------------------------------------------------------------}

Function TInfoFieldProperty.GetInterface:IUnknown;
Var
  InfoSet:TPersistent;
Begin
  Result:=Nil;
  infoset := GetInfoSet;
  If InfoSet=Nil Then
    Exit;
  infoset.GetInterface(IUnknown,Result);
End;

{------------------------------------------------------------------}

procedure TInfoFieldProperty.GetValues(Proc: TGetStrProc);
var
  i : integer;
  iProps : IDCColumns;
  iunk    : IUnknown;
  iRowSet : IDCRowSet;
begin
  iUnk:=GetInterface;
  if iUnk<>Nil  then
    if iUnk.QueryInterface(IDCRowSet, iRowSet) = S_OK then
      begin
        iProps := iRowSet.GetFields;
        If iProps=Nil Then
          Exit;
        with iProps do
          for i := 0 to GetCount - 1 do
             with (Items[i]) as IDCColumn do
                Proc(GetName);
      end;
end;

{ TInfoSetProperty }

Function TCompIntfProperty.AcceptComponent(Component:TComponent):Boolean;
Var
  Unknown:IUnknown;
  Obj:IUnknown;
  Comp:TPersistent;
Begin
  Comp:=GetComponent(0);
  If Comp Is TCollectionItem Then
    Result:=Component<>GetOwner(Comp)
  Else
    Result:=Component<>Comp;
  If Not Result Then Exit;
  Result:=Component.GetInterface(IUnknown,Unknown);
  If Not Result Then Exit;
  Result:=Unknown.QueryInterface(GetMustSupportIntf,Obj)=S_OK;
End;

{---------------------------------------------------------}

Function TCompIntfProperty.GetMustSupportIntf:TGuid;
Begin
  Result:=IUnknown;
End;

{ TInfoSetProperty }

Function TInfoSetProperty.GetMustSupportIntf:TGuid;
Begin
  Result:=IDCRowSet;
End;

function TMasterControlProperty.AcceptComponent(Component : TComponent) : boolean;
var
  iunk : IUnknown;
  iMasterInfoSet : IDCMasterInfoSet;
begin
  result := (Component <> GetComponent(0)) and
            Component.GetInterface(IUnknown, iunk) and (iunk.QueryInterface(IDCMasterInfoset, iMasterInfoSet) = S_OK);
end;

{ TInfoSetDataSetProperty }

Function TInfoSetDataSetProperty.AcceptComponent(Component:TComponent):Boolean;
Begin
  Result:=Inherited AcceptComponent(Component) Or (Component Is TDataSet);
End;

{---------------------------------------------------------}

Function TDetailFieldProperty.GetInterface:IUnknown;
Begin
  Result:=_TRelation(GetComponent(0)).DetailInfoSeti;
End;

{---------------------------------------------------------}

Function TMasterFieldProperty.GetInterface:IUnknown;
Begin
  Result:=_TRelation(GetComponent(0)).MasterInfoSeti;
End;

{---------------------------------------------------------}

{Function TFieldProperty.GetInterface:IUnknown;
Begin
  Result:=TSRInfoSet(GetComponent(0)).InfoSeti;
End;}

{---------------------------------------------------------}

function TDCInfoTreeVersionEditor.GetValue : string;
begin
  result := SDreamControlsVersion;
end;

{-----------------------------------------------------------}

function TDCInfoTreeVersionEditor.GetProductName:String;
begin
  Result:='InfoTree';
end;

{ TDCTreeEditor }

Procedure TDCTreeEditor.Edit;
Var
  TreeView:TDCTree;
  frmEditor:TfrmTreeNodesEditor;
Begin
  TreeView:=Component As TDCTree;
  frmEditor:=TfrmTreeNodesEditor.Create2(nil, TDCTree);
  Try
    If frmEditor.Execute(TreeView) or frmEditor.ApplyWasClicked Then
      Designer.Modified;
  Finally
    frmEditor.Free;
  End;
End;

Procedure TDCTreeEditor.ExecuteVerb(Index:Integer);
Begin
  Edit;
End;

Function TDCTreeEditor.GetVerb(Index:Integer):String;
Begin
  Result:='Items Editor...';
End;

Function TDCTreeEditor.GetVerbCount:Integer;
Begin
  Result:=1;
End;

{ TDCTreeItemsProperty }

Function TDCTreeItemsProperty.GetAttributes:TPropertyAttributes;
Begin
  Result:=[paDialog];
End;

Procedure TDCTreeItemsProperty.Edit;
Var
  TreeView:TDCTree;
Begin
  TreeView:=getComponent(0) As TDCTree;
  with TfrmTreeNodesEditor.Create2(nil, TDCCustomTreeViewClass(TreeView.ClassType)) do
  Try
    If Execute(TreeView) or ApplyWasClicked Then
      Modified;
  Finally
    Free;
  End;
End;

Procedure Register;
Begin
  RegisterComponents(SPalDreamInfoTree,[TDCDBRelInfoSet]);
  RegisterComponents(SPalDreamInfoTree,[TDCFilterInfoSet, TDCMultiInfoSet]);
  RegisterComponents(SPalDreamInfoTree,[TDCInfoTree, TDCInfoListView]);
  //RegisterComponents(SPalDreamInfoTree,[TDCDBJoinInfoSet]);
//  RegisterComponents(SPalDreamInfoTree,[TDCRelInfoSet]);
//  RegisterComponents(SPalDreamInfoTree,[TDCDBSRInfoSet]);
  RegisterComponents(SPalDreamInfoTree,[TDCTree]);
//  htreereg.Register;
  RegisterComponentEditor(TDCDBRelInfoSet, TDCDBRelInfoSetEditor);
  RegisterPropertyEditor(TypeInfo(TComponent), TDCDBRelInfoSet, 'RootDataSet', TInfoSetDataSetProperty); //don't resource
  RegisterPropertyEditor(TypeInfo(TComponent), TDataRelation, 'DetailDataSet', TInfoSetDataSetProperty); //don't resource
  RegisterPropertyEditor(TypeInfo(TComponent), TDataRelation, 'MasterDataSet', TInfoSetDataSetProperty); //don't resource
//  RegisterPropertyEditor(TypeInfo(TComponent), TDCDBSRInfoSet, 'DataSet', TInfoSetDataSetProperty); //don't resource
  RegisterPropertyEditor(TypeInfo(TComponent), TPersistent, 'MasterControl', TMasterControlProperty); //don't resource
  RegisterPropertyEditor(TypeInfo(TComponent), TDCRelInfoSet, 'RootInfoSet', TInfoSetProperty); //don't resource
  RegisterPropertyEditor(TypeInfo(TComponent), TInfoSetRelation, 'DetailInfoSet', TInfoSetProperty); //don't resource
  RegisterPropertyEditor(TypeInfo(TComponent), TInfoSetRelation, 'MasterInfoSet', TInfoSetProperty); //don't resource
//  RegisterPropertyEditor(TypeInfo(String), TCustomDCRelation, 'DetailDisplayField', TDetailFieldProperty); //don't resource
  RegisterPropertyEditor(TypeInfo(String), TCustomDCRelation, 'DetailFields', TDetailFieldProperty); //don't resource
  RegisterPropertyEditor(TypeInfo(String), TCustomDCRelation, 'MasterFields', TMasterFieldProperty); //don't resource
  RegisterPropertyEditor(TypeInfo(String), TPersistent, 'DisplayField', TInfoFieldProperty); //don't resource
//  RegisterPropertyEditor(TypeInfo(TComponent), TCustomDCSRInfoSet, 'InfoSet', TInfoSetProperty); //don't resource
//  RegisterPropertyEditor(TypeInfo(String), TCustomDCSRInfoSet, 'DetailFields', TFieldProperty); //don't resource
//  RegisterPropertyEditor(TypeInfo(String), TCustomDCSRInfoSet, 'DisplayField', TFieldProperty); //don't resource
//  RegisterPropertyEditor(TypeInfo(String), TCustomDCSRInfoSet, 'MasterFields', TFieldProperty); //don't resource
//  RegisterPropertyEditor(TypeInfo(String), TCustomDCSRInfoSet, 'MasterFields', TFieldProperty); //don't resource
  RegisterPropertyEditor(TypeInfo(string), TComponent, 'CheckField', TInfoFieldProperty); //don't resource
  RegisterPropertyEditor(TypeInfo(string), TComponent, 'ImageField', TInfoFieldProperty); //don't resource
  RegisterPropertyEditor(TypeInfo(string), TComponent, 'StateImageField', TInfoFieldProperty); //don't resource
  RegisterPropertyEditor(TypeInfo(TComponent), TPersistent, 'InfoSet', TInfoSetProperty); //don't resource

  RegisterPropertyEditor(TypeInfo(string), TPersistent, 'InfoField', TInfoFieldProperty); //don't resource
    
  RegisterPropertyEditor(TypeInfo(TDCInfoTreeVersion), nil, '' , TDCInfoTreeVersionEditor);
  RegisterPropertyEditor(TypeInfo(TDCTreeNodes), TDCTree, 'Items' , TDCTreeItemsProperty);

  RegisterComponentEditor(TDCTree,TDCTreeEditor);
  RegisterNoIcon([TfrmTreeNodesEditor]);

  RegisterPropertyEditor(TypeInfo(String), TDCMultiInfoSetItem, 'DisplayField',
   TMultiInfoSetsDisplayFieldProperty);
  {$IFDEF DREAMINFOTREE}
  RegisterPropertyEditor(TypeInfo(String), TCustomDCRelation, 'DetailDisplayField',
    TDCDetailDisplayFieldProperty); //don't resource
  {$ENDIF}

  {$IFDEF DREAMINFOTREEGRID}
  //TreeGrid
  RegisterComponents(SPalDreamInfoTree,[TDCTreeGrid]);
  RegisterComponentEditor(TDCTreeGrid, TDCTreeGridEditor);
  RegisterPropertyEditor(TypeInfo(TDCSortedColumns),TDCTreeGrid,'SortedColumns',Nil);
  RegisterPropertyEditor(TypeInfo(TDCTreeNodes), TDCTreeGrid, 'Items',
    TDCTreeGridItemsProperty);
  RegisterPropertyEditor(TypeInfo(string), TDCTreeColumn, 'EditorClass',
    TEditorClassProperty);

  //InfoTreeGrid
  RegisterComponents(SPalDreamInfoTree,[TDCInfoGrid]);
  RegisterComponentEditor(TDCInfoGrid, TDCInfoGridEditor);
  RegisterPropertyEditor(TypeInfo(string), TDCInfoGridColumn, 'DataField',
    TIGColumnDataFieldProperty);
  RegisterPropertyEditor(TypeInfo(String), TDCInfoGrid, 'InfoField',
    TInfoMultiFieldProperty);
  RegisterPropertyEditor(TypeInfo(string), TDCInfoGridColumn, 'EditorClass',
    TInfoEditorClassProperty);
  RegisterPropertyEditor(TypeInfo(string), TDCLookupEditorProps, 'KeyFields',
    TDCLookupFieldsProperty);
  RegisterPropertyEditor(TypeInfo(string), TDCLookupEditorProps, 'DataField',
    TDCLookupFieldsProperty);
  RegisterPropertyEditor(TypeInfo(string), TDCLookupEditorProps, 'ListFields',
    TDCLookupFieldsProperty);
  {$ENDIF}
End;

{ TDCInfoMultiFieldsProperty }

Procedure TDCInfoMultiFieldsProperty.GetFieldsList( AList : TStrings );
Var
  I       : Integer;
  iProps  : IDCColumns;
  iunk    : IUnknown;
  iRowSet : IDCRowSet;
begin
  iUnk := GetInterface;
  If iUnk <> Nil Then
    If iUnk.QueryInterface( IDCRowSet, iRowSet ) = S_OK Then
    begin
      iProps := iRowSet.GetFields;
      If iProps = Nil Then
        Exit;
      With iProps Do
        For I := 0 To GetCount - 1 Do
           With ( Items[I] ) As IDCColumn Do
              AList.Add( GetName );
    end;
end;

Function TDCInfoMultiFieldsProperty.GetInterface:IUnknown;
Begin
  Result:=Nil;
End;

{ TDCDetailDisplayFieldProperty }

Function TDCDetailDisplayFieldProperty.GetInterface : IUnknown;
Begin
  Result:=_TRelation(GetComponent(0)).DetailInfoSeti;
end;

{$IFDEF DREAMINFOTREEGRID}

{ TInfoMultiFieldProperty }

Procedure TInfoMultiFieldProperty.GetFieldsList( AList : TStrings );
var
  i : integer;
  iProps : IDCColumns;
  iunk    : IUnknown;
  iRowSet : IDCRowSet;
begin
  iUnk:=GetInterface;
  if iUnk<>Nil  then
    if iUnk.QueryInterface(IDCRowSet, iRowSet) = S_OK then
      begin
        iProps := iRowSet.GetFields;
        If iProps=Nil Then
          Exit;
        with iProps do
          for i := 0 to GetCount - 1 do
             with (Items[i]) as IDCColumn do
                AList.Add(GetName);
      end;
end;

function TInfoMultiFieldProperty.GetInfoSet: TPersistent;
var
  Instance : TPersistent;
  PropInfo : PPropInfo;
begin
  Instance := GetComponent(0);
  PropInfo := TypInfo.GetPropInfo(Instance.ClassInfo, GetInfoSetName);
  if (PropInfo <> nil) and (PropInfo^.PropType^.Kind = tkClass) then
    result := TPersistent(GetOrdProp(Instance, PropInfo))
  else
    result := nil;
end;

function TInfoMultiFieldProperty.GetInfoSetName: string;
begin
  result := 'InfoSet'; // don't resource
end;

Function TInfoMultiFieldProperty.GetInterface: IUnknown;
Var
  InfoSet:TPersistent;
Begin
  Result:=Nil;
  infoset := GetInfoSet;
  If InfoSet=Nil Then
    Exit;
  infoset.GetInterface(IUnknown,Result);
end;

{ TIGColumnDataFieldProperty }

type
  T_DCInfoGridColumn = class(TDCInfoGridColumn);

function TIGColumnDataFieldProperty.GetInterface: IUnknown;
Var
  InfoSet: TPersistent;
begin
  Result:=Nil;
  InfoSet := TCustomDCInfoGrid(T_DCInfoGridColumn(GetComponent(0)).TreeView).InfoSet;
  If InfoSet <> Nil Then
    infoset.GetInterface(IUnknown, Result);
end;
{$ENDIF}

{ TDCDBRelInfoSetEditor }

type
  {$IFDEF D6}
  IFormDesigner=IDesigner;
  {$ENDIF}

  {$IFDEF D4}
  TFormDesigner = IFormDesigner;
  {$ENDIF}
  {$IFDEF D5}
  {$IFDEF D6}
  TDesignerSelectionList=TDesignerSelections;
  {$ENDIF}
  TComponentList = class(TDesignerSelectionList)
  end;
  {$ENDIF}
  TObjectWrapper = class
  private
    FPropedit: TDCDsgnProp;
  public
    PropName: string;
    Instance: TPersistent;
    procedure Execute(Designer: TFormDesigner);
    {$IFDEF D6}
    procedure GetPropEdit(const Prop: TDCDsgnProp);
    {$ELSE}
    procedure GetPropEdit(Prop: TDCDsgnProp);
    {$ENDIF}
  end;

{----------------------------}

{$IFDEF D6}
procedure TObjectWrapper.GetPropEdit(const Prop: TDCDsgnProp);
{$ELSE}
procedure TObjectWrapper.GetPropEdit(Prop: TDCDsgnProp);
{$ENDIF}
begin
  if CompareText(Prop.GetName, PropName) = 0 then
    fPropEdit := Prop
  {$IFNDEF D6}
  else
    Prop.Free;
  {$ENDIF}
end;

{----------------------------}

procedure TObjectWrapper.Execute(Designer: TFormDesigner);
var
  clist: TComponentList;
begin
  clist := TComponentList.Create;
  try
    clist.Add(TComponent(Instance));
    GetComponentProperties(clist, tkAny, Designer, GetPropEdit);
    if fPropEdit <> nil then
      fPropEdit.Edit;
  finally
    clist.Free;
    FreeObject(fPropEdit,fPropEdit);
  end;
end;

{----------------------------}

procedure TDCDBRelInfoSetEditor.Edit;
begin
  with TObjectWrapper.Create do
  try
    Instance := Self.Component;
    Propname := 'Relations'; //don't resource
    Execute(Designer);
  finally
    Free;
  end;
end;

procedure TDCDBRelInfoSetEditor.ExecuteVerb(Index: Integer);
begin
  Edit;
end;

function TDCDBRelInfoSetEditor.GetVerb(Index: Integer): string;
begin
  Result := 'Relations Editor...';
end;

function TDCDBRelInfoSetEditor.GetVerbCount: Integer;
begin
  Result := 1;
end;

{ TMultiInfoSetsDisplayFieldProperty }

function TMultiInfoSetsDisplayFieldProperty.GetInterface: IUnknown;
var
  InfoSet: TPersistent;
begin
  Result := nil;
  InfoSet := TDCMultiInfoSetItem(GetComponent(0)).InfoSet;
  if InfoSet <> nil then
    InfoSet.GetInterface(IUnknown, Result);
end;

End.
