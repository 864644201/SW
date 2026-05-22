{==============================================================================

  ThemeEngine Form
  Copyright (C) 2000-2002 by Evgeny Kryukov
  All rights reserved

  All conTeThements of this file and all other files included in this archive
  are Copyright (C) 2002 Evgeny Kryukov. Use and/or distribution of
  them requires acceptance of the License Agreement.

  See License.txt for licence information

  $Id: ksthemeforms.pas,v 1.4 2003/01/20 13:20:09 evgeny Exp $

===============================================================================}

unit ksthemeforms;

{$I te_define.inc}
{$I ksthemeforms.inc}
{$T-,W-,X+,P+}

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms, Buttons,
  ExtCtrls, te_controls, ksthemethemes, ksthemeengine, ksthemeitems,
  ksthemeversion;

type

{ TTeThemeForm class }

{ TTeThemeForm is a inheritance of TForm with advanced features }
  TTeThemeForm = class(TTeCustomForm)
  private
    FThemeEngine: TTeThemeEngine;
    function GetVersion: TTeThemeVersion;
    procedure SetThemeEngine(const Value: TTeThemeEngine);

    function UseTheme: boolean;
    procedure SetVersion(const Value: TTeThemeVersion);
  protected
    { protected Routines }
    function GetRegion: HRgn; override;
    function GetClientBounds: TRect; override;
    function GetCaptionButtonRect(Button: TTeBorderIcon): TRect; override;
    { WindowState's Rect }
    function GetRollupRect: TRect; override;
    function GetMinimizedRect: TRect; override;
    function GetMaximizedRect: TRect; override;
    { Tracking Size }
    function GetMaxTrackSize: TPoint; override;
    function GetMinTrackSize: TPoint; override;
    { Menus }
    function CreateMenuItem(AOwner: TComponent): TTeCustomItem; override;
    { Painting }
    procedure PaintNonClientArea(Canvas: TCanvas); override;
    procedure PaintClientArea; override;
    { Mouse Routines }
    function GetHitTest(X, Y: integer): TTeHitTest; override;
    { VCL protected  }
    procedure Notification(AComponent: TComponent; Operation: TOperation); override;
    { protected Properties }
  public
    constructor Create(AOwner: TComponent); override;
    destructor Destroy; override;
    procedure Loaded; override;

    procedure UpdateForm;
    procedure UpdateControls;
  published
    property Active;
    property BorderIcons;
    property BorderStyle;
    property MenuBar;
    property ThemeEngine: TTeThemeEngine read FThemeEngine write SetThemeEngine;
    property Version: TTeThemeVersion read GetVersion write SetVersion;
    property WindowState;
  end;

implementation {===============================================================}

uses ksthememenus;

type

  THackForm = class(TCustomForm);
{$IFDEF KS_COMPILER5_UP}
  THackFrame = class(TCustomForm);
{$ENDIF}
  THackThemeMenuBar = class(TTeThemeMenuBar);

{ TTeThemeForm ===============================================================}

constructor TTeThemeForm.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
  Active := true;
end;

destructor TTeThemeForm.Destroy;
begin
  inherited Destroy;
end;

procedure TTeThemeForm.Loaded;
begin
  inherited Loaded;
  UpdateControls;
end;

procedure TTeThemeForm.UpdateForm;
begin
  if csLoading in ComponentState then Exit;
  if csDestroying in ComponentState then Exit;

  if Form <> nil then
  begin
    UpdateNonClientArea(0);
    Form.Invalidate;

    if (FThemeEngine <> nil) and (FThemeEngine.Theme <> nil) then
      FThemeEngine.Theme.BiDiMode := Form.BiDiMode;
  end
  else
    if Owner is TCustomForm then
    begin
      THackForm(Owner).Invalidate;
    end;
end;

procedure TTeThemeForm.UpdateControls;
var
  i: integer;
begin
  if csLoading in ComponentState then Exit;
  if csDestroying in ComponentState then Exit;

  if Form <> nil then
  begin
    for i := 0 to Form.ComponentCount - 1 do
    begin
      if Form.Components[i] is TTeThemeMenuBar then
      begin
        THackThemeMenuBar(Form.Components[i]).Loaded;
        THackThemeMenuBar(Form.Components[i]).Invalidate;
      end
      else
{$IFDEF KS_COMPILER5_UP}
        if (Form.Components[i] is TCustomFrame) and UseTheme then
        begin
          THackFrame(Form.Components[i]).Color := FThemeEngine.Theme.GetColor(ngcBtnFace);
          THackFrame(Form.Components[i]).Invalidate;
        end
        else
{$ENDIF}
          if Form.Components[i] is TControl then
            TControl(Form.Components[i]).Invalidate;
    end;
  end
  else
    if Owner is TCustomForm then
    begin
      for i := 0 to THackForm(Owner).ComponentCount - 1 do
        if THackForm(Owner).Components[i] is TControl then
          TControl(THackForm(Owner).Components[i]).Invalidate;
    end;
end;

function TTeThemeForm.UseTheme: boolean;
begin
  if (csDestroying in ComponentState) or (csLoading in ComponentState) then
    Result := false
  else
    Result := (FThemeEngine <> nil) and (FThemeEngine.Theme <> nil) and
      (FThemeEngine.Theme.UseTheme);
end;

function TTeThemeForm.GetRegion: HRgn;
begin
  if UseTheme then
  begin
    if (Form <> nil) and (THackForm(Form).FormStyle = fsMDIChild) and
       (WindowState = kwsMaximized) then
    begin
      Result := inherited GetRegion;
      Exit
    end;

    Result := FThemeEngine.Theme.GetRegion(Width, Height, BorderStyle)
  end
  else
    Result := inherited GetRegion
end;

{ protected Painting Routines =================================================}

procedure TTeThemeForm.PaintNonClientArea(Canvas: TCanvas);
 procedure IntDrawButton(R: TRect; Kind: TTeBorderIcon; Hot, Down: boolean);
 var
   IHandle, IHandle2 : HICON;
   IconX, IconY : integer;
   ID: boolean;
   R1: TRect;
 begin
   if RectWidth(R) = 0 then Exit;

   case Kind of
     kbiSystemMenu: begin
       ID := false;
       if THackForm(Form).Icon.Handle <> 0 then
         IHandle := THackForm(Form).Icon.Handle
       else
         if Application.Icon.Handle <> 0 then
           IHandle := Application.Icon.Handle
         else
         begin
           IHandle := LoadIcon(0, IDI_APPLICATION);
           ID := true;
         end;
       IconX := GetSystemMetrics(SM_CXSMICON);
       IconY := GetSystemMetrics(SM_CYSMICON);

       IHandle2 := CopyImage(IHandle, IMAGE_ICON, IconX, IconY, LR_COPYFROMRESOURCE);

       R1 := Rect(0, 0, IconX, IconY);
       RectCenter(R1, R);

       DrawIconEx(Canvas.Handle, R1.Left, R1.Top, IHandle2, 0, 0, 0, 0, DI_NORMAL);
       DestroyIcon(IHandle2);
       if ID then DestroyIcon(IHandle);
     end;
     kbiClose: FThemeEngine.Theme.DrawSysButton(Canvas, R, ngbClose, Hot, Down, FormActive, BorderStyle);
     kbiHelp: FThemeEngine.Theme.DrawSysButton(Canvas, R, ngbHelp, Hot, Down, FormActive, BorderStyle);
     kbiMaximize: begin
       if WindowState = kwsMaximized then
         FThemeEngine.Theme.DrawSysButton(Canvas, R, ngbRestore, Hot, Down, FormActive, BorderStyle)
       else
         FThemeEngine.Theme.DrawSysButton(Canvas, R, ngbMax, Hot, Down, FormActive, BorderStyle);
     end;
     kbiMinimize: begin
       if WindowState = kwsMinimized then
         FThemeEngine.Theme.DrawSysButton(Canvas, R, ngbRestore, Hot, Down, FormActive, BorderStyle)
       else
         FThemeEngine.Theme.DrawSysButton(Canvas, R, ngbMin, Hot, Down, FormActive, BorderStyle);
     end;
     kbiRollup: begin
       if WindowState <> kwsRollup then
         FThemeEngine.Theme.DrawSysButton(Canvas, R, ngbRollup, Hot, Down, FormActive, BorderStyle)
       else
         FThemeEngine.Theme.DrawSysButton(Canvas, R, ngbRolldown, Hot, Down, FormActive, BorderStyle);
     end;
     kbiTray: FThemeEngine.Theme.DrawSysButton(Canvas, R, ngbTray, Hot, Down, FormActive, BorderStyle);
   end;
 end;
var
  CaptionRect, R: TRect;
  Down, Hot: boolean;
  W: integer;
  S: WideString;
  SavedIndex: integer;
  SavedDC: HDC;
  Buffer: TTeBitmap;
begin
  if UseTheme then
    with FThemeEngine.Theme do
    begin
      { Paint Caption }
      CaptionRect := Rect(0, 0, Width, 0);
      case BorderStyle of
        kbsStandard: begin
          CaptionRect.Bottom := CaptionRect.Top + GetMetrix(ngmCaptionHeight);
          Inc(CaptionRect.Left, GetMetrix(ngmButtonMarginX));
          Inc(CaptionRect.Top, GetMetrix(ngmButtonMarginY));
          Dec(CaptionRect.Right, GetMetrix(ngmButtonMarginX));
        end;
        kbsToolWindow: begin
          CaptionRect.Bottom := CaptionRect.Top + GetMetrix(ngmSmCaptionHeight);
          Inc(CaptionRect.Left, GetMetrix(ngmSmButtonMarginX)+4);
          Inc(CaptionRect.Top, GetMetrix(ngmSmButtonMarginY));
          Dec(CaptionRect.Right, GetMetrix(ngmSmButtonMarginX));
        end;
      end;

      { Draw border }
      if Performance = ksfpNoBuffer then
      begin
        { Draw part of border }
        SavedIndex := SaveDC(Canvas.Handle);
        with CaptionRect do
          ExcludeClipRect(Canvas.Handle, 0, 0, Width, Bottom);
        DrawWindow(Canvas, Width, Height, GetClientBounds, FormActive, BorderStyle);
        RestoreDC(Canvas.Handle, SavedIndex);

        { Create buffer }
        Buffer := TTeBitmap.Create;
        Buffer.SetSize(Width, CaptionRect.Bottom);

        SavedDC := Canvas.Handle;
        Canvas.Handle := Buffer.DC;

        DrawWindow(Canvas, Width, Height, GetClientBounds, FormActive, BorderStyle);
      end
      else
        DrawWindow(Canvas, Width, Height, GetClientBounds, FormActive, BorderStyle);

      { Draw Text }
      if Form <> nil then
      begin
        if BorderStyle = kbsToolWindow then
          Canvas.Font := SmCaptionFont
        else
          Canvas.Font := CaptionFont;

      R := CaptionRect;

      if (BorderStyle <> kbsToolWindow) and (kbiSystemMenu in BorderIcons) then
        Inc(R.Left, RectHeight(CaptionRect));

      { Calc caption width }
      if (BorderStyle = kbsToolWindow) then
      begin
        Canvas.Font.Name := GetFontName(ngfSmCaptionText);
        Canvas.Font.Size := GetFontSize(ngfSmCaptionText);
        Canvas.Font.Style := GetFontStyle(ngfSmCaptionText);
        S := FormatStr(Canvas.Handle, Caption, Width - R.Left - GetMetrix(ngmSmButtonWidth))
      end
      else
      begin
        Canvas.Font.Name := GetFontName(ngfCaptionText);
        Canvas.Font.Size := GetFontSize(ngfCaptionText);
        Canvas.Font.Style := GetFontStyle(ngfCaptionText);
        if WindowState = kwsMinimized then
          S := FormatStr(Canvas.Handle, Caption, Width - R.Left - GetMetrix(ngmButtonWidth)*2)
        else
        begin
          W := Width - R.Left - GetMetrix(ngmButtonWidth)*4;
          if kbiHelp in BorderIcons then
            W := W - GetMetrix(ngmButtonWidth);
          if (kbiRollup in BorderIcons) and (GetThemeName <> 'XP ThemeAPI') then
            W := W - GetMetrix(ngmButtonWidth);
          if (kbiTray in BorderIcons) and  (GetThemeName <> 'XP ThemeAPI') then
            W := W - GetMetrix(ngmButtonWidth);

          S := FormatStr(Canvas.Handle, Caption, W);
        end;
      end;

      { Draw TextShadow }
      if HasCaptionTextShadow then
      begin
        OffsetRect(R, 1, 1);
        if (GetColor(ngcCaptionShadow) <> -1) and (FormActive) then
        begin
          if (GetRValue(ColorToRGB(GetColor(ngcCaptionText))) < 20) and
             (GetGValue(ColorToRGB(GetColor(ngcCaptionText))) < 20) and
             (GetBValue(ColorToRGB(GetColor(ngcCaptionText))) < 20)
          then
            Canvas.Font.Color := clBtnShadow
          else
            Canvas.Font.Color := GetColor(ngcCaptionShadow);

          if Form <> nil then
            DrawText(Canvas, S, R, Form.DrawTextBiDiModeFlags(DT_LEFT or DT_VCenter or DT_SINGLELINE));
        end;
      end
      else
        OffsetRect(R, 0, 1);
      { Draw Text }
      if FormActive then Canvas.Font.Color := GetColor(ngcCaptionText)
      else Canvas.Font.Color := GetColor(ngcInactiveCaptionText);
      OffsetRect(R, -1, -1);
      if Form <> nil then
        DrawText(Canvas, S, R, Form.DrawTextBiDiModeFlags(DT_LEFT or DT_VCenter or DT_SINGLELINE));
    end;

    { Paint Buttons }

    { Close Button }
    if kbiClose in BorderIcons then
    begin
      R := GetCaptionButtonRect(kbiClose);

      Down := (ssLeft in Shift) and (DownHitTest = khtCloseButton) and (MoveHitTest = khtCloseButton);
      Hot := (MoveHitTest = khtCloseButton) and not Dragged;
      IntDrawButton(R, kbiClose, Hot, Down);
      { Draw Glyph }
    end;

    if BorderStyle <> kbsToolWindow then
    begin
      { Icon Button }
      if kbiSystemMenu in BorderIcons then
      begin
        R := GetCaptionButtonRect(kbiSystemMenu);
        IntDrawButton(R, kbiSystemMenu, false, false);
        { Draw Glyph }
      end;

      { Min Button }
      if kbiMinimize in BorderIcons then
      begin
        R := GetCaptionButtonRect(kbiMinimize);

        Down := (ssLeft in Shift) and (DownHitTest = khtMinButton) and (MoveHitTest = khtMinButton);
        Hot := (MoveHitTest = khtMinButton) and not Dragged;
        IntDrawButton(R, kbiMinimize, Hot, Down);
      end;

      { Max Button }
      if kbiMaximize in BorderIcons then
      begin
        R := GetCaptionButtonRect(kbiMaximize);

        Down := (ssLeft in Shift) and (DownHitTest = khtMaxButton) and (MoveHitTest = khtMaxButton);
        Hot := (MoveHitTest = khtMaxButton) and not Dragged;
        IntDrawButton(R, kbiMaximize, Hot, Down);
      end;

      { Help Button }
      if kbiHelp in BorderIcons then
      begin
        R := GetCaptionButtonRect(kbiHelp);

        Down := (ssLeft in Shift) and (DownHitTest = khtHelpButton) and (MoveHitTest = khtHelpButton);
        Hot := (MoveHitTest = khtHelpButton) and not Dragged;
        IntDrawButton(R, kbiHelp, Hot, Down);
      end;

      { Roll Button }
      if kbiRollup in BorderIcons then
      begin
        R := GetCaptionButtonRect(kbiRollup);

        Down := (ssLeft in Shift) and (DownHitTest = khtRollButton) and (MoveHitTest = khtRollButton);
        Hot := (MoveHitTest = khtRollButton) and not Dragged;
        IntDrawButton(R, kbiRollup, Hot, Down);
      end;

      { Tray Button }
      if kbiTray in BorderIcons then
      begin
        R := GetCaptionButtonRect(kbiTray);

        Down := (ssLeft in Shift) and (DownHitTest = khtTrayButton) and (MoveHitTest = khtTrayButton);
        Hot := (MoveHitTest = khtTrayButton) and not Dragged;
        IntDrawButton(R, kbiTray, Hot, Down);
      end;
    end;

    if Performance = ksfpNoBuffer then
    begin
      Canvas.Handle := SavedDC;

      Buffer.Draw(Canvas, 0, 0);
      Buffer.Free;
    end;
  end
  else
    inherited ;
end;

procedure TTeThemeForm.PaintClientArea;
begin
  if UseTheme then
  begin
    FillRect(Canvas, GetClientRect, FThemeEngine.Theme.GetColor(ngcBtnFace));
  end
  else
    inherited ;
end;

{ protected Routines ==========================================================}

function TTeThemeForm.GetCaptionButtonRect(Button: TTeBorderIcon): TRect;
var
  CaptionRect, BtnRect, R: TRect;
  Pos: integer;
begin
  if not UseTheme then
  begin
    Result := inherited GetCaptionButtonRect(Button);
    Exit; 
  end;

  Result := Rect(0, 0, 0, 0);

  if not UseTheme then Exit;
  if not (Button in BorderIcons) then Exit;

  with FThemeEngine.Theme do
  begin
    { Calc caption rect }
    CaptionRect := Rect(0, 0, Width, 0);
    case BorderStyle of
      kbsStandard: begin
        CaptionRect.Bottom := CaptionRect.Top + GetMetrix(ngmCaptionHeight);
        Inc(CaptionRect.Left, GetMetrix(ngmBorderWidth));
        Inc(CaptionRect.Top, GetMetrix(ngmBorderWidth));
        Dec(CaptionRect.Right, GetMetrix(ngmBorderWidth));
      end;
      kbsToolWindow: begin
        CaptionRect.Bottom := CaptionRect.Top + GetMetrix(ngmSmCaptionHeight);
        Inc(CaptionRect.Left, GetMetrix(ngmSmBorderWidth));
        Inc(CaptionRect.Top, GetMetrix(ngmSmBorderWidth));
        Dec(CaptionRect.Right, GetMetrix(ngmSmBorderWidth));
      end;
    end;

    if Button = kbiSystemMenu then
    begin
      Result := CaptionRect;
      Result.Right := Result.Left + RectHeight(CaptionRect);
      Exit;
    end;

    { Calc Button size }
    if BorderStyle = kbsToolWindow then
    begin
      BtnRect := Rect(0, 0, GetMetrix(ngmSmButtonWidth), GetMetrix(ngmSmButtonHeight));
      { Start button position }
      Pos := Width - GetMetrix(ngmSmButtonMarginX) - RectWidth(BtnRect);
    end
    else
    begin
      BtnRect := Rect(0, 0, GetMetrix(ngmButtonWidth), GetMetrix(ngmButtonHeight));
      { Start button position }
      Pos := Width - GetMetrix(ngmButtonMarginX) - RectWidth(BtnRect);
    end;


    { Close Button }
    if kbiClose in BorderIcons then
    begin
      R := BtnRect;

      if BorderStyle = kbsToolWindow then
        OffsetRect(R, Pos, GetMetrix(ngmSmButtonMarginY))
      else
        OffsetRect(R, Pos, GetMetrix(ngmButtonMarginY));

      if Button = kbiClose then
      begin
        Result := R;
        Exit;
      end;

      Dec(Pos, RectWidth(BtnRect)+GetMetrix(ngmButtonSpace));
    end;

    if BorderStyle = kbsToolWindow then Exit;

    { Max Button }
    if (WindowState <> kwsMinimized) and (kbiMaximize in BorderIcons) then
    begin
      R := BtnRect;
      OffsetRect(R, Pos, GetMetrix(ngmButtonMarginY));

      if Button = kbiMaximize then
      begin
        Result := R;
        Exit;
      end;

      Dec(Pos, RectWidth(BtnRect)+GetMetrix(ngmButtonSpace));
    end;

    { Min Button }
    if kbiMinimize in BorderIcons then
    begin
      R := BtnRect;
      OffsetRect(R, Pos, GetMetrix(ngmButtonMarginY));

      if Button = kbiMinimize then
      begin
        Result := R;
        Exit;
      end;

      Dec(Pos, RectWidth(BtnRect)+GetMetrix(ngmButtonSpace));
    end;

    { Roll Button }
    if (WindowState <> kwsMinimized) and (kbiRollup in BorderIcons) then
    begin
      R := BtnRect;
      OffsetRect(R, Pos, GetMetrix(ngmButtonMarginY));

      if Button = kbiRollup then
      begin
        Result := R;
        Exit;
      end;

      Dec(Pos, RectWidth(BtnRect)+GetMetrix(ngmButtonSpace));
    end;

    { Tray Button }
    if (WindowState <> kwsMinimized) and (kbiTray in BorderIcons) then
    begin
      R := BtnRect;
      OffsetRect(R, Pos, GetMetrix(ngmButtonMarginY));

      if Button = kbiTray then
      begin
        Result := R;
        Exit;
      end;

      Dec(Pos, RectWidth(BtnRect)+GetMetrix(ngmButtonSpace));
    end;

    { Help Button }
    if (WindowState <> kwsRollup) and (WindowState <> kwsMinimized) and (kbiHelp in BorderIcons) then
    begin
      R := BtnRect;
      OffsetRect(R, Pos, GetMetrix(ngmButtonMarginY));

      if Button = kbiHelp then
      begin
        Result := R;
        Exit;
      end;

      Dec(Pos, RectWidth(BtnRect)+GetMetrix(ngmButtonSpace));
    end;
  end;
end;

function TTeThemeForm.GetClientBounds: TRect;
begin
  if UseTheme then
    with FThemeEngine.Theme do
    begin
      if (Form <> nil) and (THackForm(Form).FormStyle = fsMDIChild) and
         (WindowState = kwsMaximized) then
      begin
        Result := inherited GetClientBounds;
        Exit;
      end;

      case BorderStyle of
        kbsStandard: Result := Rect(GetMetrix(ngmBorderWidth), GetMetrix(ngmCaptionHeight),
          Width - GetMetrix(ngmBorderWidth), Height - GetMetrix(ngmBorderWidth));
        kbsToolWindow: Result := Rect(GetMetrix(ngmSmBorderWidth), GetMetrix(ngmSmCaptionHeight),
          Width - GetMetrix(ngmSmBorderWidth), Height - GetMetrix(ngmSmBorderWidth));
      else
        Result := inherited GetClientBounds;
      end;
    end
  else
    Result := inherited GetClientBounds;
end;

{ Tracking }

function TTeThemeForm.GetMaxTrackSize: TPoint;
begin
  Result := inherited GetMaxTrackSize;
end;

function TTeThemeForm.GetMinTrackSize: TPoint;
begin
  Result := inherited GetMinTrackSize;
end;

{ State's rect }

function TTeThemeForm.GetMaximizedRect: TRect;
begin
  if UseTheme then
  begin
    Result := GetRectOfMonitorContainingWindow(Handle, not StayOnTop);
    if (Form <> nil) and (THackForm(Form).FormStyle = fsMDIChild) then
      Result := inherited GetMaximizedRect;
  end
  else
    Result := inherited GetMaximizedRect;
end;

function TTeThemeForm.GetMinimizedRect: TRect;
begin
  Result := inherited GetMinimizedRect;
end;

function TTeThemeForm.GetRollupRect: TRect;
begin
  Result := inherited GetRollupRect;
end;

function TTeThemeForm.CreateMenuItem(AOwner: TComponent): TTeCustomItem;
begin
  Result := TTeThemeItem.Create(AOwner);
  TTeThemeItem(Result).ThemeEngine := FThemeEngine;
end;

function TTeThemeForm.GetHitTest(X, Y: integer): TTeHitTest;
begin
  Result := inherited GetHitTest(X, Y);

  { Correct for theme caption }
  if UseTheme then
    if (Result in [khtTop, khtTopLeft, khtTopRight]) and
       (X > GetClientBounds.Left) and (X < GetClientBounds.Right) and
       (((BorderStyle = kbsStandard) and (Y > FThemeEngine.Theme.GetMetrix(ngmBorderWidth))) or
        ((BorderStyle = kbsToolWindow) and (Y > FThemeEngine.Theme.GetMetrix(ngmSmBorderWidth))))
    then
      Result := khtCaption;
end;

{ VCL Routines ================================================================}

procedure TTeThemeForm.Notification(AComponent: TComponent;
  Operation: TOperation);
begin
  inherited;
  if (Operation = opRemove) and (AComponent = FThemeEngine) then
    ThemeEngine := nil;
end;

{ Properties }

function TTeThemeForm.GetVersion: TTeThemeVersion;
begin
  Result := sTeThemeVersion;
end;

procedure TTeThemeForm.SetThemeEngine(const Value: TTeThemeEngine);
begin
  FThemeEngine := Value;

  if csDestroying in ComponentState then Exit;

  if Form <> nil then
  begin
    Update;
    UpdateControls;
  end;
end;

procedure TTeThemeForm.SetVersion(const Value: TTeThemeVersion);
begin
end;

initialization
{$IFDEF ThemeTrial}
  ShowVersion2;
{$ENDIF}
end.
