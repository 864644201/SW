{==============================================================================

  ThemeEngine's Button
  Copyright (C) 2000-2002 by Evgeny Kryukov
  All rights reserved

  All conTeThements of this file and all other files included in this archive
  are Copyright (C) 2002 Evgeny Kryukov. Use and/or distribution of
  them requires acceptance of the License Agreement.

  See License.txt for licence information

  $Id: ksthemegroupboxs.pas,v 1.2 2002/10/28 21:04:00 Evgeny Exp $

===============================================================================}

unit ksthemegroupboxs;

{$I te_define.inc}
{$T-,W-,X+,P+}

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms,
  te_controls, KsThemeThemes, KsThemeEngine, KsThemeVersion;

type

{ TTeThemeGroupBox class }

  TTeThemeGroupBox = class(TTeCustomGroupBox)
  private
    FThemeEngine: TTeThemeEngine;
    function GetVersion: TTeThemeVersion;
    procedure SetVersion(const Value: TTeThemeVersion);
    procedure SetThemeEngine(const Value: TTeThemeEngine);
  protected
    function UseTheme: boolean;
    { inherited }
    function CreateCheckBox(AOwner: TComponent): TTeCustomCheckBox; override;
    procedure PaintBuffer; override;
    { VCL protected }
    procedure Notification(AComponent: TComponent; Operation: TOperation); override;
  public
    constructor Create(AOwner: TComponent); override;
    destructor Destroy; override;
    procedure Loaded; override;
  published
    property Anchors;
    property Align;
    property Blending;
    property Caption;
    property CaptionMargin;
    property Font;
    { Specifies the appearance and behavior by using specified theme's from TTeThemeEngine .
      See Also:
        TTeThemeEngine
    }
    property ThemeEngine: TTeThemeEngine read FThemeEngine write SetThemeEngine;
    property Transparent;
    property Version: TTeThemeVersion read GetVersion write SetVersion
      stored False;
  end;

{ TTeThemeRadioGroup }

  TTeThemeRadioGroup = class(TTeThemeGroupBox)
  private
    FButtons: TList;
    FItems: TStrings;
    FItemIndex: integer;
    FColumns: integer;
    FReading: Boolean;
    FUpdating: Boolean;
    FItemTabStop: Boolean;
    procedure ArrangeButtons;
    procedure ButtonClick(Sender: TObject);
    procedure ItemsChange(Sender: TObject);
    procedure SetButtonCount(Value: integer);
    procedure SetColumns(Value: integer);
    procedure SetItemIndex(Value: integer);
    procedure SetItems(Value: TStrings);
    procedure SetItemTabStop(Value: Boolean);
    procedure UpdateButtons;
    procedure CMEnabledChanged(var Message: TMessage); message CM_ENABLEDCHANGED;
    procedure CMFontChanged(var Message: TMessage); message CM_FONTCHANGED;
    procedure WMSize(var Message: TWMSize); message WM_SIZE;
  protected
    { VCL }
    procedure Loaded; override;
    procedure ReadState(Reader: TReader); override;
    function CanModify: Boolean; virtual;
    procedure GetChildren(Proc: TGetChildProc; Root: TComponent); override;
  public
    constructor Create(AOwner: TComponent); override;
    destructor Destroy; override;
    procedure FlipChildren(AllLevels: Boolean); override;
    Procedure SetItemFocus;
  published
    property Columns: integer read FColumns write SetColumns default 1;
    property ItemIndex: integer read FItemIndex write SetItemIndex default -1;
    property Items: TStrings read FItems write SetItems;
    { You automatically set the tabstop property for the item in your code to true when
      the item is checked This would be an improvement on the standard radiogroup }
    Property ItemTabStop: Boolean Read FItemTabStop Write SetItemTabStop default True;
  end;

implementation {===============================================================}

uses KsThemeCheckBoxs;

{ TTeThemeGroupBox }

constructor TTeThemeGroupBox.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
end;

destructor TTeThemeGroupBox.Destroy;
begin
  inherited Destroy;
end;

procedure TTeThemeGroupBox.Loaded;
begin
  inherited;
end;

function TTeThemeGroupBox.UseTheme: boolean;
begin
  if (csDestroying in ComponentState) or (csLoading in ComponentState) then
    Result := false
  else
    Result := (FThemeEngine <> nil) and (FThemeEngine.Theme <> nil) and
      (FThemeEngine.Theme.UseTheme);
end;

{ Drawing }

procedure TTeThemeGroupBox.PaintBuffer;
var
  R, CaptionRect: TRect;
begin
  if not UseTheme then
    inherited
  else
  begin
    if not Transparent then
      FillRect(Canvas, Rect(0, 0, FWidth, FHeight), FThemeEngine.Theme.GetColor(ngcBtnFace));

    Canvas.Font.Assign(Self.Font);

    R := GetBoxRect;

    if UseCheckBox then
      CaptionRect := Rect(CaptionMargin, 0, CaptionMargin + CheckBox.Width, CheckBox.Height)
    else
      CaptionRect := Rect(CaptionMargin, 0, CaptionMargin + TextWidth(Canvas, Caption), TextHeight(Canvas, Caption));

    if UseCheckBox then
      FThemeEngine.Theme.DrawGroupBox(Canvas, R, CaptionRect, '')
    else
    begin
      if Caption <> '' then
        InflateRect(CaptionRect, 3, 0);
      FThemeEngine.Theme.DrawGroupBox(Canvas, R, CaptionRect, Caption);
    end;
  end;
end;

{ Properties }

function TTeThemeGroupBox.GetVersion: TTeThemeVersion;
begin
  Result := sTeThemeVersion;
end;

procedure TTeThemeGroupBox.SetVersion(const Value: TTeThemeVersion);
begin
end;

procedure TTeThemeGroupBox.SetThemeEngine(const Value: TTeThemeEngine);
begin
  FThemeEngine := Value;

  if Self is TTeThemeRadioGroup then
    (Self as TTeThemeRadioGroup).UpdateButtons;

  if CheckBox <> nil then
    with (CheckBox as TTeThemeCheckBox) do
    begin
      ThemeEngine := Self.ThemeEngine;
      AdjustCheckBounds;
    end;

  Invalidate;
end;

{ TTeThemeGroupButton ================================================================}

type
  TTeThemeGroupButton = class(TTeThemeRadioButton)
  private
    FInClick: Boolean;
    procedure CNCommand(var Message: TWMCommand); message CN_COMMAND;
  protected
    procedure KeyDown(var Key: Word; Shift: TShiftState); override;
    procedure KeyPress(var Key: Char); override;
  public
    constructor InternalCreate(RadioGroup: TTeThemeRadioGroup; AItemTabStop:Boolean=True);
    destructor Destroy; override;
  end;

constructor TTeThemeGroupButton.InternalCreate(RadioGroup: TTeThemeRadioGroup; AItemTabStop: boolean = true);
begin
  inherited Create(RadioGroup);
  TabStop := AItemTabStop;
  RadioGroup.FButtons.Add(Self);
  Visible := False;
  Enabled := RadioGroup.Enabled;
  ParentShowHint := False;
  OnClick := RadioGroup.ButtonClick;
  Parent := RadioGroup;
end;

destructor TTeThemeGroupButton.Destroy;
begin
  TTeThemeRadioGroup(Owner).FButtons.Remove(Self);
  inherited Destroy;
end;

procedure TTeThemeGroupButton.CNCommand(var Message: TWMCommand);
begin
  if not FInClick then
  begin
    FInClick := True;
    try
      if ((Message.NotifyCode = BN_CLICKED) or
        (Message.NotifyCode = BN_DOUBLECLICKED)) and
        TTeThemeRadioGroup(Parent).CanModify then
        inherited;
    except
      Application.HandleException(Self);
    end;
    FInClick := False;
  end;
end;

procedure TTeThemeGroupButton.KeyPress(var Key: Char);
begin
  inherited KeyPress(Key);
  TTeThemeRadioGroup(Parent).KeyPress(Key);
  if (Key = #8) or (Key = ' ') then
  begin
    if not TTeThemeRadioGroup(Parent).CanModify then Key := #0;
  end;
end;

procedure TTeThemeGroupButton.KeyDown(var Key: Word; Shift: TShiftState);
begin
  inherited KeyDown(Key, Shift);
  TTeThemeRadioGroup(Parent).KeyDown(Key, Shift);
end;

procedure TTeThemeGroupBox.Notification(AComponent: TComponent;
  Operation: TOperation);
begin
  inherited;
  if (Operation = opRemove) and (AComponent = FThemeEngine) then
    ThemeEngine := nil;
end;

function TTeThemeGroupBox.CreateCheckBox(AOwner: TComponent): TTeCustomCheckBox;
begin
  Result := TTeThemeCheckBox.Create(AOwner);
  TTeThemeCheckBox(Result).ThemeEngine:=ThemeEngine;
end;

{ TTeThemeRadioGroup }

constructor TTeThemeRadioGroup.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
  ControlStyle := ControlStyle + [csSetCaption, csDoubleClicks];
  FItemTabStop := false;
  FButtons := TList.Create;
  FItems := TStringList.Create;
  TStringList(FItems).OnChange := ItemsChange;
  FItemIndex := -1;
  FColumns := 1;
end;

destructor TTeThemeRadioGroup.Destroy;
begin
  SetButtonCount(0);
  TStringList(FItems).OnChange := nil;
  FItems.Free;
  FButtons.Free;
  inherited Destroy;
end;

procedure TTeThemeRadioGroup.FlipChildren(AllLevels: Boolean); 
begin
  { The radio buttons are flipped using BiDiMode }
end;

procedure TTeThemeRadioGroup.ArrangeButtons;
var
  ButtonsPerCol, ButtonWidth, ButtonHeight, TopMargin, I: integer;
  DC: HDC;
  SaveFont: HFont;
  Metrics: TTextMetric;
  DeferHandle: THandle;
  ALeft: integer;
begin
  if (FButtons.Count <> 0) and not FReading then
  begin
    DC := GetDC(0);
    SaveFont := SelectObject(DC, Font.Handle);
    GetTextMetrics(DC, Metrics);
    SelectObject(DC, SaveFont);
    ReleaseDC(0, DC);
    ButtonsPerCol := (FButtons.Count + FColumns - 1) div FColumns;
    ButtonWidth := (Width - 10) div FColumns;
    I := Height - Metrics.tmHeight - 5;
    ButtonHeight := I div ButtonsPerCol;
    TopMargin := Metrics.tmHeight + 1 + (I mod ButtonsPerCol) div 2;
    DeferHandle := BeginDeferWindowPos(FButtons.Count);
    try
      for I := 0 to FButtons.Count - 1 do
        with TTeThemeGroupButton(FButtons[I]) do
        begin
          BiDiMode := Self.BiDiMode;
          ALeft := (I div ButtonsPerCol) * ButtonWidth + 8;
          if UseRightToLeftAlignment then
            ALeft := Self.ClientWidth - ALeft - ButtonWidth;
          DeferHandle := DeferWindowPos(DeferHandle, Handle, 0,
            ALeft,
            (I mod ButtonsPerCol) * ButtonHeight + TopMargin,
            ButtonWidth, ButtonHeight,
            SWP_NOZORDER or SWP_NOACTIVATE);
          Visible := True;
        end;
    finally
      EndDeferWindowPos(DeferHandle);
    end;
  end;
end;

procedure TTeThemeRadioGroup.ButtonClick(Sender: TObject);
var
  Tmp: TWinControl;
begin
  if not FUpdating then
  begin
    FItemIndex := FButtons.IndexOf(Sender);

    Tmp := TTeThemeGroupButton(FButtons[FItemIndex]).Parent;
    while (Tmp.Parent <> nil) and (Tmp.Visible) do
      Tmp := Tmp.Parent;

    if Tmp.Visible and Tmp.Enabled then
      TTeThemeGroupButton(FButtons[FItemIndex]).SetFocus;

    Changed;
    Click;
  end;
end;

procedure TTeThemeRadioGroup.ItemsChange(Sender: TObject);
begin
  if not FReading then
  begin
    if FItemIndex >= FItems.Count then FItemIndex := FItems.Count - 1;
    UpdateButtons;
  end;
end;

procedure TTeThemeRadioGroup.Loaded;
begin
  inherited Loaded;
  ArrangeButtons;
  UpdateButtons;
end;

procedure TTeThemeRadioGroup.ReadState(Reader: TReader);
begin
  FReading := True;
  inherited ReadState(Reader);
  FReading := False;
  UpdateButtons;
end;

procedure TTeThemeRadioGroup.SetButtonCount(Value: integer);
begin
  while FButtons.Count < Value do TTeThemeGroupButton.InternalCreate(Self, ItemTabStop);
  while FButtons.Count > Value do TTeThemeGroupButton(FButtons.Last).Free;
end;

procedure TTeThemeRadioGroup.SetColumns(Value: integer);
begin
  if Value < 1 then Value := 1;
  if Value > 16 then Value := 16;
  if FColumns <> Value then
  begin
    FColumns := Value;
    ArrangeButtons;
    Invalidate;
  end;
end;

procedure TTeThemeRadioGroup.SetItemIndex(Value: integer);
begin
  if FReading then FItemIndex := Value else
  begin
    if Value < -1 then Value := -1;
    if Value >= FButtons.Count then Value := FButtons.Count - 1;
    if FItemIndex <> Value then
    begin
      if FItemIndex >= 0 then
        TTeThemeGroupButton(FButtons[FItemIndex]).Checked := False;
      FItemIndex := Value;
      if FItemIndex >= 0 then
        TTeThemeGroupButton(FButtons[FItemIndex]).Checked := True;
    end;
  end;
end;

procedure TTeThemeRadioGroup.SetItems(Value: TStrings);
begin
  FItems.Assign(Value);
end;

procedure TTeThemeRadioGroup.SetItemTabStop(Value: Boolean);
var
  X: Integer;
begin
  if Value <> FItemTabStop then
  begin
     FItemTabStop := Value;
     for X := 0 to FButtons.Count - 1 do
       with TTeThemeGroupButton(FButtons.Items[X]) Do
       begin
         ItemTabStop := FItemTabStop;
         if Checked then TabStop := FItemTabStop;
       end;
    end;
end;

procedure TTeThemeRadioGroup.UpdateButtons;
var
  I: integer;
begin
  SetButtonCount(FItems.Count);
  for I := 0 to FButtons.Count - 1 do
  begin
    TTeThemeGroupButton(FButtons[I]).Caption := FItems[I];
    TTeThemeGroupButton(FButtons[I]).ThemeEngine := FThemeEngine;
  end;

  if FItemIndex >= 0 then
  begin
    FUpdating := True;
    TTeThemeGroupButton(FButtons[FItemIndex]).Checked := True;
    FUpdating := False;
  end;
  ArrangeButtons;
  Invalidate;
end;

procedure TTeThemeRadioGroup.CMEnabledChanged(var Message: TMessage);
var
  I: integer;
begin
  inherited;
  for I := 0 to FButtons.Count - 1 do
    TTeThemeGroupButton(FButtons[I]).Enabled := Enabled;
end;

procedure TTeThemeRadioGroup.CMFontChanged(var Message: TMessage);
begin
  inherited;
  ArrangeButtons;
end;

procedure TTeThemeRadioGroup.WMSize(var Message: TWMSize);
begin
  inherited;
  ArrangeButtons;
end;

function TTeThemeRadioGroup.CanModify: Boolean;
begin
  Result := True;
end;

procedure TTeThemeRadioGroup.GetChildren(Proc: TGetChildProc; Root: TComponent);
begin
end;

procedure TTeThemeRadioGroup.SetItemFocus;
Begin
  If FItemIndex<>-1 Then TTeThemeGroupButton(FButtons[FItemIndex]).SetFocus;
End;


end.
