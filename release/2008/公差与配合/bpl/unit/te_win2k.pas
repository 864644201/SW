{==============================================================================

  Windows 2k Declarations
  Copyright (C) 2000-2002 by Evgeny Kryukov
  All rights reserved

  Pascal code is Marcel van Brakel (brakelm@chello.nl)                        

  All contents of this file and all other files included in this archive
  are Copyright (C) 2002 Evgeny Kryukov. Use and/or distribution of
  them requires acceptance of the License Agreement.

  See License.txt for licence information

  $Id: te_win2k.pas,v 1.10.2.1 2003/01/23 16:14:46 evgeny Exp $

===============================================================================}

unit te_win2k;

{$I te_define.Inc}

interface

uses
  Windows, CommCtrl;

const

  WS_EX_LAYERED = $00080000;

  LWA_COLORKEY  = $00000001;
  LWA_ALPHA     = $00000002;

  ULW_COLORKEY = $00000001;
  ULW_ALPHA    = $00000002;
  ULW_OPAQUE   = $00000004;

type

  PBlendFunction = ^TBlendFunction;
  _BLENDFUNCTION = record
    BlendOp: BYTE;
    BlendFlags: BYTE;
    SourceConstantAlpha: BYTE;
    AlphaFormat: BYTE;
  end;

  BLENDFUNCTION = _BLENDFUNCTION;
  LPBLENDFUNCTION = ^BLENDFUNCTION;
  TBlendFunction = _BLENDFUNCTION;

var

  SetLayeredWindowAttributes: function (hwnd: HWND; crKey: COLORREF; bAlpha: BYTE;
    dwFlags: DWORD): BOOL; stdcall;
  UpdateLayeredWindow: function (hWnd: HWND; hdcDst: HDC; pptDst: PPOINT;
    psize: PSIZE; hdcSrc: HDC; pptSrc: PPOINT; crKey: COLORREF;
    pblend: LPBLENDFUNCTION; dwFlags: DWORD): BOOL; stdcall;

function IsWin2k: boolean;

implementation {===============================================================}

const
  User32 = 'user32.dll';
  RunOnWin2K: boolean = false;
  User32Lib: Integer = 0;

procedure Initialize;
begin
  User32Lib := LoadLibrary(User32);
  if User32Lib <> 0 then
  begin
    @SetLayeredWindowAttributes := GetProcAddress(User32Lib, 'SetLayeredWindowAttributes');
    @UpdateLayeredWindow := GetProcAddress(User32Lib, 'UpdateLayeredWindow');

    if @SetLayeredWindowAttributes <> nil then
      RunOnWin2k := true;
  end;
end;

function IsWin2k: boolean;
begin
  Result := RunOnWin2K;
end;

initialization
  Initialize;
finalization
  if User32Lib <> 0 then
    FreeLibrary(User32Lib);
end.
