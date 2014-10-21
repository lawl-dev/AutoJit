using AutoJITRuntime;

namespace AutoJITScript
{
    public class AutoJITScriptClass
    {
        private readonly AutoitContext<AutoJITScriptClass> _context;
        public AutoitRuntime<AutoJITScriptClass> _functions;
        private Variant v_ACCESS_SYSTEM_SECURITY = Variant.Create( (object) null );
        private Variant v_ANSI_FIXED_FONT = Variant.Create( (object) null );
        private Variant v_ANSI_VAR_FONT = Variant.Create( (object) null );

        private Variant v_BLACK_BRUSH = Variant.Create( (object) null );
        private Variant v_BLACK_PEN = Variant.Create( (object) null );
        private Variant v_CCS_ADJUSTABLE = Variant.Create( (object) null );
        private Variant v_CCS_BOTTOM = Variant.Create( (object) null );
        private Variant v_CCS_LEFT = Variant.Create( (object) null );
        private Variant v_CCS_NODIVIDER = Variant.Create( (object) null );
        private Variant v_CCS_NOHILITE = Variant.Create( (object) null );
        private Variant v_CCS_NOMOVEX = Variant.Create( (object) null );
        private Variant v_CCS_NOMOVEY = Variant.Create( (object) null );
        private Variant v_CCS_NOPARENTALIGN = Variant.Create( (object) null );
        private Variant v_CCS_NORESIZE = Variant.Create( (object) null );
        private Variant v_CCS_RIGHT = Variant.Create( (object) null );
        private Variant v_CCS_TOP = Variant.Create( (object) null );
        private Variant v_CCS_VERT = Variant.Create( (object) null );
        private Variant v_CLR_INVALID = Variant.Create( (object) null );
        private Variant v_CREATE_ALWAYS = Variant.Create( (object) null );
        private Variant v_CREATE_NEW = Variant.Create( (object) null );
        private Variant v_DC_BRUSH = Variant.Create( (object) null );
        private Variant v_DC_PEN = Variant.Create( (object) null );
        private Variant v_DEFAULT_GUI_FONT = Variant.Create( (object) null );
        private Variant v_DEFAULT_PALETTE = Variant.Create( (object) null );
        private Variant v_DENY_ACCESS = Variant.Create( (object) null );
        private Variant v_DEVICE_DEFAULT_FONT = Variant.Create( (object) null );
        private Variant v_DISPLAY_DEVICE_ATTACHED_TO_DESKTOP = Variant.Create( (object) null );
        private Variant v_DISPLAY_DEVICE_DISCONNECT = Variant.Create( (object) null );
        private Variant v_DISPLAY_DEVICE_MIRRORING_DRIVER = Variant.Create( (object) null );
        private Variant v_DISPLAY_DEVICE_MODESPRUNED = Variant.Create( (object) null );
        private Variant v_DISPLAY_DEVICE_MULTI_DRIVER = Variant.Create( (object) null );
        private Variant v_DISPLAY_DEVICE_PRIMARY_DEVICE = Variant.Create( (object) null );
        private Variant v_DISPLAY_DEVICE_REMOTE = Variant.Create( (object) null );
        private Variant v_DISPLAY_DEVICE_REMOVABLE = Variant.Create( (object) null );
        private Variant v_DISPLAY_DEVICE_VGA_COMPATIBLE = Variant.Create( (object) null );
        private Variant v_DI_COMPAT = Variant.Create( (object) null );
        private Variant v_DI_DEFAULTSIZE = Variant.Create( (object) null );
        private Variant v_DI_IMAGE = Variant.Create( (object) null );
        private Variant v_DI_MASK = Variant.Create( (object) null );
        private Variant v_DI_NOMIRROR = Variant.Create( (object) null );
        private Variant v_DI_NORMAL = Variant.Create( (object) null );
        private Variant v_DKGRAY_BRUSH = Variant.Create( (object) null );
        private Variant v_DLG_MOVEABLE = Variant.Create( (object) null );
        private Variant v_DLG_NOTITLE = Variant.Create( (object) null );
        private Variant v_DLG_NOTONTOP = Variant.Create( (object) null );
        private Variant v_DLG_TEXTLEFT = Variant.Create( (object) null );
        private Variant v_DLG_TEXTRIGHT = Variant.Create( (object) null );
        private Variant v_DLG_TEXTVCENTER = Variant.Create( (object) null );
        private Variant v_DONT_RESOLVE_DLL_REFERENCES = Variant.Create( (object) null );
        private Variant v_DT_BUSTYPE = Variant.Create( (object) null );
        private Variant v_DT_DRIVETYPE = Variant.Create( (object) null );
        private Variant v_DT_SSDSTATUS = Variant.Create( (object) null );
        private Variant v_DUPLICATE_CLOSE_SOURCE = Variant.Create( (object) null );
        private Variant v_DUPLICATE_SAME_ACCESS = Variant.Create( (object) null );
        private Variant v_EOF = Variant.Create( (object) null );
        private Variant v_E_ABORT = Variant.Create( (object) null );
        private Variant v_E_ACCESSDENIED = Variant.Create( (object) null );
        private Variant v_E_FAIL = Variant.Create( (object) null );
        private Variant v_E_HANDLE = Variant.Create( (object) null );
        private Variant v_E_INVALIDARG = Variant.Create( (object) null );
        private Variant v_E_NOINTERFACE = Variant.Create( (object) null );
        private Variant v_E_NOTIMPL = Variant.Create( (object) null );
        private Variant v_E_OUTOFMEMORY = Variant.Create( (object) null );
        private Variant v_E_POINTER = Variant.Create( (object) null );
        private Variant v_E_UNEXPECTED = Variant.Create( (object) null );
        private Variant v_FC_CREATEPATH = Variant.Create( (object) null );
        private Variant v_FC_NOOVERWRITE = Variant.Create( (object) null );
        private Variant v_FC_OVERWRITE = Variant.Create( (object) null );
        private Variant v_FD_FILEMUSTEXIST = Variant.Create( (object) null );
        private Variant v_FD_MULTISELECT = Variant.Create( (object) null );
        private Variant v_FD_PATHMUSTEXIST = Variant.Create( (object) null );
        private Variant v_FD_PROMPTCREATENEW = Variant.Create( (object) null );
        private Variant v_FD_PROMPTOVERWRITE = Variant.Create( (object) null );
        private Variant v_FILE_ATTRIBUTE_ARCHIVE = Variant.Create( (object) null );
        private Variant v_FILE_ATTRIBUTE_COMPRESSED = Variant.Create( (object) null );
        private Variant v_FILE_ATTRIBUTE_DEVICE = Variant.Create( (object) null );
        private Variant v_FILE_ATTRIBUTE_DIRECTORY = Variant.Create( (object) null );
        private Variant v_FILE_ATTRIBUTE_ENCRYPTED = Variant.Create( (object) null );
        private Variant v_FILE_ATTRIBUTE_HIDDEN = Variant.Create( (object) null );
        private Variant v_FILE_ATTRIBUTE_NORMAL = Variant.Create( (object) null );
        private Variant v_FILE_ATTRIBUTE_NOT_CONTENT_INDEXED = Variant.Create( (object) null );
        private Variant v_FILE_ATTRIBUTE_OFFLINE = Variant.Create( (object) null );
        private Variant v_FILE_ATTRIBUTE_READONLY = Variant.Create( (object) null );
        private Variant v_FILE_ATTRIBUTE_REPARSE_POINT = Variant.Create( (object) null );
        private Variant v_FILE_ATTRIBUTE_SPARSE_FILE = Variant.Create( (object) null );
        private Variant v_FILE_ATTRIBUTE_SYSTEM = Variant.Create( (object) null );
        private Variant v_FILE_ATTRIBUTE_TEMPORARY = Variant.Create( (object) null );
        private Variant v_FILE_BEGIN = Variant.Create( (object) null );
        private Variant v_FILE_CURRENT = Variant.Create( (object) null );
        private Variant v_FILE_END = Variant.Create( (object) null );
        private Variant v_FILE_SHARE_ANY = Variant.Create( (object) null );
        private Variant v_FILE_SHARE_DELETE = Variant.Create( (object) null );
        private Variant v_FILE_SHARE_READ = Variant.Create( (object) null );
        private Variant v_FILE_SHARE_READWRITE = Variant.Create( (object) null );
        private Variant v_FILE_SHARE_WRITE = Variant.Create( (object) null );
        private Variant v_FLASHW_CAPTION = Variant.Create( (object) null );
        private Variant v_FLASHW_TIMER = Variant.Create( (object) null );
        private Variant v_FLASHW_TIMERNOFG = Variant.Create( (object) null );
        private Variant v_FLASHW_TRAY = Variant.Create( (object) null );
        private Variant v_FLTAR_FASTSORT = Variant.Create( (object) null );
        private Variant v_FLTAR_FILES = Variant.Create( (object) null );
        private Variant v_FLTAR_FILESFOLDERS = Variant.Create( (object) null );
        private Variant v_FLTAR_FOLDERS = Variant.Create( (object) null );
        private Variant v_FLTAR_FULLPATH = Variant.Create( (object) null );
        private Variant v_FLTAR_NOHIDDEN = Variant.Create( (object) null );
        private Variant v_FLTAR_NOLINK = Variant.Create( (object) null );
        private Variant v_FLTAR_NOPATH = Variant.Create( (object) null );
        private Variant v_FLTAR_NORECUR = Variant.Create( (object) null );
        private Variant v_FLTAR_NOSORT = Variant.Create( (object) null );
        private Variant v_FLTAR_NOSYSTEM = Variant.Create( (object) null );
        private Variant v_FLTAR_RECUR = Variant.Create( (object) null );
        private Variant v_FLTAR_RELPATH = Variant.Create( (object) null );
        private Variant v_FLTAR_SORT = Variant.Create( (object) null );
        private Variant v_FLTA_FILES = Variant.Create( (object) null );
        private Variant v_FLTA_FILESFOLDERS = Variant.Create( (object) null );
        private Variant v_FLTA_FOLDERS = Variant.Create( (object) null );
        private Variant v_FORMAT_MESSAGE_ALLOCATE_BUFFER = Variant.Create( (object) null );
        private Variant v_FORMAT_MESSAGE_ARGUMENT_ARRAY = Variant.Create( (object) null );
        private Variant v_FORMAT_MESSAGE_FROM_HMODULE = Variant.Create( (object) null );
        private Variant v_FORMAT_MESSAGE_FROM_STRING = Variant.Create( (object) null );
        private Variant v_FORMAT_MESSAGE_FROM_SYSTEM = Variant.Create( (object) null );
        private Variant v_FORMAT_MESSAGE_IGNORE_INSERTS = Variant.Create( (object) null );
        private Variant v_FO_APPEND = Variant.Create( (object) null );
        private Variant v_FO_BINARY = Variant.Create( (object) null );
        private Variant v_FO_CREATEPATH = Variant.Create( (object) null );
        private Variant v_FO_OVERWRITE = Variant.Create( (object) null );
        private Variant v_FO_READ = Variant.Create( (object) null );
        private Variant v_FO_UNICODE = Variant.Create( (object) null );
        private Variant v_FO_UTF16_BE = Variant.Create( (object) null );
        private Variant v_FO_UTF16_LE = Variant.Create( (object) null );
        private Variant v_FO_UTF8 = Variant.Create( (object) null );
        private Variant v_FO_UTF8_FULL = Variant.Create( (object) null );
        private Variant v_FO_UTF8_NOBOM = Variant.Create( (object) null );
        private Variant v_FRTA_COUNT = Variant.Create( (object) null );
        private Variant v_FRTA_ENTIRESPLIT = Variant.Create( (object) null );
        private Variant v_FRTA_INTARRAYS = Variant.Create( (object) null );
        private Variant v_FRTA_NOCOUNT = Variant.Create( (object) null );
        private Variant v_FT_ACCESSED = Variant.Create( (object) null );
        private Variant v_FT_CREATED = Variant.Create( (object) null );
        private Variant v_FT_MODIFIED = Variant.Create( (object) null );
        private Variant v_GENERIC_ALL = Variant.Create( (object) null );
        private Variant v_GENERIC_EXECUTE = Variant.Create( (object) null );
        private Variant v_GENERIC_READ = Variant.Create( (object) null );
        private Variant v_GENERIC_READWRITE = Variant.Create( (object) null );
        private Variant v_GENERIC_WRITE = Variant.Create( (object) null );
        private Variant v_GRANT_ACCESS = Variant.Create( (object) null );
        private Variant v_GRAY_BRUSH = Variant.Create( (object) null );
        private Variant v_GWL_EXSTYLE = Variant.Create( (object) null );
        private Variant v_GWL_HINSTANCE = Variant.Create( (object) null );
        private Variant v_GWL_HWNDPARENT = Variant.Create( (object) null );
        private Variant v_GWL_ID = Variant.Create( (object) null );
        private Variant v_GWL_STYLE = Variant.Create( (object) null );
        private Variant v_GWL_USERDATA = Variant.Create( (object) null );
        private Variant v_GWL_WNDPROC = Variant.Create( (object) null );
        private Variant v_GW_CHILD = Variant.Create( (object) null );
        private Variant v_GW_ENABLEDPOPUP = Variant.Create( (object) null );
        private Variant v_GW_HWNDFIRST = Variant.Create( (object) null );
        private Variant v_GW_HWNDLAST = Variant.Create( (object) null );
        private Variant v_GW_HWNDNEXT = Variant.Create( (object) null );
        private Variant v_GW_HWNDPREV = Variant.Create( (object) null );
        private Variant v_GW_OWNER = Variant.Create( (object) null );
        private Variant v_HGDI_ERROR = Variant.Create( (object) null );
        private Variant v_HOLLOW_BRUSH = Variant.Create( (object) null );
        private Variant v_HWND_BOTTOM = Variant.Create( (object) null );
        private Variant v_HWND_NOTOPMOST = Variant.Create( (object) null );
        private Variant v_HWND_TOP = Variant.Create( (object) null );
        private Variant v_HWND_TOPMOST = Variant.Create( (object) null );
        private Variant v_IDABORT = Variant.Create( (object) null );
        private Variant v_IDCANCEL = Variant.Create( (object) null );
        private Variant v_IDCLOSE = Variant.Create( (object) null );
        private Variant v_IDCONTINUE = Variant.Create( (object) null );
        private Variant v_IDC_APPSTARTING = Variant.Create( (object) null );
        private Variant v_IDC_ARROW = Variant.Create( (object) null );
        private Variant v_IDC_CROSS = Variant.Create( (object) null );
        private Variant v_IDC_HAND = Variant.Create( (object) null );
        private Variant v_IDC_HELP = Variant.Create( (object) null );
        private Variant v_IDC_IBEAM = Variant.Create( (object) null );
        private Variant v_IDC_ICON = Variant.Create( (object) null );
        private Variant v_IDC_NO = Variant.Create( (object) null );
        private Variant v_IDC_SIZE = Variant.Create( (object) null );
        private Variant v_IDC_SIZEALL = Variant.Create( (object) null );
        private Variant v_IDC_SIZENESW = Variant.Create( (object) null );
        private Variant v_IDC_SIZENS = Variant.Create( (object) null );
        private Variant v_IDC_SIZENWSE = Variant.Create( (object) null );
        private Variant v_IDC_SIZEWE = Variant.Create( (object) null );
        private Variant v_IDC_UNKNOWN = Variant.Create( (object) null );
        private Variant v_IDC_UPARROW = Variant.Create( (object) null );
        private Variant v_IDC_WAIT = Variant.Create( (object) null );
        private Variant v_IDHELP = Variant.Create( (object) null );
        private Variant v_IDIGNORE = Variant.Create( (object) null );
        private Variant v_IDI_APPLICATION = Variant.Create( (object) null );
        private Variant v_IDI_ASTERISK = Variant.Create( (object) null );
        private Variant v_IDI_ERROR = Variant.Create( (object) null );
        private Variant v_IDI_EXCLAMATION = Variant.Create( (object) null );
        private Variant v_IDI_HAND = Variant.Create( (object) null );
        private Variant v_IDI_INFORMATION = Variant.Create( (object) null );
        private Variant v_IDI_QUESTION = Variant.Create( (object) null );
        private Variant v_IDI_SHIELD = Variant.Create( (object) null );
        private Variant v_IDI_WARNING = Variant.Create( (object) null );
        private Variant v_IDI_WINLOGO = Variant.Create( (object) null );
        private Variant v_IDNO = Variant.Create( (object) null );
        private Variant v_IDOK = Variant.Create( (object) null );
        private Variant v_IDRETRY = Variant.Create( (object) null );
        private Variant v_IDTIMEOUT = Variant.Create( (object) null );
        private Variant v_IDTRYAGAIN = Variant.Create( (object) null );
        private Variant v_IDYES = Variant.Create( (object) null );
        private Variant v_IMAGE_BITMAP = Variant.Create( (object) null );
        private Variant v_IMAGE_CURSOR = Variant.Create( (object) null );
        private Variant v_IMAGE_ENHMETAFILE = Variant.Create( (object) null );
        private Variant v_IMAGE_ICON = Variant.Create( (object) null );
        private Variant v_INVALID_HANDLE_VALUE = Variant.Create( (object) null );
        private Variant v_INVALID_SET_FILE_POINTER = Variant.Create( (object) null );
        private Variant v_KB_CAPSOFF = Variant.Create( (object) null );
        private Variant v_KB_CAPSON = Variant.Create( (object) null );
        private Variant v_KB_SENDRAW = Variant.Create( (object) null );
        private Variant v_KB_SENDSPECIAL = Variant.Create( (object) null );
        private Variant v_KEYWORD_DEFAULT = Variant.Create( (object) null );
        private Variant v_KEYWORD_NULL = Variant.Create( (object) null );
        private Variant v_KF_ALTDOWN = Variant.Create( (object) null );
        private Variant v_KF_EXTENDED = Variant.Create( (object) null );
        private Variant v_KF_UP = Variant.Create( (object) null );
        private Variant v_LLKHF_ALTDOWN = Variant.Create( (object) null );
        private Variant v_LLKHF_EXTENDED = Variant.Create( (object) null );
        private Variant v_LLKHF_INJECTED = Variant.Create( (object) null );
        private Variant v_LLKHF_UP = Variant.Create( (object) null );
        private Variant v_LOAD_IGNORE_CODE_AUTHZ_LEVEL = Variant.Create( (object) null );
        private Variant v_LOAD_LIBRARY_AS_DATAFILE = Variant.Create( (object) null );
        private Variant v_LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE = Variant.Create( (object) null );
        private Variant v_LOAD_LIBRARY_AS_IMAGE_RESOURCE = Variant.Create( (object) null );
        private Variant v_LOAD_LIBRARY_SEARCH_APPLICATION_DIR = Variant.Create( (object) null );
        private Variant v_LOAD_LIBRARY_SEARCH_DEFAULT_DIRS = Variant.Create( (object) null );
        private Variant v_LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR = Variant.Create( (object) null );
        private Variant v_LOAD_LIBRARY_SEARCH_SYSTEM32 = Variant.Create( (object) null );
        private Variant v_LOAD_LIBRARY_SEARCH_USER_DIRS = Variant.Create( (object) null );
        private Variant v_LOAD_WITH_ALTERED_SEARCH_PATH = Variant.Create( (object) null );
        private Variant v_LOGON_NETCREDENTIALS_ONLY = Variant.Create( (object) null );
        private Variant v_LOGON_WITH_PROFILE = Variant.Create( (object) null );
        private Variant v_LR_COLOR = Variant.Create( (object) null );
        private Variant v_LR_COPYDELETEORG = Variant.Create( (object) null );
        private Variant v_LR_COPYFROMRESOURCE = Variant.Create( (object) null );
        private Variant v_LR_COPYRETURNORG = Variant.Create( (object) null );
        private Variant v_LR_CREATEDIBSECTION = Variant.Create( (object) null );
        private Variant v_LR_DEFAULTCOLOR = Variant.Create( (object) null );
        private Variant v_LR_DEFAULTSIZE = Variant.Create( (object) null );
        private Variant v_LR_LOADFROMFILE = Variant.Create( (object) null );
        private Variant v_LR_LOADMAP3DCOLORS = Variant.Create( (object) null );
        private Variant v_LR_LOADTRANSPARENT = Variant.Create( (object) null );
        private Variant v_LR_MONOCHROME = Variant.Create( (object) null );
        private Variant v_LR_SHARED = Variant.Create( (object) null );
        private Variant v_LR_VGACOLOR = Variant.Create( (object) null );
        private Variant v_LTGRAY_BRUSH = Variant.Create( (object) null );
        private Variant v_MB_ABORTRETRYIGNORE = Variant.Create( (object) null );
        private Variant v_MB_APPLMODAL = Variant.Create( (object) null );
        private Variant v_MB_CANCELTRYCONTINUE = Variant.Create( (object) null );
        private Variant v_MB_COMPOSITE = Variant.Create( (object) null );
        private Variant v_MB_DEFAULT_DESKTOP_ONLY = Variant.Create( (object) null );
        private Variant v_MB_DEFBUTTON1 = Variant.Create( (object) null );
        private Variant v_MB_DEFBUTTON2 = Variant.Create( (object) null );
        private Variant v_MB_DEFBUTTON3 = Variant.Create( (object) null );
        private Variant v_MB_DEFBUTTON4 = Variant.Create( (object) null );
        private Variant v_MB_HELP = Variant.Create( (object) null );
        private Variant v_MB_ICONASTERISK = Variant.Create( (object) null );
        private Variant v_MB_ICONERROR = Variant.Create( (object) null );
        private Variant v_MB_ICONEXCLAMATION = Variant.Create( (object) null );
        private Variant v_MB_ICONHAND = Variant.Create( (object) null );
        private Variant v_MB_ICONINFORMATION = Variant.Create( (object) null );
        private Variant v_MB_ICONQUESTION = Variant.Create( (object) null );
        private Variant v_MB_ICONSTOP = Variant.Create( (object) null );
        private Variant v_MB_ICONWARNING = Variant.Create( (object) null );
        private Variant v_MB_OK = Variant.Create( (object) null );
        private Variant v_MB_OKCANCEL = Variant.Create( (object) null );
        private Variant v_MB_PRECOMPOSED = Variant.Create( (object) null );
        private Variant v_MB_RETRYCANCEL = Variant.Create( (object) null );
        private Variant v_MB_RIGHT = Variant.Create( (object) null );
        private Variant v_MB_RIGHTJUSTIFIED = Variant.Create( (object) null );
        private Variant v_MB_RTLREADING = Variant.Create( (object) null );
        private Variant v_MB_SERVICE_NOTIFICATION = Variant.Create( (object) null );
        private Variant v_MB_SETFOREGROUND = Variant.Create( (object) null );
        private Variant v_MB_SYSTEMMODAL = Variant.Create( (object) null );
        private Variant v_MB_TASKMODAL = Variant.Create( (object) null );
        private Variant v_MB_TOPMOST = Variant.Create( (object) null );
        private Variant v_MB_USEGLYPHCHARS = Variant.Create( (object) null );
        private Variant v_MB_USERICON = Variant.Create( (object) null );
        private Variant v_MB_YESNO = Variant.Create( (object) null );
        private Variant v_MB_YESNOCANCEL = Variant.Create( (object) null );
        private Variant v_MOUSEEVENTF_ABSOLUTE = Variant.Create( (object) null );
        private Variant v_MOUSEEVENTF_LEFTDOWN = Variant.Create( (object) null );
        private Variant v_MOUSEEVENTF_LEFTUP = Variant.Create( (object) null );
        private Variant v_MOUSEEVENTF_MIDDLEDOWN = Variant.Create( (object) null );
        private Variant v_MOUSEEVENTF_MIDDLEUP = Variant.Create( (object) null );
        private Variant v_MOUSEEVENTF_MOVE = Variant.Create( (object) null );
        private Variant v_MOUSEEVENTF_RIGHTDOWN = Variant.Create( (object) null );
        private Variant v_MOUSEEVENTF_RIGHTUP = Variant.Create( (object) null );
        private Variant v_MOUSEEVENTF_WHEEL = Variant.Create( (object) null );
        private Variant v_MOUSEEVENTF_XDOWN = Variant.Create( (object) null );
        private Variant v_MOUSEEVENTF_XUP = Variant.Create( (object) null );
        private Variant v_NOT_USED_ACCESS = Variant.Create( (object) null );
        private Variant v_NULL_BRUSH = Variant.Create( (object) null );
        private Variant v_NULL_PEN = Variant.Create( (object) null );
        private Variant v_OBJID_ALERT = Variant.Create( (object) null );
        private Variant v_OBJID_CARET = Variant.Create( (object) null );
        private Variant v_OBJID_CURSOR = Variant.Create( (object) null );
        private Variant v_OBJID_SIZEGRIP = Variant.Create( (object) null );
        private Variant v_OBJID_SOUND = Variant.Create( (object) null );
        private Variant v_OBJID_TITLEBAR = Variant.Create( (object) null );
        private Variant v_OBJID_WINDOW = Variant.Create( (object) null );
        private Variant v_OBM_BTNCORNERS = Variant.Create( (object) null );
        private Variant v_OBM_BTSIZE = Variant.Create( (object) null );
        private Variant v_OBM_CHECK = Variant.Create( (object) null );
        private Variant v_OBM_CHECKBOXES = Variant.Create( (object) null );
        private Variant v_OBM_CLOSE = Variant.Create( (object) null );
        private Variant v_OBM_COMBO = Variant.Create( (object) null );
        private Variant v_OBM_DNARROW = Variant.Create( (object) null );
        private Variant v_OBM_DNARROWD = Variant.Create( (object) null );
        private Variant v_OBM_DNARROWI = Variant.Create( (object) null );
        private Variant v_OBM_LFARROW = Variant.Create( (object) null );
        private Variant v_OBM_LFARROWD = Variant.Create( (object) null );
        private Variant v_OBM_LFARROWI = Variant.Create( (object) null );
        private Variant v_OBM_MNARROW = Variant.Create( (object) null );
        private Variant v_OBM_OLD_CLOSE = Variant.Create( (object) null );
        private Variant v_OBM_OLD_DNARROW = Variant.Create( (object) null );
        private Variant v_OBM_OLD_LFARROW = Variant.Create( (object) null );
        private Variant v_OBM_OLD_REDUCE = Variant.Create( (object) null );
        private Variant v_OBM_OLD_RESTORE = Variant.Create( (object) null );
        private Variant v_OBM_OLD_RGARROW = Variant.Create( (object) null );
        private Variant v_OBM_OLD_UPARROW = Variant.Create( (object) null );
        private Variant v_OBM_OLD_ZOOM = Variant.Create( (object) null );
        private Variant v_OBM_REDUCE = Variant.Create( (object) null );
        private Variant v_OBM_REDUCED = Variant.Create( (object) null );
        private Variant v_OBM_RESTORE = Variant.Create( (object) null );
        private Variant v_OBM_RESTORED = Variant.Create( (object) null );
        private Variant v_OBM_RGARROW = Variant.Create( (object) null );
        private Variant v_OBM_RGARROWD = Variant.Create( (object) null );
        private Variant v_OBM_RGARROWI = Variant.Create( (object) null );
        private Variant v_OBM_SIZE = Variant.Create( (object) null );
        private Variant v_OBM_TRTYPE = Variant.Create( (object) null );
        private Variant v_OBM_UPARROW = Variant.Create( (object) null );
        private Variant v_OBM_UPARROWD = Variant.Create( (object) null );
        private Variant v_OBM_UPARROWI = Variant.Create( (object) null );
        private Variant v_OBM_ZOOM = Variant.Create( (object) null );
        private Variant v_OBM_ZOOMD = Variant.Create( (object) null );
        private Variant v_OEM_FIXED_FONT = Variant.Create( (object) null );
        private Variant v_OFN_ALLOWMULTISELECT = Variant.Create( (object) null );
        private Variant v_OFN_CREATEPROMPT = Variant.Create( (object) null );
        private Variant v_OFN_DONTADDTORECENT = Variant.Create( (object) null );
        private Variant v_OFN_ENABLEHOOK = Variant.Create( (object) null );
        private Variant v_OFN_ENABLEINCLUDENOTIFY = Variant.Create( (object) null );
        private Variant v_OFN_ENABLESIZING = Variant.Create( (object) null );
        private Variant v_OFN_ENABLETEMPLATE = Variant.Create( (object) null );
        private Variant v_OFN_ENABLETEMPLATEHANDLE = Variant.Create( (object) null );
        private Variant v_OFN_EXPLORER = Variant.Create( (object) null );
        private Variant v_OFN_EXTENSIONDIFFERENT = Variant.Create( (object) null );
        private Variant v_OFN_EX_NOPLACESBAR = Variant.Create( (object) null );
        private Variant v_OFN_FILEMUSTEXIST = Variant.Create( (object) null );
        private Variant v_OFN_FORCESHOWHIDDEN = Variant.Create( (object) null );
        private Variant v_OFN_HIDEREADONLY = Variant.Create( (object) null );
        private Variant v_OFN_LONGNAMES = Variant.Create( (object) null );
        private Variant v_OFN_NOCHANGEDIR = Variant.Create( (object) null );
        private Variant v_OFN_NODEREFERENCELINKS = Variant.Create( (object) null );
        private Variant v_OFN_NOLONGNAMES = Variant.Create( (object) null );
        private Variant v_OFN_NONETWORKBUTTON = Variant.Create( (object) null );
        private Variant v_OFN_NOREADONLYRETURN = Variant.Create( (object) null );
        private Variant v_OFN_NOTESTFILECREATE = Variant.Create( (object) null );
        private Variant v_OFN_NOVALIDATE = Variant.Create( (object) null );
        private Variant v_OFN_OVERWRITEPROMPT = Variant.Create( (object) null );
        private Variant v_OFN_PATHMUSTEXIST = Variant.Create( (object) null );
        private Variant v_OFN_READONLY = Variant.Create( (object) null );
        private Variant v_OFN_SHAREAWARE = Variant.Create( (object) null );
        private Variant v_OFN_SHOWHELP = Variant.Create( (object) null );
        private Variant v_OIC_BANG = Variant.Create( (object) null );
        private Variant v_OIC_ERROR = Variant.Create( (object) null );
        private Variant v_OIC_HAND = Variant.Create( (object) null );
        private Variant v_OIC_INFORMATION = Variant.Create( (object) null );
        private Variant v_OIC_NOTE = Variant.Create( (object) null );
        private Variant v_OIC_QUES = Variant.Create( (object) null );
        private Variant v_OIC_SAMPLE = Variant.Create( (object) null );
        private Variant v_OIC_WARNING = Variant.Create( (object) null );
        private Variant v_OIC_WINLOGO = Variant.Create( (object) null );
        private Variant v_OPEN_ALWAYS = Variant.Create( (object) null );
        private Variant v_OPEN_EXISTING = Variant.Create( (object) null );
        private Variant v_OPT_CAPSNOSTORE = Variant.Create( (object) null );
        private Variant v_OPT_CAPSSTORE = Variant.Create( (object) null );
        private Variant v_OPT_COORDSABSOLUTE = Variant.Create( (object) null );
        private Variant v_OPT_COORDSCLIENT = Variant.Create( (object) null );
        private Variant v_OPT_COORDSRELATIVE = Variant.Create( (object) null );
        private Variant v_OPT_ERRORFATAL = Variant.Create( (object) null );
        private Variant v_OPT_ERRORSILENT = Variant.Create( (object) null );
        private Variant v_OPT_MATCHADVANCED = Variant.Create( (object) null );
        private Variant v_OPT_MATCHANY = Variant.Create( (object) null );
        private Variant v_OPT_MATCHEXACT = Variant.Create( (object) null );
        private Variant v_OPT_MATCHSTART = Variant.Create( (object) null );
        private Variant v_READ_CONTROL = Variant.Create( (object) null );
        private Variant v_REG_BINARY = Variant.Create( (object) null );
        private Variant v_REG_DWORD = Variant.Create( (object) null );
        private Variant v_REG_DWORD_BIG_ENDIAN = Variant.Create( (object) null );
        private Variant v_REG_DWORD_LITTLE_ENDIAN = Variant.Create( (object) null );
        private Variant v_REG_EXPAND_SZ = Variant.Create( (object) null );
        private Variant v_REG_FULL_RESOURCE_DESCRIPTOR = Variant.Create( (object) null );
        private Variant v_REG_LINK = Variant.Create( (object) null );
        private Variant v_REG_MULTI_SZ = Variant.Create( (object) null );
        private Variant v_REG_NONE = Variant.Create( (object) null );
        private Variant v_REG_QWORD = Variant.Create( (object) null );
        private Variant v_REG_QWORD_LITTLE_ENDIAN = Variant.Create( (object) null );
        private Variant v_REG_RESOURCE_LIST = Variant.Create( (object) null );
        private Variant v_REG_RESOURCE_REQUIREMENTS_LIST = Variant.Create( (object) null );
        private Variant v_REG_SZ = Variant.Create( (object) null );
        private Variant v_REVOKE_ACCESS = Variant.Create( (object) null );
        private Variant v_RIGHTS_DELETE = Variant.Create( (object) null );
        private Variant v_RUN_CREATE_NEW_CONSOLE = Variant.Create( (object) null );
        private Variant v_SD_FORCE = Variant.Create( (object) null );
        private Variant v_SD_FORCEHUNG = Variant.Create( (object) null );
        private Variant v_SD_HIBERNATE = Variant.Create( (object) null );
        private Variant v_SD_LOGOFF = Variant.Create( (object) null );
        private Variant v_SD_POWERDOWN = Variant.Create( (object) null );
        private Variant v_SD_REBOOT = Variant.Create( (object) null );
        private Variant v_SD_SHUTDOWN = Variant.Create( (object) null );
        private Variant v_SD_STANDBY = Variant.Create( (object) null );
        private Variant v_SECURITYANONYMOUS = Variant.Create( (object) null );
        private Variant v_SECURITYDELEGATION = Variant.Create( (object) null );
        private Variant v_SECURITYIDENTIFICATION = Variant.Create( (object) null );
        private Variant v_SECURITYIMPERSONATION = Variant.Create( (object) null );
        private Variant v_SET_ACCESS = Variant.Create( (object) null );
        private Variant v_SET_AUDIT_FAILURE = Variant.Create( (object) null );
        private Variant v_SET_AUDIT_SUCCESS = Variant.Create( (object) null );
        private Variant v_SE_ASSIGNPRIMARYTOKEN_NAME = Variant.Create( (object) null );
        private Variant v_SE_AUDIT_NAME = Variant.Create( (object) null );
        private Variant v_SE_BACKUP_NAME = Variant.Create( (object) null );
        private Variant v_SE_CHANGE_NOTIFY_NAME = Variant.Create( (object) null );
        private Variant v_SE_CREATE_GLOBAL_NAME = Variant.Create( (object) null );
        private Variant v_SE_CREATE_PAGEFILE_NAME = Variant.Create( (object) null );
        private Variant v_SE_CREATE_PERMANENT_NAME = Variant.Create( (object) null );
        private Variant v_SE_CREATE_SYMBOLIC_LINK_NAME = Variant.Create( (object) null );
        private Variant v_SE_CREATE_TOKEN_NAME = Variant.Create( (object) null );
        private Variant v_SE_DEBUG_NAME = Variant.Create( (object) null );
        private Variant v_SE_ENABLE_DELEGATION_NAME = Variant.Create( (object) null );
        private Variant v_SE_GROUP_ENABLED = Variant.Create( (object) null );
        private Variant v_SE_GROUP_ENABLED_BY_DEFAULT = Variant.Create( (object) null );
        private Variant v_SE_GROUP_INTEGRITY = Variant.Create( (object) null );
        private Variant v_SE_GROUP_INTEGRITY_ENABLED = Variant.Create( (object) null );
        private Variant v_SE_GROUP_LOGON_ID = Variant.Create( (object) null );
        private Variant v_SE_GROUP_MANDATORY = Variant.Create( (object) null );
        private Variant v_SE_GROUP_OWNER = Variant.Create( (object) null );
        private Variant v_SE_GROUP_RESOURCE = Variant.Create( (object) null );
        private Variant v_SE_GROUP_USE_FOR_DENY_ONLY = Variant.Create( (object) null );
        private Variant v_SE_IMPERSONATE_NAME = Variant.Create( (object) null );
        private Variant v_SE_INCREASE_QUOTA_NAME = Variant.Create( (object) null );
        private Variant v_SE_INC_BASE_PRIORITY_NAME = Variant.Create( (object) null );
        private Variant v_SE_INC_WORKING_SET_NAME = Variant.Create( (object) null );
        private Variant v_SE_LOAD_DRIVER_NAME = Variant.Create( (object) null );
        private Variant v_SE_LOCK_MEMORY_NAME = Variant.Create( (object) null );
        private Variant v_SE_MACHINE_ACCOUNT_NAME = Variant.Create( (object) null );
        private Variant v_SE_MANAGE_VOLUME_NAME = Variant.Create( (object) null );
        private Variant v_SE_PRIVILEGE_ENABLED = Variant.Create( (object) null );
        private Variant v_SE_PRIVILEGE_ENABLED_BY_DEFAULT = Variant.Create( (object) null );
        private Variant v_SE_PRIVILEGE_REMOVED = Variant.Create( (object) null );
        private Variant v_SE_PRIVILEGE_USED_FOR_ACCESS = Variant.Create( (object) null );
        private Variant v_SE_PROF_SINGLE_PROCESS_NAME = Variant.Create( (object) null );
        private Variant v_SE_RELABEL_NAME = Variant.Create( (object) null );
        private Variant v_SE_REMOTE_SHUTDOWN_NAME = Variant.Create( (object) null );
        private Variant v_SE_RESTORE_NAME = Variant.Create( (object) null );
        private Variant v_SE_SECURITY_NAME = Variant.Create( (object) null );
        private Variant v_SE_SHUTDOWN_NAME = Variant.Create( (object) null );
        private Variant v_SE_SYNC_AGENT_NAME = Variant.Create( (object) null );
        private Variant v_SE_SYSTEMTIME_NAME = Variant.Create( (object) null );
        private Variant v_SE_SYSTEM_ENVIRONMENT_NAME = Variant.Create( (object) null );
        private Variant v_SE_SYSTEM_PROFILE_NAME = Variant.Create( (object) null );
        private Variant v_SE_TAKE_OWNERSHIP_NAME = Variant.Create( (object) null );
        private Variant v_SE_TCB_NAME = Variant.Create( (object) null );
        private Variant v_SE_TIME_ZONE_NAME = Variant.Create( (object) null );
        private Variant v_SE_TRUSTED_CREDMAN_ACCESS_NAME = Variant.Create( (object) null );
        private Variant v_SE_UNDOCK_NAME = Variant.Create( (object) null );
        private Variant v_SE_UNSOLICITED_INPUT_NAME = Variant.Create( (object) null );
        private Variant v_SIDTYPEALIAS = Variant.Create( (object) null );
        private Variant v_SIDTYPECOMPUTER = Variant.Create( (object) null );
        private Variant v_SIDTYPEDELETEDACCOUNT = Variant.Create( (object) null );
        private Variant v_SIDTYPEDOMAIN = Variant.Create( (object) null );
        private Variant v_SIDTYPEGROUP = Variant.Create( (object) null );
        private Variant v_SIDTYPEINVALID = Variant.Create( (object) null );
        private Variant v_SIDTYPELABEL = Variant.Create( (object) null );
        private Variant v_SIDTYPEUNKNOWN = Variant.Create( (object) null );
        private Variant v_SIDTYPEUSER = Variant.Create( (object) null );
        private Variant v_SIDTYPEWELLKNOWNGROUP = Variant.Create( (object) null );
        private Variant v_SID_ACCOUNT_OPERATORS = Variant.Create( (object) null );
        private Variant v_SID_ADMINISTRATORS = Variant.Create( (object) null );
        private Variant v_SID_ALL_SERVICES = Variant.Create( (object) null );
        private Variant v_SID_AUTHENTICATED_USERS = Variant.Create( (object) null );
        private Variant v_SID_BACKUP_OPERATORS = Variant.Create( (object) null );
        private Variant v_SID_DIGEST_AUTHENTICATION = Variant.Create( (object) null );
        private Variant v_SID_EVERYONE = Variant.Create( (object) null );
        private Variant v_SID_GUESTS = Variant.Create( (object) null );
        private Variant v_SID_HIGH_MANDATORY_LEVEL = Variant.Create( (object) null );
        private Variant v_SID_INTERACTIVE = Variant.Create( (object) null );
        private Variant v_SID_LOW_MANDATORY_LEVEL = Variant.Create( (object) null );
        private Variant v_SID_MEDIUM_MANDATORY_LEVEL = Variant.Create( (object) null );
        private Variant v_SID_MEDIUM_PLUS_MANDATORY_LEVEL = Variant.Create( (object) null );
        private Variant v_SID_NETWORK = Variant.Create( (object) null );
        private Variant v_SID_NT_SERVICE = Variant.Create( (object) null );
        private Variant v_SID_OWNER = Variant.Create( (object) null );
        private Variant v_SID_PRINT_OPERATORS = Variant.Create( (object) null );
        private Variant v_SID_PROTECTED_PROCESS_MANDATORY_LEVEL = Variant.Create( (object) null );
        private Variant v_SID_REPLICATOR = Variant.Create( (object) null );
        private Variant v_SID_SCHANNEL_AUTHENTICATION = Variant.Create( (object) null );
        private Variant v_SID_SECURE_PROCESS_MANDATORY_LEVEL = Variant.Create( (object) null );
        private Variant v_SID_SERVER_OPERATORS = Variant.Create( (object) null );
        private Variant v_SID_SYSTEM = Variant.Create( (object) null );
        private Variant v_SID_SYSTEM_MANDATORY_LEVEL = Variant.Create( (object) null );
        private Variant v_SID_UNTRUSTED_MANDATORY_LEVEL = Variant.Create( (object) null );
        private Variant v_SID_USERS = Variant.Create( (object) null );
        private Variant v_SPECIFIC_RIGHTS_ALL = Variant.Create( (object) null );
        private Variant v_STANDARD_RIGHTS_ALL = Variant.Create( (object) null );
        private Variant v_STANDARD_RIGHTS_EXECUTE = Variant.Create( (object) null );
        private Variant v_STANDARD_RIGHTS_READ = Variant.Create( (object) null );
        private Variant v_STANDARD_RIGHTS_REQUIRED = Variant.Create( (object) null );
        private Variant v_STANDARD_RIGHTS_WRITE = Variant.Create( (object) null );
        private Variant v_STDERR_CHILD = Variant.Create( (object) null );
        private Variant v_STDERR_MERGED = Variant.Create( (object) null );
        private Variant v_STDIN_CHILD = Variant.Create( (object) null );
        private Variant v_STDIO_INHERIT_PARENT = Variant.Create( (object) null );
        private Variant v_STDOUT_CHILD = Variant.Create( (object) null );
        private Variant v_STD_COPY = Variant.Create( (object) null );
        private Variant v_STD_CUT = Variant.Create( (object) null );
        private Variant v_STD_DELETE = Variant.Create( (object) null );
        private Variant v_STD_FILENEW = Variant.Create( (object) null );
        private Variant v_STD_FILEOPEN = Variant.Create( (object) null );
        private Variant v_STD_FILESAVE = Variant.Create( (object) null );
        private Variant v_STD_FIND = Variant.Create( (object) null );
        private Variant v_STD_HELP = Variant.Create( (object) null );
        private Variant v_STD_PASTE = Variant.Create( (object) null );
        private Variant v_STD_PRINT = Variant.Create( (object) null );
        private Variant v_STD_PRINTPRE = Variant.Create( (object) null );
        private Variant v_STD_PROPERTIES = Variant.Create( (object) null );
        private Variant v_STD_REDOW = Variant.Create( (object) null );
        private Variant v_STD_REPLACE = Variant.Create( (object) null );
        private Variant v_STD_UNDO = Variant.Create( (object) null );
        private Variant v_STR_CASESENSE = Variant.Create( (object) null );
        private Variant v_STR_CHRSPLIT = Variant.Create( (object) null );
        private Variant v_STR_ENDISSTART = Variant.Create( (object) null );
        private Variant v_STR_ENDNOTSTART = Variant.Create( (object) null );
        private Variant v_STR_ENTIRESPLIT = Variant.Create( (object) null );
        private Variant v_STR_NOCASESENSE = Variant.Create( (object) null );
        private Variant v_STR_NOCASESENSEBASIC = Variant.Create( (object) null );
        private Variant v_STR_NOCOUNT = Variant.Create( (object) null );
        private Variant v_STR_REGEXPARRAYFULLMATCH = Variant.Create( (object) null );
        private Variant v_STR_REGEXPARRAYGLOBALFULLMATCH = Variant.Create( (object) null );
        private Variant v_STR_REGEXPARRAYGLOBALMATCH = Variant.Create( (object) null );
        private Variant v_STR_REGEXPARRAYMATCH = Variant.Create( (object) null );
        private Variant v_STR_REGEXPMATCH = Variant.Create( (object) null );
        private Variant v_STR_STRIPALL = Variant.Create( (object) null );
        private Variant v_STR_STRIPLEADING = Variant.Create( (object) null );
        private Variant v_STR_STRIPSPACES = Variant.Create( (object) null );
        private Variant v_STR_STRIPTRAILING = Variant.Create( (object) null );
        private Variant v_SWP_ASYNCWINDOWPOS = Variant.Create( (object) null );
        private Variant v_SWP_DEFERERASE = Variant.Create( (object) null );
        private Variant v_SWP_DRAWFRAME = Variant.Create( (object) null );
        private Variant v_SWP_FRAMECHANGED = Variant.Create( (object) null );
        private Variant v_SWP_HIDEWINDOW = Variant.Create( (object) null );
        private Variant v_SWP_NOACTIVATE = Variant.Create( (object) null );
        private Variant v_SWP_NOCOPYBITS = Variant.Create( (object) null );
        private Variant v_SWP_NOMOVE = Variant.Create( (object) null );
        private Variant v_SWP_NOOWNERZORDER = Variant.Create( (object) null );
        private Variant v_SWP_NOREDRAW = Variant.Create( (object) null );
        private Variant v_SWP_NOREPOSITION = Variant.Create( (object) null );
        private Variant v_SWP_NOSENDCHANGING = Variant.Create( (object) null );
        private Variant v_SWP_NOSIZE = Variant.Create( (object) null );
        private Variant v_SWP_NOZORDER = Variant.Create( (object) null );
        private Variant v_SWP_SHOWWINDOW = Variant.Create( (object) null );
        private Variant v_SYNCHRONIZE = Variant.Create( (object) null );
        private Variant v_SYSTEM_FIXED_FONT = Variant.Create( (object) null );
        private Variant v_SYSTEM_FONT = Variant.Create( (object) null );
        private Variant v_S_OK = Variant.Create( (object) null );
        private Variant v_TMPF_DEVICE = Variant.Create( (object) null );
        private Variant v_TMPF_FIXED_PITCH = Variant.Create( (object) null );
        private Variant v_TMPF_TRUETYPE = Variant.Create( (object) null );
        private Variant v_TMPF_VECTOR = Variant.Create( (object) null );
        private Variant v_TOKENACCESSINFORMATION = Variant.Create( (object) null );
        private Variant v_TOKENAUDITPOLICY = Variant.Create( (object) null );
        private Variant v_TOKENDEFAULTDACL = Variant.Create( (object) null );
        private Variant v_TOKENELEVATION = Variant.Create( (object) null );
        private Variant v_TOKENELEVATIONTYPE = Variant.Create( (object) null );
        private Variant v_TOKENGROUPS = Variant.Create( (object) null );
        private Variant v_TOKENGROUPSANDPRIVILEGES = Variant.Create( (object) null );
        private Variant v_TOKENHASRESTRICTIONS = Variant.Create( (object) null );
        private Variant v_TOKENIMPERSONATION = Variant.Create( (object) null );
        private Variant v_TOKENIMPERSONATIONLEVEL = Variant.Create( (object) null );
        private Variant v_TOKENINTEGRITYLEVEL = Variant.Create( (object) null );
        private Variant v_TOKENLINKEDTOKEN = Variant.Create( (object) null );
        private Variant v_TOKENLOGONSID = Variant.Create( (object) null );
        private Variant v_TOKENMANDATORYPOLICY = Variant.Create( (object) null );
        private Variant v_TOKENORIGIN = Variant.Create( (object) null );
        private Variant v_TOKENOWNER = Variant.Create( (object) null );
        private Variant v_TOKENPRIMARY = Variant.Create( (object) null );
        private Variant v_TOKENPRIMARYGROUP = Variant.Create( (object) null );
        private Variant v_TOKENPRIVILEGES = Variant.Create( (object) null );
        private Variant v_TOKENRESTRICTEDSIDS = Variant.Create( (object) null );
        private Variant v_TOKENSANDBOXINERT = Variant.Create( (object) null );
        private Variant v_TOKENSESSIONID = Variant.Create( (object) null );
        private Variant v_TOKENSESSIONREFERENCE = Variant.Create( (object) null );
        private Variant v_TOKENSOURCE = Variant.Create( (object) null );
        private Variant v_TOKENSTATISTICS = Variant.Create( (object) null );
        private Variant v_TOKENTYPE = Variant.Create( (object) null );
        private Variant v_TOKENUIACCESS = Variant.Create( (object) null );
        private Variant v_TOKENUSER = Variant.Create( (object) null );
        private Variant v_TOKENVIRTUALIZATIONALLOWED = Variant.Create( (object) null );
        private Variant v_TOKENVIRTUALIZATIONENABLED = Variant.Create( (object) null );
        private Variant v_TOKEN_ADJUST_DEFAULT = Variant.Create( (object) null );
        private Variant v_TOKEN_ADJUST_GROUPS = Variant.Create( (object) null );
        private Variant v_TOKEN_ADJUST_PRIVILEGES = Variant.Create( (object) null );
        private Variant v_TOKEN_ADJUST_SESSIONID = Variant.Create( (object) null );
        private Variant v_TOKEN_ALL_ACCESS = Variant.Create( (object) null );
        private Variant v_TOKEN_ASSIGN_PRIMARY = Variant.Create( (object) null );
        private Variant v_TOKEN_DUPLICATE = Variant.Create( (object) null );
        private Variant v_TOKEN_EXECUTE = Variant.Create( (object) null );
        private Variant v_TOKEN_HAS_ADMIN_GROUP = Variant.Create( (object) null );
        private Variant v_TOKEN_HAS_BACKUP_PRIVILEGE = Variant.Create( (object) null );
        private Variant v_TOKEN_HAS_IMPERSONATE_PRIVILEGE = Variant.Create( (object) null );
        private Variant v_TOKEN_HAS_RESTORE_PRIVILEGE = Variant.Create( (object) null );
        private Variant v_TOKEN_HAS_TRAVERSE_PRIVILEGE = Variant.Create( (object) null );
        private Variant v_TOKEN_IMPERSONATE = Variant.Create( (object) null );
        private Variant v_TOKEN_IS_RESTRICTED = Variant.Create( (object) null );
        private Variant v_TOKEN_QUERY = Variant.Create( (object) null );
        private Variant v_TOKEN_QUERY_SOURCE = Variant.Create( (object) null );
        private Variant v_TOKEN_READ = Variant.Create( (object) null );
        private Variant v_TOKEN_SANDBOX_INERT = Variant.Create( (object) null );
        private Variant v_TOKEN_SESSION_NOT_REFERENCED = Variant.Create( (object) null );
        private Variant v_TOKEN_WRITE = Variant.Create( (object) null );
        private Variant v_TRUNCATE_EXISTING = Variant.Create( (object) null );
        private Variant v_TRUSTEE_IS_ALIAS = Variant.Create( (object) null );
        private Variant v_TRUSTEE_IS_COMPUTER = Variant.Create( (object) null );
        private Variant v_TRUSTEE_IS_DELETED = Variant.Create( (object) null );
        private Variant v_TRUSTEE_IS_DOMAIN = Variant.Create( (object) null );
        private Variant v_TRUSTEE_IS_GROUP = Variant.Create( (object) null );
        private Variant v_TRUSTEE_IS_INVALID = Variant.Create( (object) null );
        private Variant v_TRUSTEE_IS_UNKNOWN = Variant.Create( (object) null );
        private Variant v_TRUSTEE_IS_USER = Variant.Create( (object) null );
        private Variant v_TRUSTEE_IS_WELL_KNOWN_GROUP = Variant.Create( (object) null );
        private Variant v_UBOUND_COLUMNS = Variant.Create( (object) null );
        private Variant v_UBOUND_DIMENSIONS = Variant.Create( (object) null );
        private Variant v_UBOUND_ROWS = Variant.Create( (object) null );
        private Variant v_ULW_ALPHA = Variant.Create( (object) null );
        private Variant v_ULW_COLORKEY = Variant.Create( (object) null );
        private Variant v_ULW_EX_NORESIZE = Variant.Create( (object) null );
        private Variant v_ULW_OPAQUE = Variant.Create( (object) null );
        private Variant v_WHITE_BRUSH = Variant.Create( (object) null );
        private Variant v_WHITE_PEN = Variant.Create( (object) null );
        private Variant v_WH_CALLWNDPROC = Variant.Create( (object) null );
        private Variant v_WH_CALLWNDPROCRET = Variant.Create( (object) null );
        private Variant v_WH_CBT = Variant.Create( (object) null );
        private Variant v_WH_DEBUG = Variant.Create( (object) null );
        private Variant v_WH_FOREGROUNDIDLE = Variant.Create( (object) null );
        private Variant v_WH_GETMESSAGE = Variant.Create( (object) null );
        private Variant v_WH_JOURNALPLAYBACK = Variant.Create( (object) null );
        private Variant v_WH_JOURNALRECORD = Variant.Create( (object) null );
        private Variant v_WH_KEYBOARD = Variant.Create( (object) null );
        private Variant v_WH_KEYBOARD_LL = Variant.Create( (object) null );
        private Variant v_WH_MOUSE = Variant.Create( (object) null );
        private Variant v_WH_MOUSE_LL = Variant.Create( (object) null );
        private Variant v_WH_MSGFILTER = Variant.Create( (object) null );
        private Variant v_WH_SHELL = Variant.Create( (object) null );
        private Variant v_WH_SYSMSGFILTER = Variant.Create( (object) null );
        private Variant v_WPF_ASYNCWINDOWPLACEMENT = Variant.Create( (object) null );
        private Variant v_WPF_RESTORETOMAXIMIZED = Variant.Create( (object) null );
        private Variant v_WPF_SETMINPOSITION = Variant.Create( (object) null );
        private Variant v_WRITE_DAC = Variant.Create( (object) null );
        private Variant v_WRITE_OWNER = Variant.Create( (object) null );
        private Variant v___WINAPICONSTANT_CLIP_DEFAULT_PRECIS = Variant.Create( (object) null );
        private Variant v___WINAPICONSTANT_DEFAULT_CHARSET = Variant.Create( (object) null );
        private Variant v___WINAPICONSTANT_DEFAULT_QUALITY = Variant.Create( (object) null );
        private Variant v___WINAPICONSTANT_FW_NORMAL = Variant.Create( (object) null );
        private Variant v___WINAPICONSTANT_LOGPIXELSX = Variant.Create( (object) null );
        private Variant v___WINAPICONSTANT_LOGPIXELSY = Variant.Create( (object) null );
        private Variant v___WINAPICONSTANT_OUT_DEFAULT_PRECIS = Variant.Create( (object) null );
        private Variant v___WINAPICONSTANT_WM_SETFONT = Variant.Create( (object) null );
        private Variant v___g_aInProcess_WinAPI = Variant.Create( (object) null );
        private Variant v___g_aWinList_WinAPI = Variant.Create( (object) null );
        private Variant v_tagBITMAPINFO = Variant.Create( (object) null );
        private Variant v_tagBITMAPINFOHEADER = Variant.Create( (object) null );
        private Variant v_tagBLENDFUNCTION = Variant.Create( (object) null );
        private Variant v_tagCOLORSCHEME = Variant.Create( (object) null );
        private Variant v_tagCOMBOBOXEXITEM = Variant.Create( (object) null );
        private Variant v_tagCURSORINFO = Variant.Create( (object) null );
        private Variant v_tagDISPLAY_DEVICE = Variant.Create( (object) null );
        private Variant v_tagDTPRANGE = Variant.Create( (object) null );
        private Variant v_tagEVENTLOGRECORD = Variant.Create( (object) null );
        private Variant v_tagFILETIME = Variant.Create( (object) null );
        private Variant v_tagFLASHWINFO = Variant.Create( (object) null );
        private Variant v_tagGDIPBITMAPDATA = Variant.Create( (object) null );
        private Variant v_tagGDIPCOLORMATRIX = Variant.Create( (object) null );
        private Variant v_tagGDIPENCODERPARAM = Variant.Create( (object) null );
        private Variant v_tagGDIPENCODERPARAMS = Variant.Create( (object) null );
        private Variant v_tagGDIPIMAGECODECINFO = Variant.Create( (object) null );
        private Variant v_tagGDIPPENCODERPARAMS = Variant.Create( (object) null );
        private Variant v_tagGDIPRECTF = Variant.Create( (object) null );
        private Variant v_tagGDIPSTARTUPINPUT = Variant.Create( (object) null );
        private Variant v_tagGDIPSTARTUPOUTPUT = Variant.Create( (object) null );
        private Variant v_tagGDIP_EFFECTPARAMS_Blur = Variant.Create( (object) null );
        private Variant v_tagGDIP_EFFECTPARAMS_BrightnessContrast = Variant.Create( (object) null );
        private Variant v_tagGDIP_EFFECTPARAMS_ColorBalance = Variant.Create( (object) null );
        private Variant v_tagGDIP_EFFECTPARAMS_ColorCurve = Variant.Create( (object) null );
        private Variant v_tagGDIP_EFFECTPARAMS_ColorLUT = Variant.Create( (object) null );
        private Variant v_tagGDIP_EFFECTPARAMS_HueSaturationLightness = Variant.Create( (object) null );
        private Variant v_tagGDIP_EFFECTPARAMS_Levels = Variant.Create( (object) null );
        private Variant v_tagGDIP_EFFECTPARAMS_RedEyeCorrection = Variant.Create( (object) null );
        private Variant v_tagGDIP_EFFECTPARAMS_Sharpen = Variant.Create( (object) null );
        private Variant v_tagGDIP_EFFECTPARAMS_Tint = Variant.Create( (object) null );
        private Variant v_tagGETIPAddress = Variant.Create( (object) null );
        private Variant v_tagGUID = Variant.Create( (object) null );
        private Variant v_tagHDITEM = Variant.Create( (object) null );
        private Variant v_tagICONINFO = Variant.Create( (object) null );
        private Variant v_tagIMAGEINFO = Variant.Create( (object) null );
        private Variant v_tagKBDLLHOOKSTRUCT = Variant.Create( (object) null );
        private Variant v_tagLOGFONT = Variant.Create( (object) null );
        private Variant v_tagLVFINDINFO = Variant.Create( (object) null );
        private Variant v_tagLVHITTESTINFO = Variant.Create( (object) null );
        private Variant v_tagLVITEM = Variant.Create( (object) null );
        private Variant v_tagMARGINS = Variant.Create( (object) null );
        private Variant v_tagMCHITTESTINFO = Variant.Create( (object) null );
        private Variant v_tagMCMONTHRANGE = Variant.Create( (object) null );
        private Variant v_tagMCRANGE = Variant.Create( (object) null );
        private Variant v_tagMCSELRANGE = Variant.Create( (object) null );
        private Variant v_tagMEMORYSTATUSEX = Variant.Create( (object) null );
        private Variant v_tagMENUINFO = Variant.Create( (object) null );
        private Variant v_tagMENUITEMINFO = Variant.Create( (object) null );
        private Variant v_tagNETRESOURCE = Variant.Create( (object) null );
        private Variant v_tagNMCBEDRAGBEGIN = Variant.Create( (object) null );
        private Variant v_tagNMCBEENDEDIT = Variant.Create( (object) null );
        private Variant v_tagNMCOMBOBOXEX = Variant.Create( (object) null );
        private Variant v_tagNMDATETIMECHANGE = Variant.Create( (object) null );
        private Variant v_tagNMDATETIMEFORMAT = Variant.Create( (object) null );
        private Variant v_tagNMDATETIMEFORMATQUERY = Variant.Create( (object) null );
        private Variant v_tagNMDATETIMEKEYDOWN = Variant.Create( (object) null );
        private Variant v_tagNMDATETIMESTRING = Variant.Create( (object) null );
        private Variant v_tagNMDAYSTATE = Variant.Create( (object) null );
        private Variant v_tagNMHDDISPINFO = Variant.Create( (object) null );
        private Variant v_tagNMHDFILTERBTNCLICK = Variant.Create( (object) null );
        private Variant v_tagNMHDR = Variant.Create( (object) null );
        private Variant v_tagNMHEADER = Variant.Create( (object) null );
        private Variant v_tagNMIPADDRESS = Variant.Create( (object) null );
        private Variant v_tagNMITEMACTIVATE = Variant.Create( (object) null );
        private Variant v_tagNMLISTVIEW = Variant.Create( (object) null );
        private Variant v_tagNMLVCUSTOMDRAW = Variant.Create( (object) null );
        private Variant v_tagNMLVDISPINFO = Variant.Create( (object) null );
        private Variant v_tagNMLVFINDITEM = Variant.Create( (object) null );
        private Variant v_tagNMLVGETINFOTIP = Variant.Create( (object) null );
        private Variant v_tagNMLVKEYDOWN = Variant.Create( (object) null );
        private Variant v_tagNMLVSCROLL = Variant.Create( (object) null );
        private Variant v_tagNMMOUSE = Variant.Create( (object) null );
        private Variant v_tagNMOBJECTNOTIFY = Variant.Create( (object) null );
        private Variant v_tagNMRBAUTOSIZE = Variant.Create( (object) null );
        private Variant v_tagNMREBAR = Variant.Create( (object) null );
        private Variant v_tagNMREBARAUTOBREAK = Variant.Create( (object) null );
        private Variant v_tagNMREBARCHEVRON = Variant.Create( (object) null );
        private Variant v_tagNMREBARCHILDSIZE = Variant.Create( (object) null );
        private Variant v_tagNMSELCHANGE = Variant.Create( (object) null );
        private Variant v_tagNMTBHOTITEM = Variant.Create( (object) null );
        private Variant v_tagNMTCKEYDOWN = Variant.Create( (object) null );
        private Variant v_tagNMTOOLBAR = Variant.Create( (object) null );
        private Variant v_tagNMTREEVIEW = Variant.Create( (object) null );
        private Variant v_tagNMTVCUSTOMDRAW = Variant.Create( (object) null );
        private Variant v_tagNMTVDISPINFO = Variant.Create( (object) null );
        private Variant v_tagNMTVGETINFOTIP = Variant.Create( (object) null );
        private Variant v_tagNMTVITEMCHANGE = Variant.Create( (object) null );
        private Variant v_tagNMTVKEYDOWN = Variant.Create( (object) null );
        private Variant v_tagOPENFILENAME = Variant.Create( (object) null );
        private Variant v_tagOVERLAPPED = Variant.Create( (object) null );
        private Variant v_tagPOINT = Variant.Create( (object) null );
        private Variant v_tagPROCESS_INFORMATION = Variant.Create( (object) null );
        private Variant v_tagREBARBANDINFO = Variant.Create( (object) null );
        private Variant v_tagRECT = Variant.Create( (object) null );
        private Variant v_tagSCROLLBARINFO = Variant.Create( (object) null );
        private Variant v_tagSCROLLINFO = Variant.Create( (object) null );
        private Variant v_tagSECURITY_ATTRIBUTES = Variant.Create( (object) null );
        private Variant v_tagSIZE = Variant.Create( (object) null );
        private Variant v_tagSTARTUPINFO = Variant.Create( (object) null );
        private Variant v_tagSYSTEMTIME = Variant.Create( (object) null );
        private Variant v_tagTBBUTTON = Variant.Create( (object) null );
        private Variant v_tagTBBUTTONINFO = Variant.Create( (object) null );
        private Variant v_tagTEXTMETRIC = Variant.Create( (object) null );
        private Variant v_tagTIME_ZONE_INFORMATION = Variant.Create( (object) null );
        private Variant v_tagTOKEN_PRIVILEGES = Variant.Create( (object) null );
        private Variant v_tagTVHITTESTINFO = Variant.Create( (object) null );
        private Variant v_tagTVITEM = Variant.Create( (object) null );
        private Variant v_tagTVITEMEX = Variant.Create( (object) null );
        private Variant v_tagWIN32_FIND_DATA = Variant.Create( (object) null );
        private Variant v_tagWINDOWPLACEMENT = Variant.Create( (object) null );
        private Variant v_tagWINDOWPOS = Variant.Create( (object) null );

        public AutoJITScriptClass() {
            _context = new AutoitContext<AutoJITScriptClass>( this );
            _functions = new AutoitRuntime<AutoJITScriptClass>( _context );
            Main();
        }

        public Variant Main() {
            v_HGDI_ERROR = _functions.Ptr( Variant.Create( -1 ) );
            v_INVALID_HANDLE_VALUE = _functions.Ptr( Variant.Create( -1 ) );
            v_CLR_INVALID = Variant.Create( -1 );
            v_NULL_BRUSH = Variant.Create( 5 );
            v_NULL_PEN = Variant.Create( 8 );
            v_BLACK_BRUSH = Variant.Create( 4 );
            v_DKGRAY_BRUSH = Variant.Create( 3 );
            v_DC_BRUSH = Variant.Create( 18 );
            v_GRAY_BRUSH = Variant.Create( 2 );
            v_HOLLOW_BRUSH = v_NULL_BRUSH;
            v_LTGRAY_BRUSH = Variant.Create( 1 );
            v_WHITE_BRUSH = Variant.Create( 0 );
            v_BLACK_PEN = Variant.Create( 7 );
            v_DC_PEN = Variant.Create( 19 );
            v_WHITE_PEN = Variant.Create( 6 );
            v_ANSI_FIXED_FONT = Variant.Create( 11 );
            v_ANSI_VAR_FONT = Variant.Create( 12 );
            v_DEVICE_DEFAULT_FONT = Variant.Create( 14 );
            v_DEFAULT_GUI_FONT = Variant.Create( 17 );
            v_OEM_FIXED_FONT = Variant.Create( 10 );
            v_SYSTEM_FONT = Variant.Create( 13 );
            v_SYSTEM_FIXED_FONT = Variant.Create( 16 );
            v_DEFAULT_PALETTE = Variant.Create( 15 );
            v_MB_PRECOMPOSED = Variant.Create( 1 );
            v_MB_COMPOSITE = Variant.Create( 2 );
            v_MB_USEGLYPHCHARS = Variant.Create( 4 );
            v_ULW_ALPHA = Variant.Create( 2 );
            v_ULW_COLORKEY = Variant.Create( 1 );
            v_ULW_OPAQUE = Variant.Create( 4 );
            v_ULW_EX_NORESIZE = Variant.Create( 8 );
            v_WH_CALLWNDPROC = Variant.Create( 4 );
            v_WH_CALLWNDPROCRET = Variant.Create( 12 );
            v_WH_CBT = Variant.Create( 5 );
            v_WH_DEBUG = Variant.Create( 9 );
            v_WH_FOREGROUNDIDLE = Variant.Create( 11 );
            v_WH_GETMESSAGE = Variant.Create( 3 );
            v_WH_JOURNALPLAYBACK = Variant.Create( 1 );
            v_WH_JOURNALRECORD = Variant.Create( 0 );
            v_WH_KEYBOARD = Variant.Create( 2 );
            v_WH_KEYBOARD_LL = Variant.Create( 13 );
            v_WH_MOUSE = Variant.Create( 7 );
            v_WH_MOUSE_LL = Variant.Create( 14 );
            v_WH_MSGFILTER = Variant.Create( -1 );
            v_WH_SHELL = Variant.Create( 10 );
            v_WH_SYSMSGFILTER = Variant.Create( 6 );
            v_WPF_ASYNCWINDOWPLACEMENT = Variant.Create( 4 );
            v_WPF_RESTORETOMAXIMIZED = Variant.Create( 2 );
            v_WPF_SETMINPOSITION = Variant.Create( 1 );
            v_KF_EXTENDED = Variant.Create( 256 );
            v_KF_ALTDOWN = Variant.Create( 8192 );
            v_KF_UP = Variant.Create( 32768 );
            v_LLKHF_EXTENDED = _functions.BitShift( v_KF_EXTENDED, Variant.Create( 8 ) );
            v_LLKHF_INJECTED = Variant.Create( 16 );
            v_LLKHF_ALTDOWN = _functions.BitShift( v_KF_ALTDOWN, Variant.Create( 8 ) );
            v_LLKHF_UP = _functions.BitShift( v_KF_UP, Variant.Create( 8 ) );
            v_OFN_ALLOWMULTISELECT = Variant.Create( 512 );
            v_OFN_CREATEPROMPT = Variant.Create( 8192 );
            v_OFN_DONTADDTORECENT = Variant.Create( 33554432 );
            v_OFN_ENABLEHOOK = Variant.Create( 32 );
            v_OFN_ENABLEINCLUDENOTIFY = Variant.Create( 4194304 );
            v_OFN_ENABLESIZING = Variant.Create( 8388608 );
            v_OFN_ENABLETEMPLATE = Variant.Create( 64 );
            v_OFN_ENABLETEMPLATEHANDLE = Variant.Create( 128 );
            v_OFN_EXPLORER = Variant.Create( 524288 );
            v_OFN_EXTENSIONDIFFERENT = Variant.Create( 1024 );
            v_OFN_FILEMUSTEXIST = Variant.Create( 4096 );
            v_OFN_FORCESHOWHIDDEN = Variant.Create( 268435456 );
            v_OFN_HIDEREADONLY = Variant.Create( 4 );
            v_OFN_LONGNAMES = Variant.Create( 2097152 );
            v_OFN_NOCHANGEDIR = Variant.Create( 8 );
            v_OFN_NODEREFERENCELINKS = Variant.Create( 1048576 );
            v_OFN_NOLONGNAMES = Variant.Create( 262144 );
            v_OFN_NONETWORKBUTTON = Variant.Create( 131072 );
            v_OFN_NOREADONLYRETURN = Variant.Create( 32768 );
            v_OFN_NOTESTFILECREATE = Variant.Create( 65536 );
            v_OFN_NOVALIDATE = Variant.Create( 256 );
            v_OFN_OVERWRITEPROMPT = Variant.Create( 2 );
            v_OFN_PATHMUSTEXIST = Variant.Create( 2048 );
            v_OFN_READONLY = Variant.Create( 1 );
            v_OFN_SHAREAWARE = Variant.Create( 16384 );
            v_OFN_SHOWHELP = Variant.Create( 16 );
            v_OFN_EX_NOPLACESBAR = Variant.Create( 1 );
            v_TMPF_FIXED_PITCH = Variant.Create( 1 );
            v_TMPF_VECTOR = Variant.Create( 2 );
            v_TMPF_TRUETYPE = Variant.Create( 4 );
            v_TMPF_DEVICE = Variant.Create( 8 );
            v_DUPLICATE_CLOSE_SOURCE = Variant.Create( 1 );
            v_DUPLICATE_SAME_ACCESS = Variant.Create( 2 );
            v_DI_MASK = Variant.Create( 1 );
            v_DI_IMAGE = Variant.Create( 2 );
            v_DI_NORMAL = Variant.Create( 3 );
            v_DI_COMPAT = Variant.Create( 4 );
            v_DI_DEFAULTSIZE = Variant.Create( 8 );
            v_DI_NOMIRROR = Variant.Create( 16 );
            v_DISPLAY_DEVICE_ATTACHED_TO_DESKTOP = Variant.Create( 1 );
            v_DISPLAY_DEVICE_MULTI_DRIVER = Variant.Create( 2 );
            v_DISPLAY_DEVICE_PRIMARY_DEVICE = Variant.Create( 4 );
            v_DISPLAY_DEVICE_MIRRORING_DRIVER = Variant.Create( 8 );
            v_DISPLAY_DEVICE_VGA_COMPATIBLE = Variant.Create( 16 );
            v_DISPLAY_DEVICE_REMOVABLE = Variant.Create( 32 );
            v_DISPLAY_DEVICE_DISCONNECT = Variant.Create( 33554432 );
            v_DISPLAY_DEVICE_REMOTE = Variant.Create( 67108864 );
            v_DISPLAY_DEVICE_MODESPRUNED = Variant.Create( 134217728 );
            v_FLASHW_CAPTION = Variant.Create( 1 );
            v_FLASHW_TRAY = Variant.Create( 2 );
            v_FLASHW_TIMER = Variant.Create( 4 );
            v_FLASHW_TIMERNOFG = Variant.Create( 12 );
            v_FORMAT_MESSAGE_ALLOCATE_BUFFER = Variant.Create( 256 );
            v_FORMAT_MESSAGE_IGNORE_INSERTS = Variant.Create( 512 );
            v_FORMAT_MESSAGE_FROM_STRING = Variant.Create( 1024 );
            v_FORMAT_MESSAGE_FROM_HMODULE = Variant.Create( 2048 );
            v_FORMAT_MESSAGE_FROM_SYSTEM = Variant.Create( 4096 );
            v_FORMAT_MESSAGE_ARGUMENT_ARRAY = Variant.Create( 8192 );
            v_GW_HWNDFIRST = Variant.Create( 0 );
            v_GW_HWNDLAST = Variant.Create( 1 );
            v_GW_HWNDNEXT = Variant.Create( 2 );
            v_GW_HWNDPREV = Variant.Create( 3 );
            v_GW_OWNER = Variant.Create( 4 );
            v_GW_CHILD = Variant.Create( 5 );
            v_GW_ENABLEDPOPUP = Variant.Create( 6 );
            v_GWL_WNDPROC = Variant.Create( -4 );
            v_GWL_HINSTANCE = Variant.Create( -6 );
            v_GWL_HWNDPARENT = Variant.Create( -8 );
            v_GWL_ID = Variant.Create( -12 );
            v_GWL_STYLE = Variant.Create( -16 );
            v_GWL_EXSTYLE = Variant.Create( -20 );
            v_GWL_USERDATA = Variant.Create( -21 );
            v_STD_CUT = Variant.Create( 0 );
            v_STD_COPY = Variant.Create( 1 );
            v_STD_PASTE = Variant.Create( 2 );
            v_STD_UNDO = Variant.Create( 3 );
            v_STD_REDOW = Variant.Create( 4 );
            v_STD_DELETE = Variant.Create( 5 );
            v_STD_FILENEW = Variant.Create( 6 );
            v_STD_FILEOPEN = Variant.Create( 7 );
            v_STD_FILESAVE = Variant.Create( 8 );
            v_STD_PRINTPRE = Variant.Create( 9 );
            v_STD_PROPERTIES = Variant.Create( 10 );
            v_STD_HELP = Variant.Create( 11 );
            v_STD_FIND = Variant.Create( 12 );
            v_STD_REPLACE = Variant.Create( 13 );
            v_STD_PRINT = Variant.Create( 14 );
            v_IMAGE_BITMAP = Variant.Create( 0 );
            v_IMAGE_ICON = Variant.Create( 1 );
            v_IMAGE_CURSOR = Variant.Create( 2 );
            v_IMAGE_ENHMETAFILE = Variant.Create( 3 );
            v_KB_SENDSPECIAL = Variant.Create( 0 );
            v_KB_SENDRAW = Variant.Create( 1 );
            v_KB_CAPSOFF = Variant.Create( 0 );
            v_KB_CAPSON = Variant.Create( 1 );
            v_DONT_RESOLVE_DLL_REFERENCES = Variant.Create( 1 );
            v_LOAD_LIBRARY_AS_DATAFILE = Variant.Create( 2 );
            v_LOAD_WITH_ALTERED_SEARCH_PATH = Variant.Create( 8 );
            v_LOAD_IGNORE_CODE_AUTHZ_LEVEL = Variant.Create( 16 );
            v_LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE = Variant.Create( 64 );
            v_LOAD_LIBRARY_AS_IMAGE_RESOURCE = Variant.Create( 32 );
            v_LOAD_LIBRARY_SEARCH_APPLICATION_DIR = Variant.Create( 512 );
            v_LOAD_LIBRARY_SEARCH_DEFAULT_DIRS = Variant.Create( 4096 );
            v_LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR = Variant.Create( 256 );
            v_LOAD_LIBRARY_SEARCH_SYSTEM32 = Variant.Create( 2048 );
            v_LOAD_LIBRARY_SEARCH_USER_DIRS = Variant.Create( 1024 );
            v_S_OK = Variant.Create( 0 );
            v_E_ABORT = Variant.Create( -2147467260 );
            v_E_ACCESSDENIED = Variant.Create( -2147024891 );
            v_E_FAIL = Variant.Create( -2147467259 );
            v_E_HANDLE = Variant.Create( -2147024890 );
            v_E_INVALIDARG = Variant.Create( -2147024809 );
            v_E_NOINTERFACE = Variant.Create( -2147467262 );
            v_E_NOTIMPL = Variant.Create( -2147467263 );
            v_E_OUTOFMEMORY = Variant.Create( -2147024882 );
            v_E_POINTER = Variant.Create( -2147467261 );
            v_E_UNEXPECTED = Variant.Create( -2147418113 );
            v_LR_DEFAULTCOLOR = Variant.Create( 0 );
            v_LR_MONOCHROME = Variant.Create( 1 );
            v_LR_COLOR = Variant.Create( 2 );
            v_LR_COPYRETURNORG = Variant.Create( 4 );
            v_LR_COPYDELETEORG = Variant.Create( 8 );
            v_LR_LOADFROMFILE = Variant.Create( 16 );
            v_LR_LOADTRANSPARENT = Variant.Create( 32 );
            v_LR_DEFAULTSIZE = Variant.Create( 64 );
            v_LR_VGACOLOR = Variant.Create( 128 );
            v_LR_LOADMAP3DCOLORS = Variant.Create( 4096 );
            v_LR_CREATEDIBSECTION = Variant.Create( 8192 );
            v_LR_COPYFROMRESOURCE = Variant.Create( 16384 );
            v_LR_SHARED = Variant.Create( 32768 );
            v_OBM_TRTYPE = Variant.Create( 32732 );
            v_OBM_LFARROWI = Variant.Create( 32734 );
            v_OBM_RGARROWI = Variant.Create( 32735 );
            v_OBM_DNARROWI = Variant.Create( 32736 );
            v_OBM_UPARROWI = Variant.Create( 32737 );
            v_OBM_COMBO = Variant.Create( 32738 );
            v_OBM_MNARROW = Variant.Create( 32739 );
            v_OBM_LFARROWD = Variant.Create( 32740 );
            v_OBM_RGARROWD = Variant.Create( 32741 );
            v_OBM_DNARROWD = Variant.Create( 32742 );
            v_OBM_UPARROWD = Variant.Create( 32743 );
            v_OBM_RESTORED = Variant.Create( 32744 );
            v_OBM_ZOOMD = Variant.Create( 32745 );
            v_OBM_REDUCED = Variant.Create( 32746 );
            v_OBM_RESTORE = Variant.Create( 32747 );
            v_OBM_ZOOM = Variant.Create( 32748 );
            v_OBM_REDUCE = Variant.Create( 32749 );
            v_OBM_LFARROW = Variant.Create( 32750 );
            v_OBM_RGARROW = Variant.Create( 32751 );
            v_OBM_DNARROW = Variant.Create( 32752 );
            v_OBM_UPARROW = Variant.Create( 32753 );
            v_OBM_CLOSE = Variant.Create( 32754 );
            v_OBM_OLD_RESTORE = Variant.Create( 32755 );
            v_OBM_OLD_ZOOM = Variant.Create( 32756 );
            v_OBM_OLD_REDUCE = Variant.Create( 32757 );
            v_OBM_BTNCORNERS = Variant.Create( 32758 );
            v_OBM_CHECKBOXES = Variant.Create( 32759 );
            v_OBM_CHECK = Variant.Create( 32760 );
            v_OBM_BTSIZE = Variant.Create( 32761 );
            v_OBM_OLD_LFARROW = Variant.Create( 32762 );
            v_OBM_OLD_RGARROW = Variant.Create( 32763 );
            v_OBM_OLD_DNARROW = Variant.Create( 32764 );
            v_OBM_OLD_UPARROW = Variant.Create( 32765 );
            v_OBM_SIZE = Variant.Create( 32766 );
            v_OBM_OLD_CLOSE = Variant.Create( 32767 );
            v_OIC_SAMPLE = Variant.Create( 32512 );
            v_OIC_HAND = Variant.Create( 32513 );
            v_OIC_QUES = Variant.Create( 32514 );
            v_OIC_BANG = Variant.Create( 32515 );
            v_OIC_NOTE = Variant.Create( 32516 );
            v_OIC_WINLOGO = Variant.Create( 32517 );
            v_OIC_WARNING = v_OIC_BANG;
            v_OIC_ERROR = v_OIC_HAND;
            v_OIC_INFORMATION = v_OIC_NOTE;
            v_tagPOINT = Variant.Create( "struct;long X;long Y;endstruct" );
            v_tagRECT = Variant.Create( "struct;long Left;long Top;long Right;long Bottom;endstruct" );
            v_tagSIZE = Variant.Create( "struct;long X;long Y;endstruct" );
            v_tagMARGINS = Variant.Create( "int cxLeftWidth;int cxRightWidth;int cyTopHeight;int cyBottomHeight" );
            v_tagFILETIME = Variant.Create( "struct;dword Lo;dword Hi;endstruct" );
            v_tagSYSTEMTIME = Variant.Create( "struct;word Year;word Month;word Dow;word Day;word Hour;word Minute;word Second;word MSeconds;endstruct" );
            v_tagTIME_ZONE_INFORMATION =
                Variant.Create( "struct;long Bias;wchar StdName[32];word StdDate[8];long StdBias;wchar DayName[32];word DayDate[8];long DayBias;endstruct" );
            v_tagNMHDR = Variant.Create( "struct;hwnd hWndFrom;uint_ptr IDFrom;INT Code;endstruct" );
            v_tagCOMBOBOXEXITEM =
                _functions.Concat(
                    Variant.Create( "uint Mask;int_ptr Item;ptr Text;int TextMax;int Image;int SelectedImage;int OverlayImage;" ),
                    Variant.Create( "int Indent;lparam Param" ) );
            v_tagNMCBEDRAGBEGIN = _functions.Concat( v_tagNMHDR, Variant.Create( ";int ItemID;wchar szText[260]" ) );
            v_tagNMCBEENDEDIT = _functions.Concat( v_tagNMHDR, Variant.Create( ";bool fChanged;int NewSelection;wchar szText[260];int Why" ) );
            v_tagNMCOMBOBOXEX = _functions.Concat(
                _functions.Concat( v_tagNMHDR, Variant.Create( ";uint Mask;int_ptr Item;ptr Text;int TextMax;int Image;" ) ),
                Variant.Create( "int SelectedImage;int OverlayImage;int Indent;lparam Param" ) );
            v_tagDTPRANGE =
                _functions.Concat(
                    _functions.Concat(
                        Variant.Create( "word MinYear;word MinMonth;word MinDOW;word MinDay;word MinHour;word MinMinute;" ),
                        Variant.Create( "word MinSecond;word MinMSecond;word MaxYear;word MaxMonth;word MaxDOW;word MaxDay;word MaxHour;" ) ),
                    Variant.Create( "word MaxMinute;word MaxSecond;word MaxMSecond;bool MinValid;bool MaxValid" ) );
            v_tagNMDATETIMECHANGE = _functions.Concat( _functions.Concat( v_tagNMHDR, Variant.Create( ";dword Flag;" ) ), v_tagSYSTEMTIME );
            v_tagNMDATETIMEFORMAT = _functions.Concat(
                _functions.Concat( _functions.Concat( v_tagNMHDR, Variant.Create( ";ptr Format;" ) ), v_tagSYSTEMTIME ),
                Variant.Create( ";ptr pDisplay;wchar Display[64]" ) );
            v_tagNMDATETIMEFORMATQUERY = _functions.Concat( v_tagNMHDR, Variant.Create( ";ptr Format;struct;long SizeX;long SizeY;endstruct" ) );
            v_tagNMDATETIMEKEYDOWN = _functions.Concat( _functions.Concat( v_tagNMHDR, Variant.Create( ";int VirtKey;ptr Format;" ) ), v_tagSYSTEMTIME );
            v_tagNMDATETIMESTRING =
                _functions.Concat(
                    _functions.Concat( _functions.Concat( v_tagNMHDR, Variant.Create( ";ptr UserString;" ) ), v_tagSYSTEMTIME ),
                    Variant.Create( ";dword Flags" ) );
            v_tagEVENTLOGRECORD =
                _functions.Concat(
                    _functions.Concat(
                        Variant.Create( "dword Length;dword Reserved;dword RecordNumber;dword TimeGenerated;dword TimeWritten;dword EventID;" ),
                        Variant.Create( "word EventType;word NumStrings;word EventCategory;word ReservedFlags;dword ClosingRecordNumber;dword StringOffset;" ) ),
                    Variant.Create( "dword UserSidLength;dword UserSidOffset;dword DataLength;dword DataOffset" ) );
            v_tagGDIP_EFFECTPARAMS_Blur = Variant.Create( "float Radius; bool ExpandEdge" );
            v_tagGDIP_EFFECTPARAMS_BrightnessContrast = Variant.Create( "int BrightnessLevel; int ContrastLevel" );
            v_tagGDIP_EFFECTPARAMS_ColorBalance = Variant.Create( "int CyanRed; int MagentaGreen; int YellowBlue" );
            v_tagGDIP_EFFECTPARAMS_ColorCurve = Variant.Create( "int Adjustment; int Channel; int AdjustValue" );
            v_tagGDIP_EFFECTPARAMS_ColorLUT = Variant.Create( "byte LutB[256]; byte LutG[256]; byte LutR[256]; byte LutA[256]" );
            v_tagGDIP_EFFECTPARAMS_HueSaturationLightness = Variant.Create( "int HueLevel; int SaturationLevel; int LightnessLevel" );
            v_tagGDIP_EFFECTPARAMS_Levels = Variant.Create( "int Highlight; int Midtone; int Shadow" );
            v_tagGDIP_EFFECTPARAMS_RedEyeCorrection = Variant.Create( "uint NumberOfAreas; ptr Areas" );
            v_tagGDIP_EFFECTPARAMS_Sharpen = Variant.Create( "float Radius; float Amount" );
            v_tagGDIP_EFFECTPARAMS_Tint = Variant.Create( "int Hue; int Amount" );
            v_tagGDIPBITMAPDATA = Variant.Create( "uint Width;uint Height;int Stride;int Format;ptr Scan0;uint_ptr Reserved" );
            v_tagGDIPCOLORMATRIX = Variant.Create( "float m[25]" );
            v_tagGDIPENCODERPARAM = Variant.Create( "struct;byte GUID[16];ulong NumberOfValues;ulong Type;ptr Values;endstruct" );
            v_tagGDIPENCODERPARAMS = _functions.Concat( Variant.Create( "uint Count;" ), v_tagGDIPENCODERPARAM );
            v_tagGDIPRECTF = Variant.Create( "struct;float X;float Y;float Width;float Height;endstruct" );
            v_tagGDIPSTARTUPINPUT = Variant.Create( "uint Version;ptr Callback;bool NoThread;bool NoCodecs" );
            v_tagGDIPSTARTUPOUTPUT = Variant.Create( "ptr HookProc;ptr UnhookProc" );
            v_tagGDIPIMAGECODECINFO =
                _functions.Concat(
                    Variant.Create( "byte CLSID[16];byte FormatID[16];ptr CodecName;ptr DllName;ptr FormatDesc;ptr FileExt;" ),
                    Variant.Create( "ptr MimeType;dword Flags;dword Version;dword SigCount;dword SigSize;ptr SigPattern;ptr SigMask" ) );
            v_tagGDIPPENCODERPARAMS = Variant.Create( "uint Count;byte Params[1]" );
            v_tagHDITEM =
                Variant.Create( "uint Mask;int XY;ptr Text;handle hBMP;int TextMax;int Fmt;lparam Param;int Image;int Order;uint Type;ptr pFilter;uint State" );
            v_tagNMHDDISPINFO = _functions.Concat( v_tagNMHDR, Variant.Create( ";int Item;uint Mask;ptr Text;int TextMax;int Image;lparam lParam" ) );
            v_tagNMHDFILTERBTNCLICK = _functions.Concat( _functions.Concat( v_tagNMHDR, Variant.Create( ";int Item;" ) ), v_tagRECT );
            v_tagNMHEADER = _functions.Concat( v_tagNMHDR, Variant.Create( ";int Item;int Button;ptr pItem" ) );
            v_tagGETIPAddress = Variant.Create( "byte Field4;byte Field3;byte Field2;byte Field1" );
            v_tagNMIPADDRESS = _functions.Concat( v_tagNMHDR, Variant.Create( ";int Field;int Value" ) );
            v_tagLVFINDINFO = _functions.Concat(
                _functions.Concat( Variant.Create( "struct;uint Flags;ptr Text;lparam Param;" ), v_tagPOINT ), Variant.Create( ";uint Direction;endstruct" ) );
            v_tagLVHITTESTINFO = _functions.Concat( v_tagPOINT, Variant.Create( ";uint Flags;int Item;int SubItem;int iGroup" ) );
            v_tagLVITEM =
                _functions.Concat(
                    Variant.Create( "struct;uint Mask;int Item;int SubItem;uint State;uint StateMask;ptr Text;int TextMax;int Image;lparam Param;" ),
                    Variant.Create( "int Indent;int GroupID;uint Columns;ptr pColumns;ptr piColFmt;int iGroup;endstruct" ) );
            v_tagNMLISTVIEW =
                _functions.Concat(
                    _functions.Concat( v_tagNMHDR, Variant.Create( ";int Item;int SubItem;uint NewState;uint OldState;uint Changed;" ) ),
                    Variant.Create( "struct;long ActionX;long ActionY;endstruct;lparam Param" ) );
            v_tagNMLVCUSTOMDRAW =
                _functions.Concat(
                    _functions.Concat(
                        _functions.Concat(
                            _functions.Concat(
                                _functions.Concat(
                                    _functions.Concat( Variant.Create( "struct;" ), v_tagNMHDR ), Variant.Create( ";dword dwDrawStage;handle hdc;" ) ),
                                v_tagRECT ), Variant.Create( ";dword_ptr dwItemSpec;uint uItemState;lparam lItemlParam;endstruct" ) ),
                        Variant.Create( ";dword clrText;dword clrTextBk;int iSubItem;dword dwItemType;dword clrFace;int iIconEffect;" ) ),
                    Variant.Create(
                        "int iIconPhase;int iPartId;int iStateId;struct;long TextLeft;long TextTop;long TextRight;long TextBottom;endstruct;uint uAlign" ) );
            v_tagNMLVDISPINFO = _functions.Concat( _functions.Concat( v_tagNMHDR, Variant.Create( ";" ) ), v_tagLVITEM );
            v_tagNMLVFINDITEM = _functions.Concat( _functions.Concat( v_tagNMHDR, Variant.Create( ";int Start;" ) ), v_tagLVFINDINFO );
            v_tagNMLVGETINFOTIP = _functions.Concat( v_tagNMHDR, Variant.Create( ";dword Flags;ptr Text;int TextMax;int Item;int SubItem;lparam lParam" ) );
            v_tagNMITEMACTIVATE =
                _functions.Concat(
                    _functions.Concat(
                        _functions.Concat( v_tagNMHDR, Variant.Create( ";int Index;int SubItem;uint NewState;uint OldState;uint Changed;" ) ), v_tagPOINT ),
                    Variant.Create( ";lparam lParam;uint KeyFlags" ) );
            v_tagNMLVKEYDOWN = _functions.Concat( _functions.Concat( Variant.Create( "align 1;" ), v_tagNMHDR ), Variant.Create( ";word VKey;uint Flags" ) );
            v_tagNMLVSCROLL = _functions.Concat( v_tagNMHDR, Variant.Create( ";int DX;int DY" ) );
            v_tagMCHITTESTINFO =
                _functions.Concat(
                    _functions.Concat(
                        _functions.Concat(
                            _functions.Concat(
                                _functions.Concat( _functions.Concat( Variant.Create( "uint Size;" ), v_tagPOINT ), Variant.Create( ";uint Hit;" ) ),
                                v_tagSYSTEMTIME ), Variant.Create( ";" ) ), v_tagRECT ), Variant.Create( ";int iOffset;int iRow;int iCol" ) );
            v_tagMCMONTHRANGE =
                _functions.Concat(
                    _functions.Concat(
                        Variant.Create( "word MinYear;word MinMonth;word MinDOW;word MinDay;word MinHour;word MinMinute;word MinSecond;" ),
                        Variant.Create( "word MinMSeconds;word MaxYear;word MaxMonth;word MaxDOW;word MaxDay;word MaxHour;word MaxMinute;word MaxSecond;" ) ),
                    Variant.Create( "word MaxMSeconds;short Span" ) );
            v_tagMCRANGE =
                _functions.Concat(
                    _functions.Concat(
                        Variant.Create( "word MinYear;word MinMonth;word MinDOW;word MinDay;word MinHour;word MinMinute;word MinSecond;" ),
                        Variant.Create( "word MinMSeconds;word MaxYear;word MaxMonth;word MaxDOW;word MaxDay;word MaxHour;word MaxMinute;word MaxSecond;" ) ),
                    Variant.Create( "word MaxMSeconds;short MinSet;short MaxSet" ) );
            v_tagMCSELRANGE =
                _functions.Concat(
                    _functions.Concat(
                        Variant.Create( "word MinYear;word MinMonth;word MinDOW;word MinDay;word MinHour;word MinMinute;word MinSecond;" ),
                        Variant.Create( "word MinMSeconds;word MaxYear;word MaxMonth;word MaxDOW;word MaxDay;word MaxHour;word MaxMinute;word MaxSecond;" ) ),
                    Variant.Create( "word MaxMSeconds" ) );
            v_tagNMDAYSTATE = _functions.Concat(
                _functions.Concat( _functions.Concat( v_tagNMHDR, Variant.Create( ";" ) ), v_tagSYSTEMTIME ), Variant.Create( ";int DayState;ptr pDayState" ) );
            v_tagNMSELCHANGE =
                _functions.Concat(
                    _functions.Concat(
                        v_tagNMHDR,
                        Variant.Create(
                            ";struct;word BegYear;word BegMonth;word BegDOW;word BegDay;word BegHour;word BegMinute;word BegSecond;word BegMSeconds;endstruct;" ) ),
                    Variant.Create(
                        "struct;word EndYear;word EndMonth;word EndDOW;word EndDay;word EndHour;word EndMinute;word EndSecond;word EndMSeconds;endstruct" ) );
            v_tagNMOBJECTNOTIFY = _functions.Concat( v_tagNMHDR, Variant.Create( ";int Item;ptr piid;ptr pObject;long Result;dword dwFlags" ) );
            v_tagNMTCKEYDOWN = _functions.Concat( _functions.Concat( Variant.Create( "align 1;" ), v_tagNMHDR ), Variant.Create( ";word VKey;uint Flags" ) );
            v_tagTVITEM =
                _functions.Concat(
                    Variant.Create( "struct;uint Mask;handle hItem;uint State;uint StateMask;ptr Text;int TextMax;int Image;int SelectedImage;" ),
                    Variant.Create( "int Children;lparam Param;endstruct" ) );
            v_tagTVITEMEX = _functions.Concat(
                _functions.Concat( Variant.Create( "struct;" ), v_tagTVITEM ),
                Variant.Create( ";int Integral;uint uStateEx;hwnd hwnd;int iExpandedImage;int iReserved;endstruct" ) );
            v_tagNMTREEVIEW =
                _functions.Concat(
                    _functions.Concat(
                        _functions.Concat(
                            _functions.Concat(
                                _functions.Concat(
                                    _functions.Concat( v_tagNMHDR, Variant.Create( ";uint Action;" ) ),
                                    Variant.Create( "struct;uint OldMask;handle OldhItem;uint OldState;uint OldStateMask;" ) ),
                                Variant.Create( "ptr OldText;int OldTextMax;int OldImage;int OldSelectedImage;int OldChildren;lparam OldParam;endstruct;" ) ),
                            Variant.Create( "struct;uint NewMask;handle NewhItem;uint NewState;uint NewStateMask;" ) ),
                        Variant.Create( "ptr NewText;int NewTextMax;int NewImage;int NewSelectedImage;int NewChildren;lparam NewParam;endstruct;" ) ),
                    Variant.Create( "struct;long PointX;long PointY;endstruct" ) );
            v_tagNMTVCUSTOMDRAW =
                _functions.Concat(
                    _functions.Concat(
                        _functions.Concat(
                            _functions.Concat( _functions.Concat( Variant.Create( "struct;" ), v_tagNMHDR ), Variant.Create( ";dword DrawStage;handle HDC;" ) ),
                            v_tagRECT ), Variant.Create( ";dword_ptr ItemSpec;uint ItemState;lparam ItemParam;endstruct" ) ),
                    Variant.Create( ";dword ClrText;dword ClrTextBk;int Level" ) );
            v_tagNMTVDISPINFO = _functions.Concat( _functions.Concat( v_tagNMHDR, Variant.Create( ";" ) ), v_tagTVITEM );
            v_tagNMTVGETINFOTIP = _functions.Concat( v_tagNMHDR, Variant.Create( ";ptr Text;int TextMax;handle hItem;lparam lParam" ) );
            v_tagNMTVITEMCHANGE = _functions.Concat( v_tagNMHDR, Variant.Create( ";uint Changed;handle hItem;uint StateNew;uint StateOld;lparam lParam;" ) );
            v_tagTVHITTESTINFO = _functions.Concat( v_tagPOINT, Variant.Create( ";uint Flags;handle Item" ) );
            v_tagNMTVKEYDOWN = _functions.Concat( _functions.Concat( Variant.Create( "align 1;" ), v_tagNMHDR ), Variant.Create( ";word VKey;uint Flags" ) );
            v_tagNMMOUSE =
                _functions.Concat(
                    _functions.Concat( _functions.Concat( v_tagNMHDR, Variant.Create( ";dword_ptr ItemSpec;dword_ptr ItemData;" ) ), v_tagPOINT ),
                    Variant.Create( ";lparam HitInfo" ) );
            v_tagTOKEN_PRIVILEGES = Variant.Create( "dword Count;align 4;int64 LUID;dword Attributes" );
            v_tagIMAGEINFO = _functions.Concat( Variant.Create( "handle hBitmap;handle hMask;int Unused1;int Unused2;" ), v_tagRECT );
            v_tagMENUINFO = Variant.Create( "dword Size;INT Mask;dword Style;uint YMax;handle hBack;dword ContextHelpID;ulong_ptr MenuData" );
            v_tagMENUITEMINFO =
                _functions.Concat(
                    Variant.Create( "uint Size;uint Mask;uint Type;uint State;uint ID;handle SubMenu;handle BmpChecked;handle BmpUnchecked;" ),
                    Variant.Create( "ulong_ptr ItemData;ptr TypeData;uint CCH;handle BmpItem" ) );
            v_tagREBARBANDINFO =
                _functions.Concat(
                    _functions.Concat(
                        _functions.Concat(
                            Variant.Create( "uint cbSize;uint fMask;uint fStyle;dword clrFore;dword clrBack;ptr lpText;uint cch;" ),
                            Variant.Create(
                                "int iImage;hwnd hwndChild;uint cxMinChild;uint cyMinChild;uint cx;handle hbmBack;uint wID;uint cyChild;uint cyMaxChild;" ) ),
                        Variant.Create( "uint cyIntegral;uint cxIdeal;lparam lParam;uint cxHeader" ) ), _context.OSVersion == Variant.Create( "WIN_XP" )
                            ? Variant.Create( "" )
                            : _functions.Concat( _functions.Concat( Variant.Create( ";" ), v_tagRECT ), Variant.Create( ";uint uChevronState" ) ) );
            v_tagNMREBARAUTOBREAK = _functions.Concat(
                v_tagNMHDR, Variant.Create( ";uint uBand;uint wID;lparam lParam;uint uMsg;uint fStyleCurrent;bool fAutoBreak" ) );
            v_tagNMRBAUTOSIZE =
                _functions.Concat(
                    _functions.Concat(
                        _functions.Concat( v_tagNMHDR, Variant.Create( ";bool fChanged;" ) ),
                        Variant.Create( "struct;long TargetLeft;long TargetTop;long TargetRight;long TargetBottom;endstruct;" ) ),
                    Variant.Create( "struct;long ActualLeft;long ActualTop;long ActualRight;long ActualBottom;endstruct" ) );
            v_tagNMREBAR = _functions.Concat( v_tagNMHDR, Variant.Create( ";dword dwMask;uint uBand;uint fStyle;uint wID;lparam lParam" ) );
            v_tagNMREBARCHEVRON =
                _functions.Concat(
                    _functions.Concat( _functions.Concat( v_tagNMHDR, Variant.Create( ";uint uBand;uint wID;lparam lParam;" ) ), v_tagRECT ),
                    Variant.Create( ";lparam lParamNM" ) );
            v_tagNMREBARCHILDSIZE =
                _functions.Concat(
                    _functions.Concat(
                        _functions.Concat( v_tagNMHDR, Variant.Create( ";uint uBand;uint wID;" ) ),
                        Variant.Create( "struct;long CLeft;long CTop;long CRight;long CBottom;endstruct;" ) ),
                    Variant.Create( "struct;long BLeft;long BTop;long BRight;long BBottom;endstruct" ) );
            v_tagCOLORSCHEME = Variant.Create( "dword Size;dword BtnHighlight;dword BtnShadow" );
            v_tagNMTOOLBAR =
                _functions.Concat(
                    _functions.Concat(
                        _functions.Concat(
                            _functions.Concat( v_tagNMHDR, Variant.Create( ";int iItem;" ) ),
                            Variant.Create( "struct;int iBitmap;int idCommand;byte fsState;byte fsStyle;dword_ptr dwData;int_ptr iString;endstruct" ) ),
                        Variant.Create( ";int cchText;ptr pszText;" ) ), v_tagRECT );
            v_tagNMTBHOTITEM = _functions.Concat( v_tagNMHDR, Variant.Create( ";int idOld;int idNew;dword dwFlags" ) );
            v_tagTBBUTTON = Variant.Create( "int Bitmap;int Command;byte State;byte Style;dword_ptr Param;int_ptr String" );
            v_tagTBBUTTONINFO = Variant.Create(
                "uint Size;dword Mask;int Command;int Image;byte State;byte Style;word CX;dword_ptr Param;ptr Text;int TextMax" );
            v_tagNETRESOURCE = Variant.Create( "dword Scope;dword Type;dword DisplayType;dword Usage;ptr LocalName;ptr RemoteName;ptr Comment;ptr Provider" );
            v_tagOVERLAPPED = Variant.Create( "ulong_ptr Internal;ulong_ptr InternalHigh;struct;dword Offset;dword OffsetHigh;endstruct;handle hEvent" );
            v_tagOPENFILENAME =
                _functions.Concat(
                    _functions.Concat(
                        _functions.Concat(
                            Variant.Create( "dword StructSize;hwnd hwndOwner;handle hInstance;ptr lpstrFilter;ptr lpstrCustomFilter;" ),
                            Variant.Create( "dword nMaxCustFilter;dword nFilterIndex;ptr lpstrFile;dword nMaxFile;ptr lpstrFileTitle;dword nMaxFileTitle;" ) ),
                        Variant.Create(
                            "ptr lpstrInitialDir;ptr lpstrTitle;dword Flags;word nFileOffset;word nFileExtension;ptr lpstrDefExt;lparam lCustData;" ) ),
                    Variant.Create( "ptr lpfnHook;ptr lpTemplateName;ptr pvReserved;dword dwReserved;dword FlagsEx" ) );
            v_tagBITMAPINFOHEADER = _functions.Concat(
                Variant.Create( "struct;dword biSize;long biWidth;long biHeight;word biPlanes;word biBitCount;" ),
                Variant.Create(
                    "dword biCompression;dword biSizeImage;long biXPelsPerMeter;long biYPelsPerMeter;dword biClrUsed;dword biClrImportant;endstruct" ) );
            v_tagBITMAPINFO = _functions.Concat( v_tagBITMAPINFOHEADER, Variant.Create( ";dword biRGBQuad[1]" ) );
            v_tagBLENDFUNCTION = Variant.Create( "byte Op;byte Flags;byte Alpha;byte Format" );
            v_tagGUID = Variant.Create( "struct;ulong Data1;ushort Data2;ushort Data3;byte Data4[8];endstruct" );
            v_tagWINDOWPLACEMENT = Variant.Create( "uint length;uint flags;uint showCmd;long ptMinPosition[2];long ptMaxPosition[2];long rcNormalPosition[4]" );
            v_tagWINDOWPOS = Variant.Create( "hwnd hWnd;hwnd InsertAfter;int X;int Y;int CX;int CY;uint Flags" );
            v_tagSCROLLINFO = Variant.Create( "uint cbSize;uint fMask;int nMin;int nMax;uint nPage;int nPos;int nTrackPos" );
            v_tagSCROLLBARINFO =
                _functions.Concat(
                    _functions.Concat(
                        _functions.Concat( Variant.Create( "dword cbSize;" ), v_tagRECT ), Variant.Create( ";int dxyLineButton;int xyThumbTop;" ) ),
                    Variant.Create( "int xyThumbBottom;int reserved;dword rgstate[6]" ) );
            v_tagLOGFONT =
                _functions.Concat(
                    Variant.Create( "struct;long Height;long Width;long Escapement;long Orientation;long Weight;byte Italic;byte Underline;" ),
                    Variant.Create(
                        "byte Strikeout;byte CharSet;byte OutPrecision;byte ClipPrecision;byte Quality;byte PitchAndFamily;wchar FaceName[32];endstruct" ) );
            v_tagKBDLLHOOKSTRUCT = Variant.Create( "dword vkCode;dword scanCode;dword flags;dword time;ulong_ptr dwExtraInfo" );
            v_tagPROCESS_INFORMATION = Variant.Create( "handle hProcess;handle hThread;dword ProcessID;dword ThreadID" );
            v_tagSTARTUPINFO =
                _functions.Concat(
                    _functions.Concat(
                        Variant.Create( "dword Size;ptr Reserved1;ptr Desktop;ptr Title;dword X;dword Y;dword XSize;dword YSize;dword XCountChars;" ),
                        Variant.Create( "dword YCountChars;dword FillAttribute;dword Flags;word ShowWindow;word Reserved2;ptr Reserved3;handle StdInput;" ) ),
                    Variant.Create( "handle StdOutput;handle StdError" ) );
            v_tagSECURITY_ATTRIBUTES = Variant.Create( "dword Length;ptr Descriptor;bool InheritHandle" );
            v_tagWIN32_FIND_DATA =
                Variant.Create(
                    "dword dwFileAttributes;dword ftCreationTime[2];dword ftLastAccessTime[2];dword ftLastWriteTime[2];dword nFileSizeHigh;dword nFileSizeLow;dword dwReserved0;dword dwReserved1;wchar cFileName[260];wchar cAlternateFileName[14]" );
            v_tagTEXTMETRIC =
                _functions.Concat(
                    _functions.Concat(
                        _functions.Concat(
                            Variant.Create( "long tmHeight;long tmAscent;long tmDescent;long tmInternalLeading;long tmExternalLeading;" ),
                            Variant.Create(
                                "long tmAveCharWidth;long tmMaxCharWidth;long tmWeight;long tmOverhang;long tmDigitizedAspectX;long tmDigitizedAspectY;" ) ),
                        Variant.Create(
                            "wchar tmFirstChar;wchar tmLastChar;wchar tmDefaultChar;wchar tmBreakChar;byte tmItalic;byte tmUnderlined;byte tmStruckOut;" ) ),
                    Variant.Create( "byte tmPitchAndFamily;byte tmCharSet" ) );
            v_STR_NOCASESENSE = Variant.Create( 0 );
            v_STR_CASESENSE = Variant.Create( 1 );
            v_STR_NOCASESENSEBASIC = Variant.Create( 2 );
            v_STR_STRIPLEADING = Variant.Create( 1 );
            v_STR_STRIPTRAILING = Variant.Create( 2 );
            v_STR_STRIPSPACES = Variant.Create( 4 );
            v_STR_STRIPALL = Variant.Create( 8 );
            v_STR_CHRSPLIT = Variant.Create( 0 );
            v_STR_ENTIRESPLIT = Variant.Create( 1 );
            v_STR_NOCOUNT = Variant.Create( 2 );
            v_STR_REGEXPMATCH = Variant.Create( 0 );
            v_STR_REGEXPARRAYMATCH = Variant.Create( 1 );
            v_STR_REGEXPARRAYFULLMATCH = Variant.Create( 2 );
            v_STR_REGEXPARRAYGLOBALMATCH = Variant.Create( 3 );
            v_STR_REGEXPARRAYGLOBALFULLMATCH = Variant.Create( 4 );
            v_STR_ENDISSTART = Variant.Create( 0 );
            v_STR_ENDNOTSTART = Variant.Create( 1 );
            v_SE_ASSIGNPRIMARYTOKEN_NAME = Variant.Create( "SeAssignPrimaryTokenPrivilege" );
            v_SE_AUDIT_NAME = Variant.Create( "SeAuditPrivilege" );
            v_SE_BACKUP_NAME = Variant.Create( "SeBackupPrivilege" );
            v_SE_CHANGE_NOTIFY_NAME = Variant.Create( "SeChangeNotifyPrivilege" );
            v_SE_CREATE_GLOBAL_NAME = Variant.Create( "SeCreateGlobalPrivilege" );
            v_SE_CREATE_PAGEFILE_NAME = Variant.Create( "SeCreatePagefilePrivilege" );
            v_SE_CREATE_PERMANENT_NAME = Variant.Create( "SeCreatePermanentPrivilege" );
            v_SE_CREATE_SYMBOLIC_LINK_NAME = Variant.Create( "SeCreateSymbolicLinkPrivilege" );
            v_SE_CREATE_TOKEN_NAME = Variant.Create( "SeCreateTokenPrivilege" );
            v_SE_DEBUG_NAME = Variant.Create( "SeDebugPrivilege" );
            v_SE_ENABLE_DELEGATION_NAME = Variant.Create( "SeEnableDelegationPrivilege" );
            v_SE_IMPERSONATE_NAME = Variant.Create( "SeImpersonatePrivilege" );
            v_SE_INC_BASE_PRIORITY_NAME = Variant.Create( "SeIncreaseBasePriorityPrivilege" );
            v_SE_INC_WORKING_SET_NAME = Variant.Create( "SeIncreaseWorkingSetPrivilege" );
            v_SE_INCREASE_QUOTA_NAME = Variant.Create( "SeIncreaseQuotaPrivilege" );
            v_SE_LOAD_DRIVER_NAME = Variant.Create( "SeLoadDriverPrivilege" );
            v_SE_LOCK_MEMORY_NAME = Variant.Create( "SeLockMemoryPrivilege" );
            v_SE_MACHINE_ACCOUNT_NAME = Variant.Create( "SeMachineAccountPrivilege" );
            v_SE_MANAGE_VOLUME_NAME = Variant.Create( "SeManageVolumePrivilege" );
            v_SE_PROF_SINGLE_PROCESS_NAME = Variant.Create( "SeProfileSingleProcessPrivilege" );
            v_SE_RELABEL_NAME = Variant.Create( "SeRelabelPrivilege" );
            v_SE_REMOTE_SHUTDOWN_NAME = Variant.Create( "SeRemoteShutdownPrivilege" );
            v_SE_RESTORE_NAME = Variant.Create( "SeRestorePrivilege" );
            v_SE_SECURITY_NAME = Variant.Create( "SeSecurityPrivilege" );
            v_SE_SHUTDOWN_NAME = Variant.Create( "SeShutdownPrivilege" );
            v_SE_SYNC_AGENT_NAME = Variant.Create( "SeSyncAgentPrivilege" );
            v_SE_SYSTEM_ENVIRONMENT_NAME = Variant.Create( "SeSystemEnvironmentPrivilege" );
            v_SE_SYSTEM_PROFILE_NAME = Variant.Create( "SeSystemProfilePrivilege" );
            v_SE_SYSTEMTIME_NAME = Variant.Create( "SeSystemtimePrivilege" );
            v_SE_TAKE_OWNERSHIP_NAME = Variant.Create( "SeTakeOwnershipPrivilege" );
            v_SE_TCB_NAME = Variant.Create( "SeTcbPrivilege" );
            v_SE_TIME_ZONE_NAME = Variant.Create( "SeTimeZonePrivilege" );
            v_SE_TRUSTED_CREDMAN_ACCESS_NAME = Variant.Create( "SeTrustedCredManAccessPrivilege" );
            v_SE_UNSOLICITED_INPUT_NAME = Variant.Create( "SeUnsolicitedInputPrivilege" );
            v_SE_UNDOCK_NAME = Variant.Create( "SeUndockPrivilege" );
            v_SE_PRIVILEGE_ENABLED_BY_DEFAULT = Variant.Create( 1 );
            v_SE_PRIVILEGE_ENABLED = Variant.Create( 2 );
            v_SE_PRIVILEGE_REMOVED = Variant.Create( 4 );
            v_SE_PRIVILEGE_USED_FOR_ACCESS = Variant.Create( -2147483648 );
            v_SE_GROUP_MANDATORY = Variant.Create( 1 );
            v_SE_GROUP_ENABLED_BY_DEFAULT = Variant.Create( 2 );
            v_SE_GROUP_ENABLED = Variant.Create( 4 );
            v_SE_GROUP_OWNER = Variant.Create( 8 );
            v_SE_GROUP_USE_FOR_DENY_ONLY = Variant.Create( 16 );
            v_SE_GROUP_INTEGRITY = Variant.Create( 32 );
            v_SE_GROUP_INTEGRITY_ENABLED = Variant.Create( 64 );
            v_SE_GROUP_RESOURCE = Variant.Create( 536870912 );
            v_SE_GROUP_LOGON_ID = Variant.Create( -1073741824 );
            v_TOKENPRIMARY = Variant.Create( 1 );
            v_TOKENIMPERSONATION = v_TOKENPRIMARY+Variant.Create( 1 );
            v_SECURITYANONYMOUS = Variant.Create( 0 );
            v_SECURITYIDENTIFICATION = v_SECURITYANONYMOUS+Variant.Create( 1 );
            v_SECURITYIMPERSONATION = v_SECURITYIDENTIFICATION+Variant.Create( 1 );
            v_SECURITYDELEGATION = v_SECURITYIMPERSONATION+Variant.Create( 1 );
            v_TOKENUSER = Variant.Create( 1 );
            v_TOKENGROUPS = v_TOKENUSER+Variant.Create( 1 );
            v_TOKENPRIVILEGES = v_TOKENGROUPS+Variant.Create( 1 );
            v_TOKENOWNER = v_TOKENPRIVILEGES+Variant.Create( 1 );
            v_TOKENPRIMARYGROUP = v_TOKENOWNER+Variant.Create( 1 );
            v_TOKENDEFAULTDACL = v_TOKENPRIMARYGROUP+Variant.Create( 1 );
            v_TOKENSOURCE = v_TOKENDEFAULTDACL+Variant.Create( 1 );
            v_TOKENTYPE = v_TOKENSOURCE+Variant.Create( 1 );
            v_TOKENIMPERSONATIONLEVEL = v_TOKENTYPE+Variant.Create( 1 );
            v_TOKENSTATISTICS = v_TOKENIMPERSONATIONLEVEL+Variant.Create( 1 );
            v_TOKENRESTRICTEDSIDS = v_TOKENSTATISTICS+Variant.Create( 1 );
            v_TOKENSESSIONID = v_TOKENRESTRICTEDSIDS+Variant.Create( 1 );
            v_TOKENGROUPSANDPRIVILEGES = v_TOKENSESSIONID+Variant.Create( 1 );
            v_TOKENSESSIONREFERENCE = v_TOKENGROUPSANDPRIVILEGES+Variant.Create( 1 );
            v_TOKENSANDBOXINERT = v_TOKENSESSIONREFERENCE+Variant.Create( 1 );
            v_TOKENAUDITPOLICY = v_TOKENSANDBOXINERT+Variant.Create( 1 );
            v_TOKENORIGIN = v_TOKENAUDITPOLICY+Variant.Create( 1 );
            v_TOKENELEVATIONTYPE = v_TOKENORIGIN+Variant.Create( 1 );
            v_TOKENLINKEDTOKEN = v_TOKENELEVATIONTYPE+Variant.Create( 1 );
            v_TOKENELEVATION = v_TOKENLINKEDTOKEN+Variant.Create( 1 );
            v_TOKENHASRESTRICTIONS = v_TOKENELEVATION+Variant.Create( 1 );
            v_TOKENACCESSINFORMATION = v_TOKENHASRESTRICTIONS+Variant.Create( 1 );
            v_TOKENVIRTUALIZATIONALLOWED = v_TOKENACCESSINFORMATION+Variant.Create( 1 );
            v_TOKENVIRTUALIZATIONENABLED = v_TOKENVIRTUALIZATIONALLOWED+Variant.Create( 1 );
            v_TOKENINTEGRITYLEVEL = v_TOKENVIRTUALIZATIONENABLED+Variant.Create( 1 );
            v_TOKENUIACCESS = v_TOKENINTEGRITYLEVEL+Variant.Create( 1 );
            v_TOKENMANDATORYPOLICY = v_TOKENUIACCESS+Variant.Create( 1 );
            v_TOKENLOGONSID = v_TOKENMANDATORYPOLICY+Variant.Create( 1 );
            v_TOKEN_ASSIGN_PRIMARY = Variant.Create( 1 );
            v_TOKEN_DUPLICATE = Variant.Create( 2 );
            v_TOKEN_IMPERSONATE = Variant.Create( 4 );
            v_TOKEN_QUERY = Variant.Create( 8 );
            v_TOKEN_QUERY_SOURCE = Variant.Create( 16 );
            v_TOKEN_ADJUST_PRIVILEGES = Variant.Create( 32 );
            v_TOKEN_ADJUST_GROUPS = Variant.Create( 64 );
            v_TOKEN_ADJUST_DEFAULT = Variant.Create( 128 );
            v_TOKEN_ADJUST_SESSIONID = Variant.Create( 256 );
            v_TOKEN_ALL_ACCESS = Variant.Create( 983551 );
            v_TOKEN_READ = Variant.Create( 131080 );
            v_TOKEN_WRITE = Variant.Create( 131296 );
            v_TOKEN_EXECUTE = Variant.Create( 131072 );
            v_TOKEN_HAS_TRAVERSE_PRIVILEGE = Variant.Create( 1 );
            v_TOKEN_HAS_BACKUP_PRIVILEGE = Variant.Create( 2 );
            v_TOKEN_HAS_RESTORE_PRIVILEGE = Variant.Create( 4 );
            v_TOKEN_HAS_ADMIN_GROUP = Variant.Create( 8 );
            v_TOKEN_IS_RESTRICTED = Variant.Create( 16 );
            v_TOKEN_SESSION_NOT_REFERENCED = Variant.Create( 32 );
            v_TOKEN_SANDBOX_INERT = Variant.Create( 64 );
            v_TOKEN_HAS_IMPERSONATE_PRIVILEGE = Variant.Create( 128 );
            v_RIGHTS_DELETE = Variant.Create( 65536 );
            v_READ_CONTROL = Variant.Create( 131072 );
            v_WRITE_DAC = Variant.Create( 262144 );
            v_WRITE_OWNER = Variant.Create( 524288 );
            v_SYNCHRONIZE = Variant.Create( 1048576 );
            v_ACCESS_SYSTEM_SECURITY = Variant.Create( 16777216 );
            v_STANDARD_RIGHTS_REQUIRED = Variant.Create( 983040 );
            v_STANDARD_RIGHTS_READ = v_READ_CONTROL;
            v_STANDARD_RIGHTS_WRITE = v_READ_CONTROL;
            v_STANDARD_RIGHTS_EXECUTE = v_READ_CONTROL;
            v_STANDARD_RIGHTS_ALL = Variant.Create( 2031616 );
            v_SPECIFIC_RIGHTS_ALL = Variant.Create( 65535 );
            v_NOT_USED_ACCESS = Variant.Create( 0 );
            v_GRANT_ACCESS = v_NOT_USED_ACCESS+Variant.Create( 1 );
            v_SET_ACCESS = v_GRANT_ACCESS+Variant.Create( 1 );
            v_DENY_ACCESS = v_SET_ACCESS+Variant.Create( 1 );
            v_REVOKE_ACCESS = v_DENY_ACCESS+Variant.Create( 1 );
            v_SET_AUDIT_SUCCESS = v_REVOKE_ACCESS+Variant.Create( 1 );
            v_SET_AUDIT_FAILURE = v_SET_AUDIT_SUCCESS+Variant.Create( 1 );
            v_TRUSTEE_IS_UNKNOWN = Variant.Create( 0 );
            v_TRUSTEE_IS_USER = v_TRUSTEE_IS_UNKNOWN+Variant.Create( 1 );
            v_TRUSTEE_IS_GROUP = v_TRUSTEE_IS_USER+Variant.Create( 1 );
            v_TRUSTEE_IS_DOMAIN = v_TRUSTEE_IS_GROUP+Variant.Create( 1 );
            v_TRUSTEE_IS_ALIAS = v_TRUSTEE_IS_DOMAIN+Variant.Create( 1 );
            v_TRUSTEE_IS_WELL_KNOWN_GROUP = v_TRUSTEE_IS_ALIAS+Variant.Create( 1 );
            v_TRUSTEE_IS_DELETED = v_TRUSTEE_IS_WELL_KNOWN_GROUP+Variant.Create( 1 );
            v_TRUSTEE_IS_INVALID = v_TRUSTEE_IS_DELETED+Variant.Create( 1 );
            v_TRUSTEE_IS_COMPUTER = v_TRUSTEE_IS_INVALID+Variant.Create( 1 );
            v_LOGON_WITH_PROFILE = Variant.Create( 1 );
            v_LOGON_NETCREDENTIALS_ONLY = Variant.Create( 2 );
            v_SIDTYPEUSER = Variant.Create( 1 );
            v_SIDTYPEGROUP = v_SIDTYPEUSER+Variant.Create( 1 );
            v_SIDTYPEDOMAIN = v_SIDTYPEGROUP+Variant.Create( 1 );
            v_SIDTYPEALIAS = v_SIDTYPEDOMAIN+Variant.Create( 1 );
            v_SIDTYPEWELLKNOWNGROUP = v_SIDTYPEALIAS+Variant.Create( 1 );
            v_SIDTYPEDELETEDACCOUNT = v_SIDTYPEWELLKNOWNGROUP+Variant.Create( 1 );
            v_SIDTYPEINVALID = v_SIDTYPEDELETEDACCOUNT+Variant.Create( 1 );
            v_SIDTYPEUNKNOWN = v_SIDTYPEINVALID+Variant.Create( 1 );
            v_SIDTYPECOMPUTER = v_SIDTYPEUNKNOWN+Variant.Create( 1 );
            v_SIDTYPELABEL = v_SIDTYPECOMPUTER+Variant.Create( 1 );
            v_SID_ADMINISTRATORS = Variant.Create( "S-1-5-32-544" );
            v_SID_USERS = Variant.Create( "S-1-5-32-545" );
            v_SID_GUESTS = Variant.Create( "S-1-5-32-546" );
            v_SID_ACCOUNT_OPERATORS = Variant.Create( "S-1-5-32-548" );
            v_SID_SERVER_OPERATORS = Variant.Create( "S-1-5-32-549" );
            v_SID_PRINT_OPERATORS = Variant.Create( "S-1-5-32-550" );
            v_SID_BACKUP_OPERATORS = Variant.Create( "S-1-5-32-551" );
            v_SID_REPLICATOR = Variant.Create( "S-1-5-32-552" );
            v_SID_OWNER = Variant.Create( "S-1-3-0" );
            v_SID_EVERYONE = Variant.Create( "S-1-1-0" );
            v_SID_NETWORK = Variant.Create( "S-1-5-2" );
            v_SID_INTERACTIVE = Variant.Create( "S-1-5-4" );
            v_SID_SYSTEM = Variant.Create( "S-1-5-18" );
            v_SID_AUTHENTICATED_USERS = Variant.Create( "S-1-5-11" );
            v_SID_SCHANNEL_AUTHENTICATION = Variant.Create( "S-1-5-64-14" );
            v_SID_DIGEST_AUTHENTICATION = Variant.Create( "S-1-5-64-21" );
            v_SID_NT_SERVICE = Variant.Create( "S-1-5-80" );
            v_SID_UNTRUSTED_MANDATORY_LEVEL = Variant.Create( "S-1-16-0" );
            v_SID_LOW_MANDATORY_LEVEL = Variant.Create( "S-1-16-4096" );
            v_SID_MEDIUM_MANDATORY_LEVEL = Variant.Create( "S-1-16-8192" );
            v_SID_MEDIUM_PLUS_MANDATORY_LEVEL = Variant.Create( "S-1-16-8448" );
            v_SID_HIGH_MANDATORY_LEVEL = Variant.Create( "S-1-16-12288" );
            v_SID_SYSTEM_MANDATORY_LEVEL = Variant.Create( "S-1-16-16384" );
            v_SID_PROTECTED_PROCESS_MANDATORY_LEVEL = Variant.Create( "S-1-16-20480" );
            v_SID_SECURE_PROCESS_MANDATORY_LEVEL = Variant.Create( "S-1-16-28672" );
            v_SID_ALL_SERVICES = Variant.Create( "S-1-5-80-0" );
            v_MB_OK = Variant.Create( 0 );
            v_MB_OKCANCEL = Variant.Create( 1 );
            v_MB_ABORTRETRYIGNORE = Variant.Create( 2 );
            v_MB_YESNOCANCEL = Variant.Create( 3 );
            v_MB_YESNO = Variant.Create( 4 );
            v_MB_RETRYCANCEL = Variant.Create( 5 );
            v_MB_CANCELTRYCONTINUE = Variant.Create( 6 );
            v_MB_HELP = Variant.Create( 16384 );
            v_MB_ICONSTOP = Variant.Create( 16 );
            v_MB_ICONERROR = Variant.Create( 16 );
            v_MB_ICONHAND = Variant.Create( 16 );
            v_MB_ICONQUESTION = Variant.Create( 32 );
            v_MB_ICONEXCLAMATION = Variant.Create( 48 );
            v_MB_ICONWARNING = Variant.Create( 48 );
            v_MB_ICONINFORMATION = Variant.Create( 64 );
            v_MB_ICONASTERISK = Variant.Create( 64 );
            v_MB_USERICON = Variant.Create( 128 );
            v_MB_DEFBUTTON1 = Variant.Create( 0 );
            v_MB_DEFBUTTON2 = Variant.Create( 256 );
            v_MB_DEFBUTTON3 = Variant.Create( 512 );
            v_MB_DEFBUTTON4 = Variant.Create( 768 );
            v_MB_APPLMODAL = Variant.Create( 0 );
            v_MB_SYSTEMMODAL = Variant.Create( 4096 );
            v_MB_TASKMODAL = Variant.Create( 8192 );
            v_MB_DEFAULT_DESKTOP_ONLY = Variant.Create( 131072 );
            v_MB_RIGHT = Variant.Create( 524288 );
            v_MB_RTLREADING = Variant.Create( 1048576 );
            v_MB_SETFOREGROUND = Variant.Create( 65536 );
            v_MB_TOPMOST = Variant.Create( 262144 );
            v_MB_SERVICE_NOTIFICATION = Variant.Create( 2097152 );
            v_MB_RIGHTJUSTIFIED = v_MB_RIGHT;
            v_IDTIMEOUT = Variant.Create( -1 );
            v_IDOK = Variant.Create( 1 );
            v_IDCANCEL = Variant.Create( 2 );
            v_IDABORT = Variant.Create( 3 );
            v_IDRETRY = Variant.Create( 4 );
            v_IDIGNORE = Variant.Create( 5 );
            v_IDYES = Variant.Create( 6 );
            v_IDNO = Variant.Create( 7 );
            v_IDCLOSE = Variant.Create( 8 );
            v_IDHELP = Variant.Create( 9 );
            v_IDTRYAGAIN = Variant.Create( 10 );
            v_IDCONTINUE = Variant.Create( 11 );
            v_FC_NOOVERWRITE = Variant.Create( 0 );
            v_FC_OVERWRITE = Variant.Create( 1 );
            v_FC_CREATEPATH = Variant.Create( 8 );
            v_FT_MODIFIED = Variant.Create( 0 );
            v_FT_CREATED = Variant.Create( 1 );
            v_FT_ACCESSED = Variant.Create( 2 );
            v_FO_READ = Variant.Create( 0 );
            v_FO_APPEND = Variant.Create( 1 );
            v_FO_OVERWRITE = Variant.Create( 2 );
            v_FO_CREATEPATH = Variant.Create( 8 );
            v_FO_BINARY = Variant.Create( 16 );
            v_FO_UNICODE = Variant.Create( 32 );
            v_FO_UTF16_LE = Variant.Create( 32 );
            v_FO_UTF16_BE = Variant.Create( 64 );
            v_FO_UTF8 = Variant.Create( 128 );
            v_FO_UTF8_NOBOM = Variant.Create( 256 );
            v_FO_UTF8_FULL = Variant.Create( 16384 );
            v_EOF = Variant.Create( -1 );
            v_FD_FILEMUSTEXIST = Variant.Create( 1 );
            v_FD_PATHMUSTEXIST = Variant.Create( 2 );
            v_FD_MULTISELECT = Variant.Create( 4 );
            v_FD_PROMPTCREATENEW = Variant.Create( 8 );
            v_FD_PROMPTOVERWRITE = Variant.Create( 16 );
            v_CREATE_NEW = Variant.Create( 1 );
            v_CREATE_ALWAYS = Variant.Create( 2 );
            v_OPEN_EXISTING = Variant.Create( 3 );
            v_OPEN_ALWAYS = Variant.Create( 4 );
            v_TRUNCATE_EXISTING = Variant.Create( 5 );
            v_INVALID_SET_FILE_POINTER = Variant.Create( -1 );
            v_FILE_BEGIN = Variant.Create( 0 );
            v_FILE_CURRENT = Variant.Create( 1 );
            v_FILE_END = Variant.Create( 2 );
            v_FILE_ATTRIBUTE_READONLY = Variant.Create( 1 );
            v_FILE_ATTRIBUTE_HIDDEN = Variant.Create( 2 );
            v_FILE_ATTRIBUTE_SYSTEM = Variant.Create( 4 );
            v_FILE_ATTRIBUTE_DIRECTORY = Variant.Create( 16 );
            v_FILE_ATTRIBUTE_ARCHIVE = Variant.Create( 32 );
            v_FILE_ATTRIBUTE_DEVICE = Variant.Create( 64 );
            v_FILE_ATTRIBUTE_NORMAL = Variant.Create( 128 );
            v_FILE_ATTRIBUTE_TEMPORARY = Variant.Create( 256 );
            v_FILE_ATTRIBUTE_SPARSE_FILE = Variant.Create( 512 );
            v_FILE_ATTRIBUTE_REPARSE_POINT = Variant.Create( 1024 );
            v_FILE_ATTRIBUTE_COMPRESSED = Variant.Create( 2048 );
            v_FILE_ATTRIBUTE_OFFLINE = Variant.Create( 4096 );
            v_FILE_ATTRIBUTE_NOT_CONTENT_INDEXED = Variant.Create( 8192 );
            v_FILE_ATTRIBUTE_ENCRYPTED = Variant.Create( 16384 );
            v_FILE_SHARE_READ = Variant.Create( 1 );
            v_FILE_SHARE_WRITE = Variant.Create( 2 );
            v_FILE_SHARE_DELETE = Variant.Create( 4 );
            v_FILE_SHARE_READWRITE = _functions.BitOR( v_FILE_SHARE_READ, v_FILE_SHARE_WRITE );
            v_FILE_SHARE_ANY = _functions.BitOR( v_FILE_SHARE_READ, v_FILE_SHARE_WRITE, v_FILE_SHARE_DELETE );
            v_GENERIC_ALL = Variant.Create( 268435456 );
            v_GENERIC_EXECUTE = Variant.Create( 536870912 );
            v_GENERIC_WRITE = Variant.Create( 1073741824 );
            v_GENERIC_READ = Variant.Create( -2147483648 );
            v_GENERIC_READWRITE = _functions.BitOR( v_GENERIC_READ, v_GENERIC_WRITE );
            v_FRTA_NOCOUNT = Variant.Create( 0 );
            v_FRTA_COUNT = Variant.Create( 1 );
            v_FRTA_INTARRAYS = Variant.Create( 2 );
            v_FRTA_ENTIRESPLIT = Variant.Create( 4 );
            v_FLTA_FILESFOLDERS = Variant.Create( 0 );
            v_FLTA_FILES = Variant.Create( 1 );
            v_FLTA_FOLDERS = Variant.Create( 2 );
            v_FLTAR_FILESFOLDERS = Variant.Create( 0 );
            v_FLTAR_FILES = Variant.Create( 1 );
            v_FLTAR_FOLDERS = Variant.Create( 2 );
            v_FLTAR_NOHIDDEN = Variant.Create( 4 );
            v_FLTAR_NOSYSTEM = Variant.Create( 8 );
            v_FLTAR_NOLINK = Variant.Create( 16 );
            v_FLTAR_NORECUR = Variant.Create( 0 );
            v_FLTAR_RECUR = Variant.Create( 1 );
            v_FLTAR_NOSORT = Variant.Create( 0 );
            v_FLTAR_SORT = Variant.Create( 1 );
            v_FLTAR_FASTSORT = Variant.Create( 2 );
            v_FLTAR_NOPATH = Variant.Create( 0 );
            v_FLTAR_RELPATH = Variant.Create( 1 );
            v_FLTAR_FULLPATH = Variant.Create( 2 );
            v_OPT_COORDSRELATIVE = Variant.Create( 0 );
            v_OPT_COORDSABSOLUTE = Variant.Create( 1 );
            v_OPT_COORDSCLIENT = Variant.Create( 2 );
            v_OPT_ERRORSILENT = Variant.Create( 0 );
            v_OPT_ERRORFATAL = Variant.Create( 1 );
            v_OPT_CAPSNOSTORE = Variant.Create( 0 );
            v_OPT_CAPSSTORE = Variant.Create( 1 );
            v_OPT_MATCHSTART = Variant.Create( 1 );
            v_OPT_MATCHANY = Variant.Create( 2 );
            v_OPT_MATCHEXACT = Variant.Create( 3 );
            v_OPT_MATCHADVANCED = Variant.Create( 4 );
            v_CCS_TOP = Variant.Create( 1 );
            v_CCS_NOMOVEY = Variant.Create( 2 );
            v_CCS_BOTTOM = Variant.Create( 3 );
            v_CCS_NORESIZE = Variant.Create( 4 );
            v_CCS_NOPARENTALIGN = Variant.Create( 8 );
            v_CCS_NOHILITE = Variant.Create( 16 );
            v_CCS_ADJUSTABLE = Variant.Create( 32 );
            v_CCS_NODIVIDER = Variant.Create( 64 );
            v_CCS_VERT = Variant.Create( 128 );
            v_CCS_LEFT = Variant.Create( 129 );
            v_CCS_NOMOVEX = Variant.Create( 130 );
            v_CCS_RIGHT = Variant.Create( 131 );
            v_DT_DRIVETYPE = Variant.Create( 1 );
            v_DT_SSDSTATUS = Variant.Create( 2 );
            v_DT_BUSTYPE = Variant.Create( 3 );
            v_OBJID_WINDOW = Variant.Create( 0 );
            v_OBJID_TITLEBAR = Variant.Create( -2 );
            v_OBJID_SIZEGRIP = Variant.Create( -7 );
            v_OBJID_CARET = Variant.Create( -8 );
            v_OBJID_CURSOR = Variant.Create( -9 );
            v_OBJID_ALERT = Variant.Create( -10 );
            v_OBJID_SOUND = Variant.Create( -11 );
            v_DLG_NOTITLE = Variant.Create( 1 );
            v_DLG_NOTONTOP = Variant.Create( 2 );
            v_DLG_TEXTLEFT = Variant.Create( 4 );
            v_DLG_TEXTRIGHT = Variant.Create( 8 );
            v_DLG_MOVEABLE = Variant.Create( 16 );
            v_DLG_TEXTVCENTER = Variant.Create( 32 );
            v_IDC_UNKNOWN = Variant.Create( 0 );
            v_IDC_APPSTARTING = Variant.Create( 1 );
            v_IDC_ARROW = Variant.Create( 2 );
            v_IDC_CROSS = Variant.Create( 3 );
            v_IDC_HAND = Variant.Create( 32649 );
            v_IDC_HELP = Variant.Create( 4 );
            v_IDC_IBEAM = Variant.Create( 5 );
            v_IDC_ICON = Variant.Create( 6 );
            v_IDC_NO = Variant.Create( 7 );
            v_IDC_SIZE = Variant.Create( 8 );
            v_IDC_SIZEALL = Variant.Create( 9 );
            v_IDC_SIZENESW = Variant.Create( 10 );
            v_IDC_SIZENS = Variant.Create( 11 );
            v_IDC_SIZENWSE = Variant.Create( 12 );
            v_IDC_SIZEWE = Variant.Create( 13 );
            v_IDC_UPARROW = Variant.Create( 14 );
            v_IDC_WAIT = Variant.Create( 15 );
            v_IDI_APPLICATION = Variant.Create( 32512 );
            v_IDI_ASTERISK = Variant.Create( 32516 );
            v_IDI_EXCLAMATION = Variant.Create( 32515 );
            v_IDI_HAND = Variant.Create( 32513 );
            v_IDI_QUESTION = Variant.Create( 32514 );
            v_IDI_WINLOGO = Variant.Create( 32517 );
            v_IDI_SHIELD = Variant.Create( 32518 );
            v_IDI_ERROR = v_IDI_HAND;
            v_IDI_INFORMATION = v_IDI_ASTERISK;
            v_IDI_WARNING = v_IDI_EXCLAMATION;
            v_SD_LOGOFF = Variant.Create( 0 );
            v_SD_SHUTDOWN = Variant.Create( 1 );
            v_SD_REBOOT = Variant.Create( 2 );
            v_SD_FORCE = Variant.Create( 4 );
            v_SD_POWERDOWN = Variant.Create( 8 );
            v_SD_FORCEHUNG = Variant.Create( 16 );
            v_SD_STANDBY = Variant.Create( 32 );
            v_SD_HIBERNATE = Variant.Create( 64 );
            v_STDIN_CHILD = Variant.Create( 1 );
            v_STDOUT_CHILD = Variant.Create( 2 );
            v_STDERR_CHILD = Variant.Create( 4 );
            v_STDERR_MERGED = Variant.Create( 8 );
            v_STDIO_INHERIT_PARENT = Variant.Create( 16 );
            v_RUN_CREATE_NEW_CONSOLE = Variant.Create( 65536 );
            v_UBOUND_DIMENSIONS = Variant.Create( 0 );
            v_UBOUND_ROWS = Variant.Create( 1 );
            v_UBOUND_COLUMNS = Variant.Create( 2 );
            v_MOUSEEVENTF_ABSOLUTE = Variant.Create( 32768 );
            v_MOUSEEVENTF_MOVE = Variant.Create( 1 );
            v_MOUSEEVENTF_LEFTDOWN = Variant.Create( 2 );
            v_MOUSEEVENTF_LEFTUP = Variant.Create( 4 );
            v_MOUSEEVENTF_RIGHTDOWN = Variant.Create( 8 );
            v_MOUSEEVENTF_RIGHTUP = Variant.Create( 16 );
            v_MOUSEEVENTF_MIDDLEDOWN = Variant.Create( 32 );
            v_MOUSEEVENTF_MIDDLEUP = Variant.Create( 64 );
            v_MOUSEEVENTF_WHEEL = Variant.Create( 2048 );
            v_MOUSEEVENTF_XDOWN = Variant.Create( 128 );
            v_MOUSEEVENTF_XUP = Variant.Create( 256 );
            v_REG_NONE = Variant.Create( 0 );
            v_REG_SZ = Variant.Create( 1 );
            v_REG_EXPAND_SZ = Variant.Create( 2 );
            v_REG_BINARY = Variant.Create( 3 );
            v_REG_DWORD = Variant.Create( 4 );
            v_REG_DWORD_LITTLE_ENDIAN = Variant.Create( 4 );
            v_REG_DWORD_BIG_ENDIAN = Variant.Create( 5 );
            v_REG_LINK = Variant.Create( 6 );
            v_REG_MULTI_SZ = Variant.Create( 7 );
            v_REG_RESOURCE_LIST = Variant.Create( 8 );
            v_REG_FULL_RESOURCE_DESCRIPTOR = Variant.Create( 9 );
            v_REG_RESOURCE_REQUIREMENTS_LIST = Variant.Create( 10 );
            v_REG_QWORD = Variant.Create( 11 );
            v_REG_QWORD_LITTLE_ENDIAN = Variant.Create( 11 );
            v_HWND_BOTTOM = Variant.Create( 1 );
            v_HWND_NOTOPMOST = Variant.Create( -2 );
            v_HWND_TOP = Variant.Create( 0 );
            v_HWND_TOPMOST = Variant.Create( -1 );
            v_SWP_NOSIZE = Variant.Create( 1 );
            v_SWP_NOMOVE = Variant.Create( 2 );
            v_SWP_NOZORDER = Variant.Create( 4 );
            v_SWP_NOREDRAW = Variant.Create( 8 );
            v_SWP_NOACTIVATE = Variant.Create( 16 );
            v_SWP_FRAMECHANGED = Variant.Create( 32 );
            v_SWP_DRAWFRAME = Variant.Create( 32 );
            v_SWP_SHOWWINDOW = Variant.Create( 64 );
            v_SWP_HIDEWINDOW = Variant.Create( 128 );
            v_SWP_NOCOPYBITS = Variant.Create( 256 );
            v_SWP_NOOWNERZORDER = Variant.Create( 512 );
            v_SWP_NOREPOSITION = Variant.Create( 512 );
            v_SWP_NOSENDCHANGING = Variant.Create( 1024 );
            v_SWP_DEFERERASE = Variant.Create( 8192 );
            v_SWP_ASYNCWINDOWPOS = Variant.Create( 16384 );
            v_KEYWORD_DEFAULT = Variant.Create( 1 );
            v_KEYWORD_NULL = Variant.Create( 2 );
            v___g_aInProcess_WinAPI = Variant.CreateArray( new Variant[Variant.Create( 64 ), Variant.Create( 2 )] );
            v___g_aInProcess_WinAPI.InitArray(
                new Variant[] {
                    new[] {
                        Variant.Create( 0 ),
                        Variant.Create( 0 )
                    }
                }
                );
            v___g_aWinList_WinAPI = Variant.CreateArray( new Variant[Variant.Create( 64 ), Variant.Create( 2 )] );
            v___g_aWinList_WinAPI.InitArray(
                new Variant[] {
                    new[] {
                        Variant.Create( 0 ),
                        Variant.Create( 0 )
                    }
                }
                );
            v___WINAPICONSTANT_WM_SETFONT = Variant.Create( 48 );
            v___WINAPICONSTANT_FW_NORMAL = Variant.Create( 400 );
            v___WINAPICONSTANT_DEFAULT_CHARSET = Variant.Create( 1 );
            v___WINAPICONSTANT_OUT_DEFAULT_PRECIS = Variant.Create( 0 );
            v___WINAPICONSTANT_CLIP_DEFAULT_PRECIS = Variant.Create( 0 );
            v___WINAPICONSTANT_DEFAULT_QUALITY = Variant.Create( 0 );
            v___WINAPICONSTANT_LOGPIXELSX = Variant.Create( 88 );
            v___WINAPICONSTANT_LOGPIXELSY = Variant.Create( 90 );
            v_tagCURSORINFO = _functions.Concat( Variant.Create( "dword Size;dword Flags;handle hCursor;" ), v_tagPOINT );
            v_tagDISPLAY_DEVICE = Variant.Create( "dword Size;wchar Name[32];wchar String[128];dword Flags;wchar ID[128];wchar Key[128]" );
            v_tagFLASHWINFO = Variant.Create( "uint Size;hwnd hWnd;dword Flags;uint Count;dword TimeOut" );
            v_tagICONINFO = Variant.Create( "bool Icon;dword XHotSpot;dword YHotSpot;handle hMask;handle hColor" );
            v_tagMEMORYSTATUSEX =
                _functions.Concat(
                    _functions.Concat(
                        Variant.Create( "dword Length;dword MemoryLoad;" ),
                        Variant.Create( "uint64 TotalPhys;uint64 AvailPhys;uint64 TotalPageFile;uint64 AvailPageFile;" ) ),
                    Variant.Create( "uint64 TotalVirtual;uint64 AvailVirtual;uint64 AvailExtendedVirtual" ) );
            return Variant.Create( (object) null );
        }

        public Variant f__SendMessage(
            Variant v_hWnd,
            Variant v_iMsg,
            Variant v_wParam = null,
            Variant v_lParam = null,
            Variant v_iReturn = null,
            Variant v_wParamType = null,
            Variant v_lParamType = null,
            Variant v_sReturnType = null ) {
            Variant v_aResult = Variant.Create( (object) null );
            if ( v_sReturnType == null ) {
                v_sReturnType = Variant.Create( "lresult" );
            }
            if ( v_lParamType == null ) {
                v_lParamType = Variant.Create( "lparam" );
            }
            if ( v_wParamType == null ) {
                v_wParamType = Variant.Create( "wparam" );
            }
            if ( v_iReturn == null ) {
                v_iReturn = Variant.Create( 0 );
            }
            if ( v_lParam == null ) {
                v_lParam = Variant.Create( 0 );
            }
            if ( v_wParam == null ) {
                v_wParam = Variant.Create( 0 );
            }
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), v_sReturnType, Variant.Create( "SendMessageW" ), Variant.Create( "hwnd" ), v_hWnd, Variant.Create( "uint" ),
                v_iMsg, v_wParamType, v_wParam, v_lParamType, v_lParam );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( "" ) );
            }

            if ( _functions.AND( v_iReturn >= Variant.Create( 0 ), v_iReturn <= Variant.Create( 4 ) ) ) {
                return v_aResult[v_iReturn];
            }

            return v_aResult;
            return Variant.Create( (object) null );
        }

        public Variant f__SendMessageA(
            Variant v_hWnd,
            Variant v_iMsg,
            Variant v_wParam = null,
            Variant v_lParam = null,
            Variant v_iReturn = null,
            Variant v_wParamType = null,
            Variant v_lParamType = null,
            Variant v_sReturnType = null ) {
            Variant v_aResult = Variant.Create( (object) null );
            if ( v_sReturnType == null ) {
                v_sReturnType = Variant.Create( "lresult" );
            }
            if ( v_lParamType == null ) {
                v_lParamType = Variant.Create( "lparam" );
            }
            if ( v_wParamType == null ) {
                v_wParamType = Variant.Create( "wparam" );
            }
            if ( v_iReturn == null ) {
                v_iReturn = Variant.Create( 0 );
            }
            if ( v_lParam == null ) {
                v_lParam = Variant.Create( 0 );
            }
            if ( v_wParam == null ) {
                v_wParam = Variant.Create( 0 );
            }
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), v_sReturnType, Variant.Create( "SendMessageA" ), Variant.Create( "hwnd" ), v_hWnd, Variant.Create( "uint" ),
                v_iMsg, v_wParamType, v_wParam, v_lParamType, v_lParam );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( "" ) );
            }

            if ( _functions.AND( v_iReturn >= Variant.Create( 0 ), v_iReturn <= Variant.Create( 4 ) ) ) {
                return v_aResult[v_iReturn];
            }

            return v_aResult;
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetLastError( Variant v_iError = null, Variant v_iExtended = null ) {
            Variant v_aResult = Variant.Create( (object) null );
            if ( v_iExtended == null ) {
                v_iExtended = _context.Extended;
            }
            if ( v_iError == null ) {
                v_iError = _context.Error;
            }
            v_aResult = _functions.DllCall( Variant.Create( "kernel32.dll" ), Variant.Create( "dword" ), Variant.Create( "GetLastError" ) );
            return _functions.SetError( v_iError, v_iExtended, v_aResult[Variant.Create( 0 )] );
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_SetLastError( Variant v_iErrorCode, Variant v_iError = null, Variant v_iExtended = null ) {
            if ( v_iExtended == null ) {
                v_iExtended = _context.Extended;
            }
            if ( v_iError == null ) {
                v_iError = _context.Error;
            }
            _functions.DllCall(
                Variant.Create( "kernel32.dll" ), Variant.Create( "none" ), Variant.Create( "SetLastError" ), Variant.Create( "dword" ), v_iErrorCode );
            return _functions.SetError( v_iError, v_iExtended, Variant.Create( (object) null ) );
            return Variant.Create( (object) null );
        }

        public Variant f__Security__AdjustTokenPrivileges(
            Variant v_hToken,
            Variant v_bDisableAll,
            Variant v_pNewState,
            Variant v_iBufferLen,
            Variant v_pPrevState = null,
            Variant v_pRequired = null ) {
            Variant v_aCall = Variant.Create( (object) null );
            if ( v_pRequired == null ) {
                v_pRequired = Variant.Create( 0 );
            }
            if ( v_pPrevState == null ) {
                v_pPrevState = Variant.Create( 0 );
            }
            v_aCall = _functions.DllCall(
                Variant.Create( "advapi32.dll" ), Variant.Create( "bool" ), Variant.Create( "AdjustTokenPrivileges" ), Variant.Create( "handle" ), v_hToken,
                Variant.Create( "bool" ), v_bDisableAll, Variant.Create( "struct*" ), v_pNewState, Variant.Create( "dword" ), v_iBufferLen,
                Variant.Create( "struct*" ), v_pPrevState, Variant.Create( "struct*" ), v_pRequired );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return _functions.NOT( v_aCall[Variant.Create( 0 )] == Variant.Create( 0 ) );
            return Variant.Create( (object) null );
        }

        public Variant f__Security__CreateProcessWithToken(
            Variant v_hToken,
            Variant v_iLogonFlags,
            Variant v_sCommandLine,
            Variant v_iCreationFlags,
            Variant v_sCurDir,
            Variant v_tSTARTUPINFO,
            Variant v_tPROCESS_INFORMATION ) {
            Variant v_aCall = Variant.Create( (object) null );
            v_aCall = _functions.DllCall(
                Variant.Create( "advapi32.dll" ), Variant.Create( "bool" ), Variant.Create( "CreateProcessWithTokenW" ), Variant.Create( "handle" ), v_hToken,
                Variant.Create( "dword" ), v_iLogonFlags, Variant.Create( "ptr" ), Variant.Create( 0 ), Variant.Create( "wstr" ), v_sCommandLine,
                Variant.Create( "dword" ), v_iCreationFlags, Variant.Create( "struct*" ), Variant.Create( 0 ), Variant.Create( "wstr" ), v_sCurDir,
                Variant.Create( "struct*" ), v_tSTARTUPINFO, Variant.Create( "struct*" ), v_tPROCESS_INFORMATION );
            if ( _functions.OR( _context.Error, _functions.NOT( v_aCall[Variant.Create( 0 )] ) ) ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return Variant.Create( true );
            return Variant.Create( (object) null );
        }

        public Variant f__Security__DuplicateTokenEx( Variant v_hExistingToken, Variant v_iDesiredAccess, Variant v_iImpersonationLevel, Variant v_iTokenType ) {
            Variant v_aCall = Variant.Create( (object) null );
            v_aCall = _functions.DllCall(
                Variant.Create( "advapi32.dll" ), Variant.Create( "bool" ), Variant.Create( "DuplicateTokenEx" ), Variant.Create( "handle" ), v_hExistingToken,
                Variant.Create( "dword" ), v_iDesiredAccess, Variant.Create( "struct*" ), Variant.Create( 0 ), Variant.Create( "int" ), v_iImpersonationLevel,
                Variant.Create( "int" ), v_iTokenType, Variant.Create( "handle*" ), Variant.Create( 0 ) );
            if ( _functions.OR( _context.Error, _functions.NOT( v_aCall[Variant.Create( 0 )] ) ) ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aCall[Variant.Create( 6 )];
            return Variant.Create( (object) null );
        }

        public Variant f__Security__GetAccountSid( Variant v_sAccount, Variant v_sSystem = null ) {
            Variant v_aAcct = Variant.Create( (object) null );
            if ( v_sSystem == null ) {
                v_sSystem = Variant.Create( "" );
            }
            v_aAcct = f__Security__LookupAccountName( v_sAccount, v_sSystem );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            if ( _functions.IsArray( v_aAcct ) ) {
                return f__Security__StringSidToSid( v_aAcct[Variant.Create( 0 )] );
            }

            return Variant.Create( "" );
            return Variant.Create( (object) null );
        }

        public Variant f__Security__GetLengthSid( Variant v_pSID ) {
            Variant v_aCall = Variant.Create( (object) null );
            if ( _functions.NOT( f__Security__IsValidSid( v_pSID ) ) ) {
                return _functions.SetError( _context.Error+Variant.Create( 10 ), _context.Extended, Variant.Create( 0 ) );
            }

            v_aCall = _functions.DllCall(
                Variant.Create( "advapi32.dll" ), Variant.Create( "dword" ), Variant.Create( "GetLengthSid" ), Variant.Create( "struct*" ), v_pSID );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aCall[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__Security__GetTokenInformation( Variant v_hToken, Variant v_iClass ) {
            Variant v_aCall = Variant.Create( (object) null );
            Variant v_iLen = Variant.Create( (object) null );
            Variant v_tBuffer = Variant.Create( (object) null );
            v_aCall = _functions.DllCall(
                Variant.Create( "advapi32.dll" ), Variant.Create( "bool" ), Variant.Create( "GetTokenInformation" ), Variant.Create( "handle" ), v_hToken,
                Variant.Create( "int" ), v_iClass, Variant.Create( "struct*" ), Variant.Create( 0 ), Variant.Create( "dword" ), Variant.Create( 0 ),
                Variant.Create( "dword*" ), Variant.Create( 0 ) );
            if ( _functions.OR( _context.Error, _functions.NOT( v_aCall[Variant.Create( 5 )] ) ) ) {
                return _functions.SetError( _context.Error+Variant.Create( 10 ), _context.Extended, Variant.Create( 0 ) );
            }

            v_iLen = v_aCall[Variant.Create( 5 )];
            v_tBuffer = _functions.DllStructCreate( _functions.Concat( _functions.Concat( Variant.Create( "byte[" ), v_iLen ), Variant.Create( "]" ) ) );
            v_aCall = _functions.DllCall(
                Variant.Create( "advapi32.dll" ), Variant.Create( "bool" ), Variant.Create( "GetTokenInformation" ), Variant.Create( "handle" ), v_hToken,
                Variant.Create( "int" ), v_iClass, Variant.Create( "struct*" ), v_tBuffer, Variant.Create( "dword" ), _functions.DllStructGetSize( v_tBuffer ),
                Variant.Create( "dword*" ), Variant.Create( 0 ) );
            if ( _functions.OR( _context.Error, _functions.NOT( v_aCall[Variant.Create( 0 )] ) ) ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_tBuffer;
            return Variant.Create( (object) null );
        }

        public Variant f__Security__ImpersonateSelf( Variant v_iLevel = null ) {
            Variant v_aCall = Variant.Create( (object) null );
            if ( v_iLevel == null ) {
                v_iLevel = v_SECURITYIMPERSONATION;
            }
            v_aCall = _functions.DllCall(
                Variant.Create( "advapi32.dll" ), Variant.Create( "bool" ), Variant.Create( "ImpersonateSelf" ), Variant.Create( "int" ), v_iLevel );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return _functions.NOT( v_aCall[Variant.Create( 0 )] == Variant.Create( 0 ) );
            return Variant.Create( (object) null );
        }

        public Variant f__Security__IsValidSid( Variant v_pSID ) {
            Variant v_aCall = Variant.Create( (object) null );
            v_aCall = _functions.DllCall(
                Variant.Create( "advapi32.dll" ), Variant.Create( "bool" ), Variant.Create( "IsValidSid" ), Variant.Create( "struct*" ), v_pSID );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return _functions.NOT( v_aCall[Variant.Create( 0 )] == Variant.Create( 0 ) );
            return Variant.Create( (object) null );
        }

        public Variant f__Security__LookupAccountName( Variant v_sAccount, Variant v_sSystem = null ) {
            Variant v_tData = Variant.Create( (object) null );
            Variant v_aCall = Variant.Create( (object) null );
            Variant v_aAcct = Variant.Create( (object) null );
            if ( v_sSystem == null ) {
                v_sSystem = Variant.Create( "" );
            }
            v_tData = _functions.DllStructCreate( Variant.Create( "byte SID[256]" ) );
            v_aCall = _functions.DllCall(
                Variant.Create( "advapi32.dll" ), Variant.Create( "bool" ), Variant.Create( "LookupAccountNameW" ), Variant.Create( "wstr" ), v_sSystem,
                Variant.Create( "wstr" ), v_sAccount, Variant.Create( "struct*" ), v_tData, Variant.Create( "dword*" ), _functions.DllStructGetSize( v_tData ),
                Variant.Create( "wstr" ), Variant.Create( "" ), Variant.Create( "dword*" ), _functions.DllStructGetSize( v_tData ), Variant.Create( "int*" ),
                Variant.Create( 0 ) );
            if ( _functions.OR( _context.Error, _functions.NOT( v_aCall[Variant.Create( 0 )] ) ) ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            v_aAcct = Variant.CreateArray( new Variant[Variant.Create( 3 )] );
            v_aAcct[Variant.Create( 0 )] = f__Security__SidToStringSid( _functions.DllStructGetPtr( v_tData, Variant.Create( "SID" ) ) );
            v_aAcct[Variant.Create( 1 )] = v_aCall[Variant.Create( 5 )];
            v_aAcct[Variant.Create( 2 )] = v_aCall[Variant.Create( 7 )];
            return v_aAcct;
            return Variant.Create( (object) null );
        }

        public Variant f__Security__LookupAccountSid( Variant v_vSID, Variant v_sSystem = null ) {
            Variant v_pSID = Variant.Create( (object) null );
            Variant v_aAcct = Variant.Create( (object) null );
            Variant v_sTypeSystem = Variant.Create( (object) null );
            Variant v_aCall = Variant.Create( (object) null );
            if ( v_sSystem == null ) {
                v_sSystem = Variant.Create( "" );
            }
            v_aAcct = Variant.CreateArray( new Variant[Variant.Create( 3 )] );
            if ( _functions.IsString( v_vSID ) ) {
                v_pSID = f__Security__StringSidToSid( v_vSID );
            }
            else {
                v_pSID = v_vSID;
            }

            if ( _functions.NOT( f__Security__IsValidSid( v_pSID ) ) ) {
                return _functions.SetError( _context.Error+Variant.Create( 10 ), _context.Extended, Variant.Create( 0 ) );
            }

            v_sTypeSystem = Variant.Create( "ptr" );
            if ( v_sSystem ) {
                v_sTypeSystem = Variant.Create( "wstr" );
            }

            v_aCall = _functions.DllCall(
                Variant.Create( "advapi32.dll" ), Variant.Create( "bool" ), Variant.Create( "LookupAccountSidW" ), v_sTypeSystem, v_sSystem,
                Variant.Create( "struct*" ), v_pSID, Variant.Create( "wstr" ), Variant.Create( "" ), Variant.Create( "dword*" ), Variant.Create( 65536 ),
                Variant.Create( "wstr" ), Variant.Create( "" ), Variant.Create( "dword*" ), Variant.Create( 65536 ), Variant.Create( "int*" ),
                Variant.Create( 0 ) );
            if ( _functions.OR( _context.Error, _functions.NOT( v_aCall[Variant.Create( 0 )] ) ) ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            v_aAcct = Variant.CreateArray( new Variant[Variant.Create( 3 )] );
            v_aAcct[Variant.Create( 0 )] = v_aCall[Variant.Create( 3 )];
            v_aAcct[Variant.Create( 1 )] = v_aCall[Variant.Create( 5 )];
            v_aAcct[Variant.Create( 2 )] = v_aCall[Variant.Create( 7 )];
            return v_aAcct;
            return Variant.Create( (object) null );
        }

        public Variant f__Security__LookupPrivilegeValue( Variant v_sSystem, Variant v_sName ) {
            Variant v_aCall = Variant.Create( (object) null );
            v_aCall = _functions.DllCall(
                Variant.Create( "advapi32.dll" ), Variant.Create( "bool" ), Variant.Create( "LookupPrivilegeValueW" ), Variant.Create( "wstr" ), v_sSystem,
                Variant.Create( "wstr" ), v_sName, Variant.Create( "int64*" ), Variant.Create( 0 ) );
            if ( _functions.OR( _context.Error, _functions.NOT( v_aCall[Variant.Create( 0 )] ) ) ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aCall[Variant.Create( 3 )];
            return Variant.Create( (object) null );
        }

        public Variant f__Security__OpenProcessToken( Variant v_hProcess, Variant v_iAccess ) {
            Variant v_aCall = Variant.Create( (object) null );
            v_aCall = _functions.DllCall(
                Variant.Create( "advapi32.dll" ), Variant.Create( "bool" ), Variant.Create( "OpenProcessToken" ), Variant.Create( "handle" ), v_hProcess,
                Variant.Create( "dword" ), v_iAccess, Variant.Create( "handle*" ), Variant.Create( 0 ) );
            if ( _functions.OR( _context.Error, _functions.NOT( v_aCall[Variant.Create( 0 )] ) ) ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aCall[Variant.Create( 3 )];
            return Variant.Create( (object) null );
        }

        public Variant f__Security__OpenThreadToken( Variant v_iAccess, Variant v_hThread = null, Variant v_bOpenAsSelf = null ) {
            Variant v_aResult = Variant.Create( (object) null );
            Variant v_aCall = Variant.Create( (object) null );
            if ( v_bOpenAsSelf == null ) {
                v_bOpenAsSelf = Variant.Create( false );
            }
            if ( v_hThread == null ) {
                v_hThread = Variant.Create( 0 );
            }
            if ( v_hThread == Variant.Create( 0 ) ) {
                v_aResult = _functions.DllCall( Variant.Create( "kernel32.dll" ), Variant.Create( "handle" ), Variant.Create( "GetCurrentThread" ) );
                if ( _context.Error ) {
                    return _functions.SetError( _context.Error+Variant.Create( 10 ), _context.Extended, Variant.Create( 0 ) );
                }

                v_hThread = v_aResult[Variant.Create( 0 )];
            }

            v_aCall = _functions.DllCall(
                Variant.Create( "advapi32.dll" ), Variant.Create( "bool" ), Variant.Create( "OpenThreadToken" ), Variant.Create( "handle" ), v_hThread,
                Variant.Create( "dword" ), v_iAccess, Variant.Create( "bool" ), v_bOpenAsSelf, Variant.Create( "handle*" ), Variant.Create( 0 ) );
            if ( _functions.OR( _context.Error, _functions.NOT( v_aCall[Variant.Create( 0 )] ) ) ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aCall[Variant.Create( 4 )];
            return Variant.Create( (object) null );
        }

        public Variant f__Security__OpenThreadTokenEx( Variant v_iAccess, Variant v_hThread = null, Variant v_bOpenAsSelf = null ) {
            Variant v_hToken = Variant.Create( (object) null );
            Variant v_ERROR_NO_TOKEN = Variant.Create( (object) null );
            if ( v_bOpenAsSelf == null ) {
                v_bOpenAsSelf = Variant.Create( false );
            }
            if ( v_hThread == null ) {
                v_hThread = Variant.Create( 0 );
            }
            v_hToken = f__Security__OpenThreadToken( v_iAccess, v_hThread, v_bOpenAsSelf );
            if ( v_hToken == Variant.Create( 0 ) ) {
                v_ERROR_NO_TOKEN = Variant.Create( 1008 );
                if ( f__WinAPI_GetLastError() != v_ERROR_NO_TOKEN ) {
                    return _functions.SetError( Variant.Create( 20 ), f__WinAPI_GetLastError(), Variant.Create( 0 ) );
                }

                if ( _functions.NOT( f__Security__ImpersonateSelf() ) ) {
                    return _functions.SetError( _context.Error+Variant.Create( 10 ), f__WinAPI_GetLastError(), Variant.Create( 0 ) );
                }

                v_hToken = f__Security__OpenThreadToken( v_iAccess, v_hThread, v_bOpenAsSelf );
                if ( v_hToken == Variant.Create( 0 ) ) {
                    return _functions.SetError( _context.Error, f__WinAPI_GetLastError(), Variant.Create( 0 ) );
                }
            }

            return v_hToken;
            return Variant.Create( (object) null );
        }

        public Variant f__Security__SetPrivilege( Variant v_hToken, Variant v_sPrivilege, Variant v_bEnable ) {
            Variant v_iLUID = Variant.Create( (object) null );
            Variant v_tCurrState = Variant.Create( (object) null );
            Variant v_iCurrState = Variant.Create( (object) null );
            Variant v_tPrevState = Variant.Create( (object) null );
            Variant v_iPrevState = Variant.Create( (object) null );
            Variant v_tRequired = Variant.Create( (object) null );
            Variant v_iAttributes = Variant.Create( (object) null );
            v_iLUID = f__Security__LookupPrivilegeValue( Variant.Create( "" ), v_sPrivilege );
            if ( v_iLUID == Variant.Create( 0 ) ) {
                return _functions.SetError( _context.Error+Variant.Create( 10 ), _context.Extended, Variant.Create( false ) );
            }

            v_tagTOKEN_PRIVILEGES = Variant.Create( "dword Count;align 4;int64 LUID;dword Attributes" );
            v_tCurrState = _functions.DllStructCreate( v_tagTOKEN_PRIVILEGES );
            v_iCurrState = _functions.DllStructGetSize( v_tCurrState );
            v_tPrevState = _functions.DllStructCreate( v_tagTOKEN_PRIVILEGES );
            v_iPrevState = _functions.DllStructGetSize( v_tPrevState );
            v_tRequired = _functions.DllStructCreate( Variant.Create( "int Data" ) );
            _functions.DllStructSetData( v_tCurrState, Variant.Create( "Count" ), Variant.Create( 1 ) );
            _functions.DllStructSetData( v_tCurrState, Variant.Create( "LUID" ), v_iLUID );
            if ( _functions.NOT(
                f__Security__AdjustTokenPrivileges( v_hToken, Variant.Create( false ), v_tCurrState, v_iCurrState, v_tPrevState, v_tRequired ) ) ) {
                return _functions.SetError( Variant.Create( 2 ), _context.Error, Variant.Create( false ) );
            }

            _functions.DllStructSetData( v_tPrevState, Variant.Create( "Count" ), Variant.Create( 1 ) );
            _functions.DllStructSetData( v_tPrevState, Variant.Create( "LUID" ), v_iLUID );
            v_iAttributes = _functions.DllStructGetData( v_tPrevState, Variant.Create( "Attributes" ) );
            if ( v_bEnable ) {
                v_iAttributes = _functions.BitOR( v_iAttributes, v_SE_PRIVILEGE_ENABLED );
            }
            else {
                v_iAttributes = _functions.BitAND( v_iAttributes, _functions.BitNOT( v_SE_PRIVILEGE_ENABLED ) );
            }

            _functions.DllStructSetData( v_tPrevState, Variant.Create( "Attributes" ), v_iAttributes );
            if ( _functions.NOT(
                f__Security__AdjustTokenPrivileges( v_hToken, Variant.Create( false ), v_tPrevState, v_iPrevState, v_tCurrState, v_tRequired ) ) ) {
                return _functions.SetError( Variant.Create( 3 ), _context.Error, Variant.Create( false ) );
            }

            return Variant.Create( true );
            return Variant.Create( (object) null );
        }

        public Variant f__Security__SetTokenInformation(
            Variant v_hToken,
            Variant v_iTokenInformation,
            Variant v_vTokenInformation,
            Variant v_iTokenInformationLength ) {
            Variant v_aCall = Variant.Create( (object) null );
            v_aCall = _functions.DllCall(
                Variant.Create( "advapi32.dll" ), Variant.Create( "bool" ), Variant.Create( "SetTokenInformation" ), Variant.Create( "handle" ), v_hToken,
                Variant.Create( "int" ), v_iTokenInformation, Variant.Create( "struct*" ), v_vTokenInformation, Variant.Create( "dword" ),
                v_iTokenInformationLength );
            if ( _functions.OR( _context.Error, _functions.NOT( v_aCall[Variant.Create( 0 )] ) ) ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return Variant.Create( true );
            return Variant.Create( (object) null );
        }

        public Variant f__Security__SidToStringSid( Variant v_pSID ) {
            Variant v_aCall = Variant.Create( (object) null );
            Variant v_pStringSid = Variant.Create( (object) null );
            Variant v_aLen = Variant.Create( (object) null );
            Variant v_sSID = Variant.Create( (object) null );
            if ( _functions.NOT( f__Security__IsValidSid( v_pSID ) ) ) {
                return _functions.SetError( _context.Error+Variant.Create( 10 ), Variant.Create( 0 ), Variant.Create( "" ) );
            }

            v_aCall = _functions.DllCall(
                Variant.Create( "advapi32.dll" ), Variant.Create( "bool" ), Variant.Create( "ConvertSidToStringSidW" ), Variant.Create( "struct*" ), v_pSID,
                Variant.Create( "ptr*" ), Variant.Create( 0 ) );
            if ( _functions.OR( _context.Error, _functions.NOT( v_aCall[Variant.Create( 0 )] ) ) ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( "" ) );
            }

            v_pStringSid = v_aCall[Variant.Create( 2 )];
            v_aLen = _functions.DllCall(
                Variant.Create( "kernel32.dll" ), Variant.Create( "int" ), Variant.Create( "lstrlenW" ), Variant.Create( "struct*" ), v_pStringSid );
            v_sSID =
                _functions.DllStructGetData(
                    _functions.DllStructCreate(
                        _functions.Concat(
                            _functions.Concat( Variant.Create( "wchar Text[" ), v_aLen[Variant.Create( 0 )]+Variant.Create( 1 ) ), Variant.Create( "]" ) ),
                        v_pStringSid ), Variant.Create( "Text" ) );
            _functions.DllCall(
                Variant.Create( "kernel32.dll" ), Variant.Create( "handle" ), Variant.Create( "LocalFree" ), Variant.Create( "handle" ), v_pStringSid );
            return v_sSID;
            return Variant.Create( (object) null );
        }

        public Variant f__Security__SidTypeStr( Variant v_iType ) {
            if ( _functions.Equal( v_iType, v_SIDTYPEUSER ) ) {
                return Variant.Create( "User" );
            }
            if ( _functions.Equal( v_iType, v_SIDTYPEGROUP ) ) {
                return Variant.Create( "Group" );
            }
            if ( _functions.Equal( v_iType, v_SIDTYPEDOMAIN ) ) {
                return Variant.Create( "Domain" );
            }
            if ( _functions.Equal( v_iType, v_SIDTYPEALIAS ) ) {
                return Variant.Create( "Alias" );
            }
            if ( _functions.Equal( v_iType, v_SIDTYPEWELLKNOWNGROUP ) ) {
                return Variant.Create( "Well Known Group" );
            }
            if ( _functions.Equal( v_iType, v_SIDTYPEDELETEDACCOUNT ) ) {
                return Variant.Create( "Deleted Account" );
            }
            if ( _functions.Equal( v_iType, v_SIDTYPEINVALID ) ) {
                return Variant.Create( "Invalid" );
            }
            if ( _functions.Equal( v_iType, v_SIDTYPEUNKNOWN ) ) {
                return Variant.Create( "Unknown Type" );
            }
            if ( _functions.Equal( v_iType, v_SIDTYPECOMPUTER ) ) {
                return Variant.Create( "Computer" );
            }
            if ( _functions.Equal( v_iType, v_SIDTYPELABEL ) ) {
                return Variant.Create( "A mandatory integrity label SID" );
            }
            return Variant.Create( "Unknown SID Type" );

            return Variant.Create( (object) null );
        }

        public Variant f__Security__StringSidToSid( Variant v_sSID ) {
            Variant v_aCall = Variant.Create( (object) null );
            Variant v_pSID = Variant.Create( (object) null );
            Variant v_tBuffer = Variant.Create( (object) null );
            Variant v_tSID = Variant.Create( (object) null );
            v_aCall = _functions.DllCall(
                Variant.Create( "advapi32.dll" ), Variant.Create( "bool" ), Variant.Create( "ConvertStringSidToSidW" ), Variant.Create( "wstr" ), v_sSID,
                Variant.Create( "ptr*" ), Variant.Create( 0 ) );
            if ( _functions.OR( _context.Error, _functions.NOT( v_aCall[Variant.Create( 0 )] ) ) ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            v_pSID = v_aCall[Variant.Create( 2 )];
            v_tBuffer =
                _functions.DllStructCreate(
                    _functions.Concat( _functions.Concat( Variant.Create( "byte Data[" ), f__Security__GetLengthSid( v_pSID ) ), Variant.Create( "]" ) ), v_pSID );
            v_tSID =
                _functions.DllStructCreate(
                    _functions.Concat( _functions.Concat( Variant.Create( "byte Data[" ), _functions.DllStructGetSize( v_tBuffer ) ), Variant.Create( "]" ) ) );
            _functions.DllStructSetData( v_tSID, Variant.Create( "Data" ), _functions.DllStructGetData( v_tBuffer, Variant.Create( "Data" ) ) );
            _functions.DllCall(
                Variant.Create( "kernel32.dll" ), Variant.Create( "handle" ), Variant.Create( "LocalFree" ), Variant.Create( "handle" ), v_pSID );
            return v_tSID;
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_AttachConsole( Variant v_iPID = null ) {
            Variant v_aResult = Variant.Create( (object) null );
            if ( v_iPID == null ) {
                v_iPID = Variant.Create( -1 );
            }
            v_aResult = _functions.DllCall(
                Variant.Create( "kernel32.dll" ), Variant.Create( "bool" ), Variant.Create( "AttachConsole" ), Variant.Create( "dword" ), v_iPID );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_AttachThreadInput( Variant v_iAttach, Variant v_iAttachTo, Variant v_bAttach ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "bool" ), Variant.Create( "AttachThreadInput" ), Variant.Create( "dword" ), v_iAttach,
                Variant.Create( "dword" ), v_iAttachTo, Variant.Create( "bool" ), v_bAttach );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_Beep( Variant v_iFreq = null, Variant v_iDuration = null ) {
            Variant v_aResult = Variant.Create( (object) null );
            if ( v_iDuration == null ) {
                v_iDuration = Variant.Create( 1000 );
            }
            if ( v_iFreq == null ) {
                v_iFreq = Variant.Create( 500 );
            }
            v_aResult = _functions.DllCall(
                Variant.Create( "kernel32.dll" ), Variant.Create( "bool" ), Variant.Create( "Beep" ), Variant.Create( "dword" ), v_iFreq,
                Variant.Create( "dword" ), v_iDuration );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_BitBlt(
            Variant v_hDestDC,
            Variant v_iXDest,
            Variant v_iYDest,
            Variant v_iWidth,
            Variant v_iHeight,
            Variant v_hSrcDC,
            Variant v_iXSrc,
            Variant v_iYSrc,
            Variant v_iROP ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "gdi32.dll" ), Variant.Create( "bool" ), Variant.Create( "BitBlt" ), Variant.Create( "handle" ), v_hDestDC,
                Variant.Create( "int" ), v_iXDest, Variant.Create( "int" ), v_iYDest, Variant.Create( "int" ), v_iWidth, Variant.Create( "int" ), v_iHeight,
                Variant.Create( "handle" ), v_hSrcDC, Variant.Create( "int" ), v_iXSrc, Variant.Create( "int" ), v_iYSrc, Variant.Create( "dword" ), v_iROP );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_CallNextHookEx( Variant v_hHk, Variant v_iCode, Variant v_wParam, Variant v_lParam ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "lresult" ), Variant.Create( "CallNextHookEx" ), Variant.Create( "handle" ), v_hHk,
                Variant.Create( "int" ), v_iCode, Variant.Create( "wparam" ), v_wParam, Variant.Create( "lparam" ), v_lParam );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( -1 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_CallWindowProc( Variant v_pPrevWndFunc, Variant v_hWnd, Variant v_iMsg, Variant v_wParam, Variant v_lParam ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "lresult" ), Variant.Create( "CallWindowProc" ), Variant.Create( "ptr" ), v_pPrevWndFunc,
                Variant.Create( "hwnd" ), v_hWnd, Variant.Create( "uint" ), v_iMsg, Variant.Create( "wparam" ), v_wParam, Variant.Create( "lparam" ), v_lParam );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( -1 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_ClientToScreen( Variant v_hWnd, ref Variant v_tPoint ) {
            Variant v_aRet = Variant.Create( (object) null );
            v_aRet = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "bool" ), Variant.Create( "ClientToScreen" ), Variant.Create( "hwnd" ), v_hWnd,
                Variant.Create( "struct*" ), v_tPoint );
            if ( _functions.OR( _context.Error, _functions.NOT( v_aRet[Variant.Create( 0 )] ) ) ) {
                return _functions.SetError( _context.Error+Variant.Create( 10 ), _context.Extended, Variant.Create( 0 ) );
            }

            return v_tPoint;
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_CloseHandle( Variant v_hObject ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "kernel32.dll" ), Variant.Create( "bool" ), Variant.Create( "CloseHandle" ), Variant.Create( "handle" ), v_hObject );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_CombineRgn( Variant v_hRgnDest, Variant v_hRgnSrc1, Variant v_hRgnSrc2, Variant v_iCombineMode ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "gdi32.dll" ), Variant.Create( "int" ), Variant.Create( "CombineRgn" ), Variant.Create( "handle" ), v_hRgnDest,
                Variant.Create( "handle" ), v_hRgnSrc1, Variant.Create( "handle" ), v_hRgnSrc2, Variant.Create( "int" ), v_iCombineMode );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_CommDlgExtendedError() {
            Variant v_CDERR_DIALOGFAILURE = Variant.Create( (object) null );
            Variant v_CDERR_FINDRESFAILURE = Variant.Create( (object) null );
            Variant v_CDERR_INITIALIZATION = Variant.Create( (object) null );
            Variant v_CDERR_LOADRESFAILURE = Variant.Create( (object) null );
            Variant v_CDERR_LOADSTRFAILURE = Variant.Create( (object) null );
            Variant v_CDERR_LOCKRESFAILURE = Variant.Create( (object) null );
            Variant v_CDERR_MEMALLOCFAILURE = Variant.Create( (object) null );
            Variant v_CDERR_MEMLOCKFAILURE = Variant.Create( (object) null );
            Variant v_CDERR_NOHINSTANCE = Variant.Create( (object) null );
            Variant v_CDERR_NOHOOK = Variant.Create( (object) null );
            Variant v_CDERR_NOTEMPLATE = Variant.Create( (object) null );
            Variant v_CDERR_REGISTERMSGFAIL = Variant.Create( (object) null );
            Variant v_CDERR_STRUCTSIZE = Variant.Create( (object) null );
            Variant v_FNERR_BUFFERTOOSMALL = Variant.Create( (object) null );
            Variant v_FNERR_INVALIDFILENAME = Variant.Create( (object) null );
            Variant v_FNERR_SUBCLASSFAILURE = Variant.Create( (object) null );
            Variant v_aResult = Variant.Create( (object) null );
            v_CDERR_DIALOGFAILURE = Variant.Create( 65535 );
            v_CDERR_FINDRESFAILURE = Variant.Create( 6 );
            v_CDERR_INITIALIZATION = Variant.Create( 2 );
            v_CDERR_LOADRESFAILURE = Variant.Create( 7 );
            v_CDERR_LOADSTRFAILURE = Variant.Create( 5 );
            v_CDERR_LOCKRESFAILURE = Variant.Create( 8 );
            v_CDERR_MEMALLOCFAILURE = Variant.Create( 9 );
            v_CDERR_MEMLOCKFAILURE = Variant.Create( 10 );
            v_CDERR_NOHINSTANCE = Variant.Create( 4 );
            v_CDERR_NOHOOK = Variant.Create( 11 );
            v_CDERR_NOTEMPLATE = Variant.Create( 3 );
            v_CDERR_REGISTERMSGFAIL = Variant.Create( 12 );
            v_CDERR_STRUCTSIZE = Variant.Create( 1 );
            v_FNERR_BUFFERTOOSMALL = Variant.Create( 12291 );
            v_FNERR_INVALIDFILENAME = Variant.Create( 12290 );
            v_FNERR_SUBCLASSFAILURE = Variant.Create( 12289 );
            v_aResult = _functions.DllCall( Variant.Create( "comdlg32.dll" ), Variant.Create( "dword" ), Variant.Create( "CommDlgExtendedError" ) );
            if ( _functions.NOT( _context.Error ) ) {
                if ( _functions.Equal( v_aResult[Variant.Create( 0 )], v_CDERR_DIALOGFAILURE ) ) {
                    return _functions.SetError(
                        v_aResult[Variant.Create( 0 )], Variant.Create( 0 ),
                        _functions.Concat(
                            _functions.Concat(
                                _functions.Concat(
                                    _functions.Concat( Variant.Create( "The dialog box could not be created." ), _context.LF ),
                                    Variant.Create( "The common dialog box function's call to the DialogBox function failed." ) ), _context.LF ),
                            Variant.Create( "For example, this error occurs if the common dialog box call specifies an invalid window handle." ) ) );
                }
                if ( _functions.Equal( v_aResult[Variant.Create( 0 )], v_CDERR_FINDRESFAILURE ) ) {
                    return _functions.SetError(
                        v_aResult[Variant.Create( 0 )], Variant.Create( 0 ),
                        Variant.Create( "The common dialog box function failed to find a specified resource." ) );
                }
                if ( _functions.Equal( v_aResult[Variant.Create( 0 )], v_CDERR_INITIALIZATION ) ) {
                    return _functions.SetError(
                        v_aResult[Variant.Create( 0 )], Variant.Create( 0 ),
                        _functions.Concat(
                            _functions.Concat( Variant.Create( "The common dialog box function failed during initialization." ), _context.LF ),
                            Variant.Create( "This error often occurs when sufficient memory is not available." ) ) );
                }
                if ( _functions.Equal( v_aResult[Variant.Create( 0 )], v_CDERR_LOADRESFAILURE ) ) {
                    return _functions.SetError(
                        v_aResult[Variant.Create( 0 )], Variant.Create( 0 ),
                        Variant.Create( "The common dialog box function failed to load a specified resource." ) );
                }
                if ( _functions.Equal( v_aResult[Variant.Create( 0 )], v_CDERR_LOADSTRFAILURE ) ) {
                    return _functions.SetError(
                        v_aResult[Variant.Create( 0 )], Variant.Create( 0 ),
                        Variant.Create( "The common dialog box function failed to load a specified string." ) );
                }
                if ( _functions.Equal( v_aResult[Variant.Create( 0 )], v_CDERR_LOCKRESFAILURE ) ) {
                    return _functions.SetError(
                        v_aResult[Variant.Create( 0 )], Variant.Create( 0 ),
                        Variant.Create( "The common dialog box function failed to lock a specified resource." ) );
                }
                if ( _functions.Equal( v_aResult[Variant.Create( 0 )], v_CDERR_MEMALLOCFAILURE ) ) {
                    return _functions.SetError(
                        v_aResult[Variant.Create( 0 )], Variant.Create( 0 ),
                        Variant.Create( "The common dialog box function was unable to allocate memory for internal structures." ) );
                }
                if ( _functions.Equal( v_aResult[Variant.Create( 0 )], v_CDERR_MEMLOCKFAILURE ) ) {
                    return _functions.SetError(
                        v_aResult[Variant.Create( 0 )], Variant.Create( 0 ),
                        Variant.Create( "The common dialog box function was unable to lock the memory associated with a handle." ) );
                }
                if ( _functions.Equal( v_aResult[Variant.Create( 0 )], v_CDERR_NOHINSTANCE ) ) {
                    return _functions.SetError(
                        v_aResult[Variant.Create( 0 )], Variant.Create( 0 ),
                        _functions.Concat(
                            _functions.Concat(
                                Variant.Create(
                                    "The ENABLETEMPLATE flag was set in the Flags member of the initialization structure for the corresponding common dialog box," ),
                                _context.LF ), Variant.Create( "but you failed to provide a corresponding instance handle." ) ) );
                }
                if ( _functions.Equal( v_aResult[Variant.Create( 0 )], v_CDERR_NOHOOK ) ) {
                    return _functions.SetError(
                        v_aResult[Variant.Create( 0 )], Variant.Create( 0 ),
                        _functions.Concat(
                            _functions.Concat(
                                Variant.Create(
                                    "The ENABLEHOOK flag was set in the Flags member of the initialization structure for the corresponding common dialog box," ),
                                _context.LF ), Variant.Create( "but you failed to provide a pointer to a corresponding hook procedure." ) ) );
                }
                if ( _functions.Equal( v_aResult[Variant.Create( 0 )], v_CDERR_NOTEMPLATE ) ) {
                    return _functions.SetError(
                        v_aResult[Variant.Create( 0 )], Variant.Create( 0 ),
                        _functions.Concat(
                            _functions.Concat(
                                Variant.Create(
                                    "The ENABLETEMPLATE flag was set in the Flags member of the initialization structure for the corresponding common dialog box," ),
                                _context.LF ), Variant.Create( "but you failed to provide a corresponding template." ) ) );
                }
                if ( _functions.Equal( v_aResult[Variant.Create( 0 )], v_CDERR_REGISTERMSGFAIL ) ) {
                    return _functions.SetError(
                        v_aResult[Variant.Create( 0 )], Variant.Create( 0 ),
                        Variant.Create( "The RegisterWindowMessage function returned an error code when it was called by the common dialog box function." ) );
                }
                if ( _functions.Equal( v_aResult[Variant.Create( 0 )], v_CDERR_STRUCTSIZE ) ) {
                    return _functions.SetError(
                        v_aResult[Variant.Create( 0 )], Variant.Create( 0 ),
                        Variant.Create( "The lStructSize member of the initialization structure for the corresponding common dialog box is invalid" ) );
                }
                if ( _functions.Equal( v_aResult[Variant.Create( 0 )], v_FNERR_BUFFERTOOSMALL ) ) {
                    return _functions.SetError(
                        v_aResult[Variant.Create( 0 )], Variant.Create( 0 ),
                        _functions.Concat(
                            _functions.Concat(
                                Variant.Create(
                                    "The buffer pointed to by the lpstrFile member of the OPENFILENAME structure is too small for the file name specified by the user." ),
                                _context.LF ),
                            Variant.Create(
                                "The first two bytes of the lpstrFile buffer contain an integer value specifying the size, in TCHARs, required to receive the full name." ) ) );
                }
                if ( _functions.Equal( v_aResult[Variant.Create( 0 )], v_FNERR_INVALIDFILENAME ) ) {
                    return _functions.SetError( v_aResult[Variant.Create( 0 )], Variant.Create( 0 ), Variant.Create( "A file name is invalid." ) );
                }
                if ( _functions.Equal( v_aResult[Variant.Create( 0 )], v_FNERR_SUBCLASSFAILURE ) ) {
                    return _functions.SetError(
                        v_aResult[Variant.Create( 0 )], Variant.Create( 0 ),
                        Variant.Create( "An attempt to subclass a list box failed because sufficient memory was not available." ) );
                }
            }

            return _functions.SetError(
                _context.Error, _context.Extended, _functions.Concat( Variant.Create( "0x" ), _functions.Hex( v_aResult[Variant.Create( 0 )] ) ) );
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_CopyIcon( Variant v_hIcon ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "handle" ), Variant.Create( "CopyIcon" ), Variant.Create( "handle" ), v_hIcon );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_CreateBitmap(
            Variant v_iWidth,
            Variant v_iHeight,
            Variant v_iPlanes = null,
            Variant v_iBitsPerPel = null,
            Variant v_pBits = null ) {
            Variant v_aResult = Variant.Create( (object) null );
            if ( v_pBits == null ) {
                v_pBits = Variant.Create( 0 );
            }
            if ( v_iBitsPerPel == null ) {
                v_iBitsPerPel = Variant.Create( 1 );
            }
            if ( v_iPlanes == null ) {
                v_iPlanes = Variant.Create( 1 );
            }
            v_aResult = _functions.DllCall(
                Variant.Create( "gdi32.dll" ), Variant.Create( "handle" ), Variant.Create( "CreateBitmap" ), Variant.Create( "int" ), v_iWidth,
                Variant.Create( "int" ), v_iHeight, Variant.Create( "uint" ), v_iPlanes, Variant.Create( "uint" ), v_iBitsPerPel, Variant.Create( "ptr" ),
                v_pBits );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_CreateCompatibleBitmap( Variant v_hDC, Variant v_iWidth, Variant v_iHeight ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "gdi32.dll" ), Variant.Create( "handle" ), Variant.Create( "CreateCompatibleBitmap" ), Variant.Create( "handle" ), v_hDC,
                Variant.Create( "int" ), v_iWidth, Variant.Create( "int" ), v_iHeight );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_CreateCompatibleDC( Variant v_hDC ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "gdi32.dll" ), Variant.Create( "handle" ), Variant.Create( "CreateCompatibleDC" ), Variant.Create( "handle" ), v_hDC );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_CreateEvent(
            Variant v_pAttributes = null,
            Variant v_bManualReset = null,
            Variant v_bInitialState = null,
            Variant v_sName = null ) {
            Variant v_sNameType = Variant.Create( (object) null );
            Variant v_aResult = Variant.Create( (object) null );
            if ( v_sName == null ) {
                v_sName = Variant.Create( "" );
            }
            if ( v_bInitialState == null ) {
                v_bInitialState = Variant.Create( true );
            }
            if ( v_bManualReset == null ) {
                v_bManualReset = Variant.Create( true );
            }
            if ( v_pAttributes == null ) {
                v_pAttributes = Variant.Create( 0 );
            }
            v_sNameType = Variant.Create( "wstr" );
            if ( v_sName == Variant.Create( "" ) ) {
                v_sName = Variant.Create( 0 );
                v_sNameType = Variant.Create( "ptr" );
            }

            v_aResult = _functions.DllCall(
                Variant.Create( "kernel32.dll" ), Variant.Create( "handle" ), Variant.Create( "CreateEventW" ), Variant.Create( "ptr" ), v_pAttributes,
                Variant.Create( "bool" ), v_bManualReset, Variant.Create( "bool" ), v_bInitialState, v_sNameType, v_sName );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_CreateFile(
            Variant v_sFileName,
            Variant v_iCreation,
            Variant v_iAccess = null,
            Variant v_iShare = null,
            Variant v_iAttributes = null,
            Variant v_pSecurity = null ) {
            Variant v_iDA = Variant.Create( (object) null );
            Variant v_iSM = Variant.Create( (object) null );
            Variant v_iCD = Variant.Create( (object) null );
            Variant v_iFA = Variant.Create( (object) null );
            Variant v_aResult = Variant.Create( (object) null );
            if ( v_pSecurity == null ) {
                v_pSecurity = Variant.Create( 0 );
            }
            if ( v_iAttributes == null ) {
                v_iAttributes = Variant.Create( 0 );
            }
            if ( v_iShare == null ) {
                v_iShare = Variant.Create( 0 );
            }
            if ( v_iAccess == null ) {
                v_iAccess = Variant.Create( 4 );
            }
            v_iDA = Variant.Create( 0 );
            v_iSM = Variant.Create( 0 );
            v_iCD = Variant.Create( 0 );
            v_iFA = Variant.Create( 0 );
            if ( _functions.BitAND( v_iAccess, Variant.Create( 1 ) ) != Variant.Create( 0 ) ) {
                v_iDA = _functions.BitOR( v_iDA, v_GENERIC_EXECUTE );
            }

            if ( _functions.BitAND( v_iAccess, Variant.Create( 2 ) ) != Variant.Create( 0 ) ) {
                v_iDA = _functions.BitOR( v_iDA, v_GENERIC_READ );
            }

            if ( _functions.BitAND( v_iAccess, Variant.Create( 4 ) ) != Variant.Create( 0 ) ) {
                v_iDA = _functions.BitOR( v_iDA, v_GENERIC_WRITE );
            }

            if ( _functions.BitAND( v_iShare, Variant.Create( 1 ) ) != Variant.Create( 0 ) ) {
                v_iSM = _functions.BitOR( v_iSM, v_FILE_SHARE_DELETE );
            }

            if ( _functions.BitAND( v_iShare, Variant.Create( 2 ) ) != Variant.Create( 0 ) ) {
                v_iSM = _functions.BitOR( v_iSM, v_FILE_SHARE_READ );
            }

            if ( _functions.BitAND( v_iShare, Variant.Create( 4 ) ) != Variant.Create( 0 ) ) {
                v_iSM = _functions.BitOR( v_iSM, v_FILE_SHARE_WRITE );
            }

            if ( _functions.Equal( v_iCreation, Variant.Create( 0 ) ) ) {
                v_iCD = v_CREATE_NEW;
            }
            else if ( _functions.Equal( v_iCreation, Variant.Create( 1 ) ) ) {
                v_iCD = v_CREATE_ALWAYS;
            }
            else if ( _functions.Equal( v_iCreation, Variant.Create( 2 ) ) ) {
                v_iCD = v_OPEN_EXISTING;
            }
            else if ( _functions.Equal( v_iCreation, Variant.Create( 3 ) ) ) {
                v_iCD = v_OPEN_ALWAYS;
            }
            else if ( _functions.Equal( v_iCreation, Variant.Create( 4 ) ) ) {
                v_iCD = v_TRUNCATE_EXISTING;
            }

            if ( _functions.BitAND( v_iAttributes, Variant.Create( 1 ) ) != Variant.Create( 0 ) ) {
                v_iFA = _functions.BitOR( v_iFA, v_FILE_ATTRIBUTE_ARCHIVE );
            }

            if ( _functions.BitAND( v_iAttributes, Variant.Create( 2 ) ) != Variant.Create( 0 ) ) {
                v_iFA = _functions.BitOR( v_iFA, v_FILE_ATTRIBUTE_HIDDEN );
            }

            if ( _functions.BitAND( v_iAttributes, Variant.Create( 4 ) ) != Variant.Create( 0 ) ) {
                v_iFA = _functions.BitOR( v_iFA, v_FILE_ATTRIBUTE_READONLY );
            }

            if ( _functions.BitAND( v_iAttributes, Variant.Create( 8 ) ) != Variant.Create( 0 ) ) {
                v_iFA = _functions.BitOR( v_iFA, v_FILE_ATTRIBUTE_SYSTEM );
            }

            v_aResult = _functions.DllCall(
                Variant.Create( "kernel32.dll" ), Variant.Create( "handle" ), Variant.Create( "CreateFileW" ), Variant.Create( "wstr" ), v_sFileName,
                Variant.Create( "dword" ), v_iDA, Variant.Create( "dword" ), v_iSM, Variant.Create( "ptr" ), v_pSecurity, Variant.Create( "dword" ), v_iCD,
                Variant.Create( "dword" ), v_iFA, Variant.Create( "ptr" ), Variant.Create( 0 ) );
            if ( _functions.OR( _context.Error, v_aResult[Variant.Create( 0 )] == v_INVALID_HANDLE_VALUE ) ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_CreateFont(
            Variant v_iHeight,
            Variant v_iWidth,
            Variant v_iEscape = null,
            Variant v_iOrientn = null,
            Variant v_iWeight = null,
            Variant v_bItalic = null,
            Variant v_bUnderline = null,
            Variant v_bStrikeout = null,
            Variant v_iCharset = null,
            Variant v_iOutputPrec = null,
            Variant v_iClipPrec = null,
            Variant v_iQuality = null,
            Variant v_iPitch = null,
            Variant v_sFace = null ) {
            Variant v_aResult = Variant.Create( (object) null );
            if ( v_sFace == null ) {
                v_sFace = Variant.Create( "Arial" );
            }
            if ( v_iPitch == null ) {
                v_iPitch = Variant.Create( 0 );
            }
            if ( v_iQuality == null ) {
                v_iQuality = v___WINAPICONSTANT_DEFAULT_QUALITY;
            }
            if ( v_iClipPrec == null ) {
                v_iClipPrec = v___WINAPICONSTANT_CLIP_DEFAULT_PRECIS;
            }
            if ( v_iOutputPrec == null ) {
                v_iOutputPrec = v___WINAPICONSTANT_OUT_DEFAULT_PRECIS;
            }
            if ( v_iCharset == null ) {
                v_iCharset = v___WINAPICONSTANT_DEFAULT_CHARSET;
            }
            if ( v_bStrikeout == null ) {
                v_bStrikeout = Variant.Create( false );
            }
            if ( v_bUnderline == null ) {
                v_bUnderline = Variant.Create( false );
            }
            if ( v_bItalic == null ) {
                v_bItalic = Variant.Create( false );
            }
            if ( v_iWeight == null ) {
                v_iWeight = v___WINAPICONSTANT_FW_NORMAL;
            }
            if ( v_iOrientn == null ) {
                v_iOrientn = Variant.Create( 0 );
            }
            if ( v_iEscape == null ) {
                v_iEscape = Variant.Create( 0 );
            }
            v_aResult = _functions.DllCall(
                Variant.Create( "gdi32.dll" ), Variant.Create( "handle" ), Variant.Create( "CreateFontW" ), Variant.Create( "int" ), v_iHeight,
                Variant.Create( "int" ), v_iWidth, Variant.Create( "int" ), v_iEscape, Variant.Create( "int" ), v_iOrientn, Variant.Create( "int" ), v_iWeight,
                Variant.Create( "dword" ), v_bItalic, Variant.Create( "dword" ), v_bUnderline, Variant.Create( "dword" ), v_bStrikeout,
                Variant.Create( "dword" ), v_iCharset, Variant.Create( "dword" ), v_iOutputPrec, Variant.Create( "dword" ), v_iClipPrec,
                Variant.Create( "dword" ), v_iQuality, Variant.Create( "dword" ), v_iPitch, Variant.Create( "wstr" ), v_sFace );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_CreateFontIndirect( Variant v_tLogFont ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "gdi32.dll" ), Variant.Create( "handle" ), Variant.Create( "CreateFontIndirectW" ), Variant.Create( "struct*" ), v_tLogFont );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_CreatePen( Variant v_iPenStyle, Variant v_iWidth, Variant v_nColor ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "gdi32.dll" ), Variant.Create( "handle" ), Variant.Create( "CreatePen" ), Variant.Create( "int" ), v_iPenStyle,
                Variant.Create( "int" ), v_iWidth, Variant.Create( "INT" ), v_nColor );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_CreateProcess(
            Variant v_sAppName,
            Variant v_sCommand,
            Variant v_pSecurity,
            Variant v_pThread,
            Variant v_bInherit,
            Variant v_iFlags,
            Variant v_pEnviron,
            Variant v_sDir,
            Variant v_pStartupInfo,
            Variant v_pProcess ) {
            Variant v_tCommand = Variant.Create( (object) null );
            Variant v_sAppNameType = Variant.Create( (object) null );
            Variant v_sDirType = Variant.Create( (object) null );
            Variant v_aResult = Variant.Create( (object) null );
            v_tCommand = Variant.Create( 0 );
            v_sAppNameType = Variant.Create( "wstr" );
            v_sDirType = Variant.Create( "wstr" );
            if ( v_sAppName == Variant.Create( "" ) ) {
                v_sAppNameType = Variant.Create( "ptr" );
                v_sAppName = Variant.Create( 0 );
            }

            if ( v_sCommand != Variant.Create( "" ) ) {
                v_tCommand =
                    _functions.DllStructCreate(
                        _functions.Concat(
                            _functions.Concat( Variant.Create( "wchar Text[" ), Variant.Create( 260 )+Variant.Create( 1 ) ), Variant.Create( "]" ) ) );
                _functions.DllStructSetData( v_tCommand, Variant.Create( "Text" ), v_sCommand );
            }

            if ( v_sDir == Variant.Create( "" ) ) {
                v_sDirType = Variant.Create( "ptr" );
                v_sDir = Variant.Create( 0 );
            }

            v_aResult = _functions.DllCall(
                Variant.Create( "kernel32.dll" ), Variant.Create( "bool" ), Variant.Create( "CreateProcessW" ), v_sAppNameType, v_sAppName,
                Variant.Create( "struct*" ), v_tCommand, Variant.Create( "ptr" ), v_pSecurity, Variant.Create( "ptr" ), v_pThread, Variant.Create( "bool" ),
                v_bInherit, Variant.Create( "dword" ), v_iFlags, Variant.Create( "ptr" ), v_pEnviron, v_sDirType, v_sDir, Variant.Create( "ptr" ),
                v_pStartupInfo, Variant.Create( "ptr" ), v_pProcess );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_CreateRectRgn( Variant v_iLeftRect, Variant v_iTopRect, Variant v_iRightRect, Variant v_iBottomRect ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "gdi32.dll" ), Variant.Create( "handle" ), Variant.Create( "CreateRectRgn" ), Variant.Create( "int" ), v_iLeftRect,
                Variant.Create( "int" ), v_iTopRect, Variant.Create( "int" ), v_iRightRect, Variant.Create( "int" ), v_iBottomRect );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_CreateRoundRectRgn(
            Variant v_iLeftRect,
            Variant v_iTopRect,
            Variant v_iRightRect,
            Variant v_iBottomRect,
            Variant v_iWidthEllipse,
            Variant v_iHeightEllipse ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "gdi32.dll" ), Variant.Create( "handle" ), Variant.Create( "CreateRoundRectRgn" ), Variant.Create( "int" ), v_iLeftRect,
                Variant.Create( "int" ), v_iTopRect, Variant.Create( "int" ), v_iRightRect, Variant.Create( "int" ), v_iBottomRect, Variant.Create( "int" ),
                v_iWidthEllipse, Variant.Create( "int" ), v_iHeightEllipse );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_CreateSolidBitmap( Variant v_hWnd, Variant v_iColor, Variant v_iWidth, Variant v_iHeight, Variant v_bRGB = null ) {
            Variant v_hDC = Variant.Create( (object) null );
            Variant v_hDestDC = Variant.Create( (object) null );
            Variant v_hBitmap = Variant.Create( (object) null );
            Variant v_hOld = Variant.Create( (object) null );
            Variant v_tRect = Variant.Create( (object) null );
            Variant v_hBrush = Variant.Create( (object) null );
            if ( v_bRGB == null ) {
                v_bRGB = Variant.Create( 1 );
            }
            v_hDC = f__WinAPI_GetDC( v_hWnd );
            v_hDestDC = f__WinAPI_CreateCompatibleDC( v_hDC );
            v_hBitmap = f__WinAPI_CreateCompatibleBitmap( v_hDC, v_iWidth, v_iHeight );
            v_hOld = f__WinAPI_SelectObject( v_hDestDC, v_hBitmap );
            v_tRect = _functions.DllStructCreate( v_tagRECT );
            _functions.DllStructSetData( v_tRect, Variant.Create( 1 ), Variant.Create( 0 ) );
            _functions.DllStructSetData( v_tRect, Variant.Create( 2 ), Variant.Create( 0 ) );
            _functions.DllStructSetData( v_tRect, Variant.Create( 3 ), v_iWidth );
            _functions.DllStructSetData( v_tRect, Variant.Create( 4 ), v_iHeight );
            if ( v_bRGB ) {
                v_iColor = _functions.BitOR(
                    _functions.BitAND( v_iColor, Variant.Create( 65280 ) ),
                    _functions.BitShift( _functions.BitAND( v_iColor, Variant.Create( 255 ) ), Variant.Create( -16 ) ),
                    _functions.BitShift( _functions.BitAND( v_iColor, Variant.Create( 16711680 ) ), Variant.Create( 16 ) ) );
            }

            v_hBrush = f__WinAPI_CreateSolidBrush( v_iColor );
            if ( _functions.NOT( f__WinAPI_FillRect( v_hDestDC, v_tRect, v_hBrush ) ) ) {
                f__WinAPI_DeleteObject( v_hBitmap );
                v_hBitmap = Variant.Create( 0 );
            }

            f__WinAPI_DeleteObject( v_hBrush );
            f__WinAPI_ReleaseDC( v_hWnd, v_hDC );
            f__WinAPI_SelectObject( v_hDestDC, v_hOld );
            f__WinAPI_DeleteDC( v_hDestDC );
            if ( _functions.NOT( v_hBitmap ) ) {
                return _functions.SetError( Variant.Create( 1 ), Variant.Create( 0 ), Variant.Create( 0 ) );
            }

            return v_hBitmap;
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_CreateSolidBrush( Variant v_nColor ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "gdi32.dll" ), Variant.Create( "handle" ), Variant.Create( "CreateSolidBrush" ), Variant.Create( "INT" ), v_nColor );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_CreateWindowEx(
            Variant v_iExStyle,
            Variant v_sClass,
            Variant v_sName,
            Variant v_iStyle,
            Variant v_iX,
            Variant v_iY,
            Variant v_iWidth,
            Variant v_iHeight,
            Variant v_hParent,
            Variant v_hMenu = null,
            Variant v_hInstance = null,
            Variant v_pParam = null ) {
            Variant v_aResult = Variant.Create( (object) null );
            if ( v_pParam == null ) {
                v_pParam = Variant.Create( 0 );
            }
            if ( v_hInstance == null ) {
                v_hInstance = Variant.Create( 0 );
            }
            if ( v_hMenu == null ) {
                v_hMenu = Variant.Create( 0 );
            }
            if ( v_hInstance == Variant.Create( 0 ) ) {
                v_hInstance = f__WinAPI_GetModuleHandle( Variant.Create( "" ) );
            }

            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "hwnd" ), Variant.Create( "CreateWindowExW" ), Variant.Create( "dword" ), v_iExStyle,
                Variant.Create( "wstr" ), v_sClass, Variant.Create( "wstr" ), v_sName, Variant.Create( "dword" ), v_iStyle, Variant.Create( "int" ), v_iX,
                Variant.Create( "int" ), v_iY, Variant.Create( "int" ), v_iWidth, Variant.Create( "int" ), v_iHeight, Variant.Create( "hwnd" ), v_hParent,
                Variant.Create( "handle" ), v_hMenu, Variant.Create( "handle" ), v_hInstance, Variant.Create( "ptr" ), v_pParam );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_DefWindowProc( Variant v_hWnd, Variant v_iMsg, Variant v_iwParam, Variant v_ilParam ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "lresult" ), Variant.Create( "DefWindowProc" ), Variant.Create( "hwnd" ), v_hWnd,
                Variant.Create( "uint" ), v_iMsg, Variant.Create( "wparam" ), v_iwParam, Variant.Create( "lparam" ), v_ilParam );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_DeleteDC( Variant v_hDC ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "gdi32.dll" ), Variant.Create( "bool" ), Variant.Create( "DeleteDC" ), Variant.Create( "handle" ), v_hDC );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_DeleteObject( Variant v_hObject ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "gdi32.dll" ), Variant.Create( "bool" ), Variant.Create( "DeleteObject" ), Variant.Create( "handle" ), v_hObject );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_DestroyIcon( Variant v_hIcon ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "bool" ), Variant.Create( "DestroyIcon" ), Variant.Create( "handle" ), v_hIcon );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_DestroyWindow( Variant v_hWnd ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "bool" ), Variant.Create( "DestroyWindow" ), Variant.Create( "hwnd" ), v_hWnd );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_DrawEdge( Variant v_hDC, Variant v_pRect, Variant v_iEdgeType, Variant v_iFlags ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "bool" ), Variant.Create( "DrawEdge" ), Variant.Create( "handle" ), v_hDC,
                Variant.Create( "ptr" ), v_pRect, Variant.Create( "uint" ), v_iEdgeType, Variant.Create( "uint" ), v_iFlags );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_DrawFrameControl( Variant v_hDC, Variant v_pRect, Variant v_iType, Variant v_iState ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "bool" ), Variant.Create( "DrawFrameControl" ), Variant.Create( "handle" ), v_hDC,
                Variant.Create( "ptr" ), v_pRect, Variant.Create( "uint" ), v_iType, Variant.Create( "uint" ), v_iState );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_DrawIcon( Variant v_hDC, Variant v_iX, Variant v_iY, Variant v_hIcon ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "bool" ), Variant.Create( "DrawIcon" ), Variant.Create( "handle" ), v_hDC,
                Variant.Create( "int" ), v_iX, Variant.Create( "int" ), v_iY, Variant.Create( "handle" ), v_hIcon );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_DrawIconEx(
            Variant v_hDC,
            Variant v_iX,
            Variant v_iY,
            Variant v_hIcon,
            Variant v_iWidth = null,
            Variant v_iHeight = null,
            Variant v_iStep = null,
            Variant v_hBrush = null,
            Variant v_iFlags = null ) {
            Variant v_iOptions = Variant.Create( (object) null );
            Variant v_aResult = Variant.Create( (object) null );
            if ( v_iFlags == null ) {
                v_iFlags = Variant.Create( 3 );
            }
            if ( v_hBrush == null ) {
                v_hBrush = Variant.Create( 0 );
            }
            if ( v_iStep == null ) {
                v_iStep = Variant.Create( 0 );
            }
            if ( v_iHeight == null ) {
                v_iHeight = Variant.Create( 0 );
            }
            if ( v_iWidth == null ) {
                v_iWidth = Variant.Create( 0 );
            }
            if ( _functions.Equal( v_iFlags, Variant.Create( 1 ) ) ) {
                v_iOptions = v_DI_MASK;
            }
            else if ( _functions.Equal( v_iFlags, Variant.Create( 2 ) ) ) {
                v_iOptions = v_DI_IMAGE;
            }
            else if ( _functions.Equal( v_iFlags, Variant.Create( 3 ) ) ) {
                v_iOptions = v_DI_NORMAL;
            }
            else if ( _functions.Equal( v_iFlags, Variant.Create( 4 ) ) ) {
                v_iOptions = v_DI_COMPAT;
            }
            else if ( _functions.Equal( v_iFlags, Variant.Create( 5 ) ) ) {
                v_iOptions = v_DI_DEFAULTSIZE;
            }
            else {
                v_iOptions = v_DI_NOMIRROR;
            }

            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "bool" ), Variant.Create( "DrawIconEx" ), Variant.Create( "handle" ), v_hDC,
                Variant.Create( "int" ), v_iX, Variant.Create( "int" ), v_iY, Variant.Create( "handle" ), v_hIcon, Variant.Create( "int" ), v_iWidth,
                Variant.Create( "int" ), v_iHeight, Variant.Create( "uint" ), v_iStep, Variant.Create( "handle" ), v_hBrush, Variant.Create( "uint" ),
                v_iOptions );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_DrawLine( Variant v_hDC, Variant v_iX1, Variant v_iY1, Variant v_iX2, Variant v_iY2 ) {
            f__WinAPI_MoveTo( v_hDC, v_iX1, v_iY1 );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            f__WinAPI_LineTo( v_hDC, v_iX2, v_iY2 );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error+Variant.Create( 10 ), _context.Extended, Variant.Create( false ) );
            }

            return Variant.Create( true );
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_DrawText( Variant v_hDC, Variant v_sText, ref Variant v_tRect, Variant v_iFlags ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "int" ), Variant.Create( "DrawTextW" ), Variant.Create( "handle" ), v_hDC,
                Variant.Create( "wstr" ), v_sText, Variant.Create( "int" ), Variant.Create( -1 ), Variant.Create( "struct*" ), v_tRect, Variant.Create( "uint" ),
                v_iFlags );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_DuplicateHandle(
            Variant v_hSourceProcessHandle,
            Variant v_hSourceHandle,
            Variant v_hTargetProcessHandle,
            Variant v_iDesiredAccess,
            Variant v_bInheritHandle,
            Variant v_iOptions ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "kernel32.dll" ), Variant.Create( "bool" ), Variant.Create( "DuplicateHandle" ), Variant.Create( "handle" ),
                v_hSourceProcessHandle, Variant.Create( "handle" ), v_hSourceHandle, Variant.Create( "handle" ), v_hTargetProcessHandle,
                Variant.Create( "handle*" ), Variant.Create( 0 ), Variant.Create( "dword" ), v_iDesiredAccess, Variant.Create( "bool" ), v_bInheritHandle,
                Variant.Create( "dword" ), v_iOptions );
            if ( _functions.OR( _context.Error, _functions.NOT( v_aResult[Variant.Create( 0 )] ) ) ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 4 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_EnableWindow( Variant v_hWnd, Variant v_bEnable = null ) {
            Variant v_aResult = Variant.Create( (object) null );
            if ( v_bEnable == null ) {
                v_bEnable = Variant.Create( true );
            }
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "bool" ), Variant.Create( "EnableWindow" ), Variant.Create( "hwnd" ), v_hWnd,
                Variant.Create( "bool" ), v_bEnable );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_EnumDisplayDevices( Variant v_sDevice, Variant v_iDevNum ) {
            Variant v_tName = Variant.Create( (object) null );
            Variant v_iFlags = Variant.Create( (object) null );
            Variant v_aDevice = Variant.Create( (object) null );
            Variant v_tDevice = Variant.Create( (object) null );
            Variant v_iDevice = Variant.Create( (object) null );
            Variant v_aRet = Variant.Create( (object) null );
            Variant v_iN = Variant.Create( (object) null );
            v_tName = Variant.Create( 0 );
            v_iFlags = Variant.Create( 0 );
            v_aDevice = Variant.CreateArray( new Variant[Variant.Create( 5 )] );
            if ( v_sDevice != Variant.Create( "" ) ) {
                v_tName =
                    _functions.DllStructCreate(
                        _functions.Concat(
                            _functions.Concat( Variant.Create( "wchar Text[" ), _functions.StringLen( v_sDevice )+Variant.Create( 1 ) ), Variant.Create( "]" ) ) );
                _functions.DllStructSetData( v_tName, Variant.Create( "Text" ), v_sDevice );
            }

            v_tDevice = _functions.DllStructCreate( v_tagDISPLAY_DEVICE );
            v_iDevice = _functions.DllStructGetSize( v_tDevice );
            _functions.DllStructSetData( v_tDevice, Variant.Create( "Size" ), v_iDevice );
            v_aRet = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "bool" ), Variant.Create( "EnumDisplayDevicesW" ), Variant.Create( "struct*" ), v_tName,
                Variant.Create( "dword" ), v_iDevNum, Variant.Create( "struct*" ), v_tDevice, Variant.Create( "dword" ), Variant.Create( 1 ) );
            if ( _functions.OR( _context.Error, _functions.NOT( v_aRet[Variant.Create( 0 )] ) ) ) {
                return _functions.SetError( _context.Error+Variant.Create( 10 ), _context.Extended, Variant.Create( 0 ) );
            }

            v_iN = _functions.DllStructGetData( v_tDevice, Variant.Create( "Flags" ) );
            if ( _functions.BitAND( v_iN, v_DISPLAY_DEVICE_ATTACHED_TO_DESKTOP ) != Variant.Create( 0 ) ) {
                v_iFlags = _functions.BitOR( v_iFlags, Variant.Create( 1 ) );
            }

            if ( _functions.BitAND( v_iN, v_DISPLAY_DEVICE_PRIMARY_DEVICE ) != Variant.Create( 0 ) ) {
                v_iFlags = _functions.BitOR( v_iFlags, Variant.Create( 2 ) );
            }

            if ( _functions.BitAND( v_iN, v_DISPLAY_DEVICE_MIRRORING_DRIVER ) != Variant.Create( 0 ) ) {
                v_iFlags = _functions.BitOR( v_iFlags, Variant.Create( 4 ) );
            }

            if ( _functions.BitAND( v_iN, v_DISPLAY_DEVICE_VGA_COMPATIBLE ) != Variant.Create( 0 ) ) {
                v_iFlags = _functions.BitOR( v_iFlags, Variant.Create( 8 ) );
            }

            if ( _functions.BitAND( v_iN, v_DISPLAY_DEVICE_REMOVABLE ) != Variant.Create( 0 ) ) {
                v_iFlags = _functions.BitOR( v_iFlags, Variant.Create( 16 ) );
            }

            if ( _functions.BitAND( v_iN, v_DISPLAY_DEVICE_MODESPRUNED ) != Variant.Create( 0 ) ) {
                v_iFlags = _functions.BitOR( v_iFlags, Variant.Create( 32 ) );
            }

            v_aDevice[Variant.Create( 0 )] = Variant.Create( true );
            v_aDevice[Variant.Create( 1 )] = _functions.DllStructGetData( v_tDevice, Variant.Create( "Name" ) );
            v_aDevice[Variant.Create( 2 )] = _functions.DllStructGetData( v_tDevice, Variant.Create( "String" ) );
            v_aDevice[Variant.Create( 3 )] = v_iFlags;
            v_aDevice[Variant.Create( 4 )] = _functions.DllStructGetData( v_tDevice, Variant.Create( "ID" ) );
            return v_aDevice;
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_EnumWindows( Variant v_bVisible = null, Variant v_hWnd = null ) {
            if ( v_hWnd == null ) {
                v_hWnd = Variant.Create( new Default() );
            }
            if ( v_bVisible == null ) {
                v_bVisible = Variant.Create( true );
            }
            f___WinAPI_EnumWindowsInit();
            if ( v_hWnd == Variant.Create( new Default() ) ) {
                v_hWnd = f__WinAPI_GetDesktopWindow();
            }

            f___WinAPI_EnumWindowsChild( v_hWnd, v_bVisible );
            return v___g_aWinList_WinAPI;
            return Variant.Create( (object) null );
        }

        public Variant f___WinAPI_EnumWindowsAdd( Variant v_hWnd, Variant v_sClass = null ) {
            Variant v_iCount = Variant.Create( (object) null );
            if ( v_sClass == null ) {
                v_sClass = Variant.Create( "" );
            }
            if ( v_sClass == Variant.Create( "" ) ) {
                v_sClass = f__WinAPI_GetClassName( v_hWnd );
            }

            v___g_aWinList_WinAPI[Variant.Create( 0 ), Variant.Create( 0 )] += Variant.Create( 1 );
            v_iCount = v___g_aWinList_WinAPI[Variant.Create( 0 ), Variant.Create( 0 )];
            if ( v_iCount >= v___g_aWinList_WinAPI[Variant.Create( 0 ), Variant.Create( 1 )] ) {
                v___g_aWinList_WinAPI.ReDim( v_iCount+Variant.Create( 64 ), Variant.Create( 2 ) );
                v___g_aWinList_WinAPI[Variant.Create( 0 ), Variant.Create( 1 )] += Variant.Create( 64 );
            }

            v___g_aWinList_WinAPI[v_iCount, Variant.Create( 0 )] = v_hWnd;
            v___g_aWinList_WinAPI[v_iCount, Variant.Create( 1 )] = v_sClass;
            return Variant.Create( (object) null );
        }

        public Variant f___WinAPI_EnumWindowsChild( Variant v_hWnd, Variant v_bVisible = null ) {
            if ( v_bVisible == null ) {
                v_bVisible = Variant.Create( true );
            }
            v_hWnd = f__WinAPI_GetWindow( v_hWnd, v_GW_CHILD );
            while ( v_hWnd != Variant.Create( 0 ) ) {
                if ( _functions.OR( _functions.NOT( v_bVisible ), f__WinAPI_IsWindowVisible( v_hWnd ) ) ) {
                    f___WinAPI_EnumWindowsAdd( v_hWnd );
                    f___WinAPI_EnumWindowsChild( v_hWnd, v_bVisible );
                }

                v_hWnd = f__WinAPI_GetWindow( v_hWnd, v_GW_HWNDNEXT );
            }

            return Variant.Create( (object) null );
        }

        public Variant f___WinAPI_EnumWindowsInit() {
            v___g_aWinList_WinAPI.ReDim( Variant.Create( 64 ), Variant.Create( 2 ) );
            v___g_aWinList_WinAPI[Variant.Create( 0 ), Variant.Create( 0 )] = Variant.Create( 0 );
            v___g_aWinList_WinAPI[Variant.Create( 0 ), Variant.Create( 1 )] = Variant.Create( 64 );
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_EnumWindowsPopup() {
            Variant v_hWnd = Variant.Create( (object) null );
            Variant v_sClass = Variant.Create( (object) null );
            f___WinAPI_EnumWindowsInit();
            v_hWnd = f__WinAPI_GetWindow( f__WinAPI_GetDesktopWindow(), v_GW_CHILD );
            while ( v_hWnd != Variant.Create( 0 ) ) {
                if ( f__WinAPI_IsWindowVisible( v_hWnd ) ) {
                    v_sClass = f__WinAPI_GetClassName( v_hWnd );
                    if ( v_sClass == Variant.Create( "#32768" ) ) {
                        f___WinAPI_EnumWindowsAdd( v_hWnd );
                    }
                    else if ( v_sClass == Variant.Create( "ToolbarWindow32" ) ) {
                        f___WinAPI_EnumWindowsAdd( v_hWnd );
                    }
                    else if ( v_sClass == Variant.Create( "ToolTips_Class32" ) ) {
                        f___WinAPI_EnumWindowsAdd( v_hWnd );
                    }
                    else if ( v_sClass == Variant.Create( "BaseBar" ) ) {
                        f___WinAPI_EnumWindowsChild( v_hWnd );
                    }
                }

                v_hWnd = f__WinAPI_GetWindow( v_hWnd, v_GW_HWNDNEXT );
            }

            return v___g_aWinList_WinAPI;
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_EnumWindowsTop() {
            Variant v_hWnd = Variant.Create( (object) null );
            f___WinAPI_EnumWindowsInit();
            v_hWnd = f__WinAPI_GetWindow( f__WinAPI_GetDesktopWindow(), v_GW_CHILD );
            while ( v_hWnd != Variant.Create( 0 ) ) {
                if ( f__WinAPI_IsWindowVisible( v_hWnd ) ) {
                    f___WinAPI_EnumWindowsAdd( v_hWnd );
                }

                v_hWnd = f__WinAPI_GetWindow( v_hWnd, v_GW_HWNDNEXT );
            }

            return v___g_aWinList_WinAPI;
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_ExpandEnvironmentStrings( Variant v_sString ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "kernel32.dll" ), Variant.Create( "dword" ), Variant.Create( "ExpandEnvironmentStringsW" ), Variant.Create( "wstr" ), v_sString,
                Variant.Create( "wstr" ), Variant.Create( "" ), Variant.Create( "dword" ), Variant.Create( 4096 ) );
            if ( _functions.OR( _context.Error, _functions.NOT( v_aResult[Variant.Create( 0 )] ) ) ) {
                return _functions.SetError( _context.Error+Variant.Create( 10 ), _context.Extended, Variant.Create( "" ) );
            }

            return v_aResult[Variant.Create( 2 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_ExtractIconEx( Variant v_sFile, Variant v_iIndex, Variant v_pLarge, Variant v_pSmall, Variant v_iIcons ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "shell32.dll" ), Variant.Create( "uint" ), Variant.Create( "ExtractIconExW" ), Variant.Create( "wstr" ), v_sFile,
                Variant.Create( "int" ), v_iIndex, Variant.Create( "struct*" ), v_pLarge, Variant.Create( "struct*" ), v_pSmall, Variant.Create( "uint" ),
                v_iIcons );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_FatalAppExit( Variant v_sMessage ) {
            _functions.DllCall(
                Variant.Create( "kernel32.dll" ), Variant.Create( "none" ), Variant.Create( "FatalAppExitW" ), Variant.Create( "uint" ), Variant.Create( 0 ),
                Variant.Create( "wstr" ), v_sMessage );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended );
            }

            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_FillRect( Variant v_hDC, Variant v_pRect, Variant v_hBrush ) {
            Variant v_aResult = Variant.Create( (object) null );
            if ( _functions.IsPtr( v_hBrush ) ) {
                v_aResult = _functions.DllCall(
                    Variant.Create( "user32.dll" ), Variant.Create( "int" ), Variant.Create( "FillRect" ), Variant.Create( "handle" ), v_hDC,
                    Variant.Create( "struct*" ), v_pRect, Variant.Create( "handle" ), v_hBrush );
            }
            else {
                v_aResult = _functions.DllCall(
                    Variant.Create( "user32.dll" ), Variant.Create( "int" ), Variant.Create( "FillRect" ), Variant.Create( "handle" ), v_hDC,
                    Variant.Create( "struct*" ), v_pRect, Variant.Create( "dword_ptr" ), v_hBrush );
            }

            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_FindExecutable( Variant v_sFileName, Variant v_sDirectory = null ) {
            Variant v_aResult = Variant.Create( (object) null );
            if ( v_sDirectory == null ) {
                v_sDirectory = Variant.Create( "" );
            }
            v_aResult = _functions.DllCall(
                Variant.Create( "shell32.dll" ), Variant.Create( "INT" ), Variant.Create( "FindExecutableW" ), Variant.Create( "wstr" ), v_sFileName,
                Variant.Create( "wstr" ), v_sDirectory, Variant.Create( "wstr" ), Variant.Create( "" ) );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( "" ) );
            }

            if ( v_aResult[Variant.Create( 0 )] <= Variant.Create( 32 ) ) {
                return _functions.SetError( Variant.Create( 10 ), v_aResult[Variant.Create( 0 )], Variant.Create( "" ) );
            }

            return _functions.SetExtended( v_aResult[Variant.Create( 0 )], v_aResult[Variant.Create( 3 )] );
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_FindWindow( Variant v_sClassName, Variant v_sWindowName ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "hwnd" ), Variant.Create( "FindWindowW" ), Variant.Create( "wstr" ), v_sClassName,
                Variant.Create( "wstr" ), v_sWindowName );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_FlashWindow( Variant v_hWnd, Variant v_bInvert = null ) {
            Variant v_aResult = Variant.Create( (object) null );
            if ( v_bInvert == null ) {
                v_bInvert = Variant.Create( true );
            }
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "bool" ), Variant.Create( "FlashWindow" ), Variant.Create( "hwnd" ), v_hWnd,
                Variant.Create( "bool" ), v_bInvert );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_FlashWindowEx( Variant v_hWnd, Variant v_iFlags = null, Variant v_iCount = null, Variant v_iTimeout = null ) {
            Variant v_tFlash = Variant.Create( (object) null );
            Variant v_iFlash = Variant.Create( (object) null );
            Variant v_iMode = Variant.Create( (object) null );
            Variant v_aResult = Variant.Create( (object) null );
            if ( v_iTimeout == null ) {
                v_iTimeout = Variant.Create( 0 );
            }
            if ( v_iCount == null ) {
                v_iCount = Variant.Create( 3 );
            }
            if ( v_iFlags == null ) {
                v_iFlags = Variant.Create( 3 );
            }
            v_tFlash = _functions.DllStructCreate( v_tagFLASHWINFO );
            v_iFlash = _functions.DllStructGetSize( v_tFlash );
            v_iMode = Variant.Create( 0 );
            if ( _functions.BitAND( v_iFlags, Variant.Create( 1 ) ) != Variant.Create( 0 ) ) {
                v_iMode = _functions.BitOR( v_iMode, v_FLASHW_CAPTION );
            }

            if ( _functions.BitAND( v_iFlags, Variant.Create( 2 ) ) != Variant.Create( 0 ) ) {
                v_iMode = _functions.BitOR( v_iMode, v_FLASHW_TRAY );
            }

            if ( _functions.BitAND( v_iFlags, Variant.Create( 4 ) ) != Variant.Create( 0 ) ) {
                v_iMode = _functions.BitOR( v_iMode, v_FLASHW_TIMER );
            }

            if ( _functions.BitAND( v_iFlags, Variant.Create( 8 ) ) != Variant.Create( 0 ) ) {
                v_iMode = _functions.BitOR( v_iMode, v_FLASHW_TIMERNOFG );
            }

            _functions.DllStructSetData( v_tFlash, Variant.Create( "Size" ), v_iFlash );
            _functions.DllStructSetData( v_tFlash, Variant.Create( "hWnd" ), v_hWnd );
            _functions.DllStructSetData( v_tFlash, Variant.Create( "Flags" ), v_iMode );
            _functions.DllStructSetData( v_tFlash, Variant.Create( "Count" ), v_iCount );
            _functions.DllStructSetData( v_tFlash, Variant.Create( "Timeout" ), v_iTimeout );
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "bool" ), Variant.Create( "FlashWindowEx" ), Variant.Create( "struct*" ), v_tFlash );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_FloatToInt( Variant v_nFloat ) {
            Variant v_tFloat = Variant.Create( (object) null );
            Variant v_tInt = Variant.Create( (object) null );
            v_tFloat = _functions.DllStructCreate( Variant.Create( "float" ) );
            v_tInt = _functions.DllStructCreate( Variant.Create( "int" ), _functions.DllStructGetPtr( v_tFloat ) );
            _functions.DllStructSetData( v_tFloat, Variant.Create( 1 ), v_nFloat );
            return _functions.DllStructGetData( v_tInt, Variant.Create( 1 ) );
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_FlushFileBuffers( Variant v_hFile ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "kernel32.dll" ), Variant.Create( "bool" ), Variant.Create( "FlushFileBuffers" ), Variant.Create( "handle" ), v_hFile );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_FormatMessage(
            Variant v_iFlags,
            Variant v_pSource,
            Variant v_iMessageID,
            Variant v_iLanguageID,
            ref Variant v_pBuffer,
            Variant v_iSize,
            Variant v_vArguments ) {
            Variant v_sBufferType = Variant.Create( (object) null );
            Variant v_aResult = Variant.Create( (object) null );
            v_sBufferType = Variant.Create( "struct*" );
            if ( _functions.IsString( v_pBuffer ) ) {
                v_sBufferType = Variant.Create( "wstr" );
            }

            v_aResult = _functions.DllCall(
                Variant.Create( "Kernel32.dll" ), Variant.Create( "dword" ), Variant.Create( "FormatMessageW" ), Variant.Create( "dword" ), v_iFlags,
                Variant.Create( "ptr" ), v_pSource, Variant.Create( "dword" ), v_iMessageID, Variant.Create( "dword" ), v_iLanguageID, v_sBufferType, v_pBuffer,
                Variant.Create( "dword" ), v_iSize, Variant.Create( "ptr" ), v_vArguments );
            if ( _functions.OR( _context.Error, _functions.NOT( v_aResult[Variant.Create( 0 )] ) ) ) {
                return _functions.SetError( _context.Error+Variant.Create( 10 ), _context.Extended, Variant.Create( 0 ) );
            }

            if ( v_sBufferType == Variant.Create( "wstr" ) ) {
                v_pBuffer = v_aResult[Variant.Create( 5 )];
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_FrameRect( Variant v_hDC, Variant v_pRect, Variant v_hBrush ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "int" ), Variant.Create( "FrameRect" ), Variant.Create( "handle" ), v_hDC,
                Variant.Create( "ptr" ), v_pRect, Variant.Create( "handle" ), v_hBrush );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_FreeLibrary( Variant v_hModule ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "kernel32.dll" ), Variant.Create( "bool" ), Variant.Create( "FreeLibrary" ), Variant.Create( "handle" ), v_hModule );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetAncestor( Variant v_hWnd, Variant v_iFlags = null ) {
            Variant v_aResult = Variant.Create( (object) null );
            if ( v_iFlags == null ) {
                v_iFlags = Variant.Create( 1 );
            }
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "hwnd" ), Variant.Create( "GetAncestor" ), Variant.Create( "hwnd" ), v_hWnd,
                Variant.Create( "uint" ), v_iFlags );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetAsyncKeyState( Variant v_iKey ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "short" ), Variant.Create( "GetAsyncKeyState" ), Variant.Create( "int" ), v_iKey );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetBkMode( Variant v_hDC ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "gdi32.dll" ), Variant.Create( "int" ), Variant.Create( "GetBkMode" ), Variant.Create( "handle" ), v_hDC );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetClassName( Variant v_hWnd ) {
            Variant v_aResult = Variant.Create( (object) null );
            if ( _functions.NOT( _functions.IsHWnd( v_hWnd ) ) ) {
                v_hWnd = _functions.GUICtrlGetHandle( v_hWnd );
            }

            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "int" ), Variant.Create( "GetClassNameW" ), Variant.Create( "hwnd" ), v_hWnd,
                Variant.Create( "wstr" ), Variant.Create( "" ), Variant.Create( "int" ), Variant.Create( 4096 ) );
            if ( _functions.OR( _context.Error, _functions.NOT( v_aResult[Variant.Create( 0 )] ) ) ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( "" ) );
            }

            return _functions.SetExtended( v_aResult[Variant.Create( 0 )], v_aResult[Variant.Create( 2 )] );
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetClientHeight( Variant v_hWnd ) {
            Variant v_tRect = Variant.Create( (object) null );
            v_tRect = f__WinAPI_GetClientRect( v_hWnd );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return _functions.DllStructGetData( v_tRect, Variant.Create( "Bottom" ) )-_functions.DllStructGetData( v_tRect, Variant.Create( "Top" ) );
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetClientWidth( Variant v_hWnd ) {
            Variant v_tRect = Variant.Create( (object) null );
            v_tRect = f__WinAPI_GetClientRect( v_hWnd );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return _functions.DllStructGetData( v_tRect, Variant.Create( "Right" ) )-_functions.DllStructGetData( v_tRect, Variant.Create( "Left" ) );
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetClientRect( Variant v_hWnd ) {
            Variant v_tRect = Variant.Create( (object) null );
            Variant v_aRet = Variant.Create( (object) null );
            v_tRect = _functions.DllStructCreate( v_tagRECT );
            v_aRet = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "bool" ), Variant.Create( "GetClientRect" ), Variant.Create( "hwnd" ), v_hWnd,
                Variant.Create( "struct*" ), v_tRect );
            if ( _functions.OR( _context.Error, _functions.NOT( v_aRet[Variant.Create( 0 )] ) ) ) {
                return _functions.SetError( _context.Error+Variant.Create( 10 ), _context.Extended, Variant.Create( 0 ) );
            }

            return v_tRect;
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetCurrentProcess() {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall( Variant.Create( "kernel32.dll" ), Variant.Create( "handle" ), Variant.Create( "GetCurrentProcess" ) );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetCurrentProcessID() {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall( Variant.Create( "kernel32.dll" ), Variant.Create( "dword" ), Variant.Create( "GetCurrentProcessId" ) );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetCurrentThread() {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall( Variant.Create( "kernel32.dll" ), Variant.Create( "handle" ), Variant.Create( "GetCurrentThread" ) );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetCurrentThreadId() {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall( Variant.Create( "kernel32.dll" ), Variant.Create( "dword" ), Variant.Create( "GetCurrentThreadId" ) );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetCursorInfo() {
            Variant v_tCursor = Variant.Create( (object) null );
            Variant v_iCursor = Variant.Create( (object) null );
            Variant v_aRet = Variant.Create( (object) null );
            Variant v_aCursor = Variant.Create( (object) null );
            v_tCursor = _functions.DllStructCreate( v_tagCURSORINFO );
            v_iCursor = _functions.DllStructGetSize( v_tCursor );
            _functions.DllStructSetData( v_tCursor, Variant.Create( "Size" ), v_iCursor );
            v_aRet = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "bool" ), Variant.Create( "GetCursorInfo" ), Variant.Create( "struct*" ), v_tCursor );
            if ( _functions.OR( _context.Error, _functions.NOT( v_aRet[Variant.Create( 0 )] ) ) ) {
                return _functions.SetError( _context.Error+Variant.Create( 10 ), _context.Extended, Variant.Create( 0 ) );
            }

            v_aCursor = Variant.CreateArray( new Variant[Variant.Create( 5 )] );
            v_aCursor[Variant.Create( 0 )] = Variant.Create( true );
            v_aCursor[Variant.Create( 1 )] = _functions.DllStructGetData( v_tCursor, Variant.Create( "Flags" ) ) != Variant.Create( 0 );
            v_aCursor[Variant.Create( 2 )] = _functions.DllStructGetData( v_tCursor, Variant.Create( "hCursor" ) );
            v_aCursor[Variant.Create( 3 )] = _functions.DllStructGetData( v_tCursor, Variant.Create( "X" ) );
            v_aCursor[Variant.Create( 4 )] = _functions.DllStructGetData( v_tCursor, Variant.Create( "Y" ) );
            return v_aCursor;
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetDC( Variant v_hWnd ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "handle" ), Variant.Create( "GetDC" ), Variant.Create( "hwnd" ), v_hWnd );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetDesktopWindow() {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall( Variant.Create( "user32.dll" ), Variant.Create( "hwnd" ), Variant.Create( "GetDesktopWindow" ) );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetDeviceCaps( Variant v_hDC, Variant v_iIndex ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "gdi32.dll" ), Variant.Create( "int" ), Variant.Create( "GetDeviceCaps" ), Variant.Create( "handle" ), v_hDC,
                Variant.Create( "int" ), v_iIndex );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetDIBits(
            Variant v_hDC,
            Variant v_hBmp,
            Variant v_iStartScan,
            Variant v_iScanLines,
            Variant v_pBits,
            Variant v_pBI,
            Variant v_iUsage ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "gdi32.dll" ), Variant.Create( "int" ), Variant.Create( "GetDIBits" ), Variant.Create( "handle" ), v_hDC,
                Variant.Create( "handle" ), v_hBmp, Variant.Create( "uint" ), v_iStartScan, Variant.Create( "uint" ), v_iScanLines, Variant.Create( "ptr" ),
                v_pBits, Variant.Create( "ptr" ), v_pBI, Variant.Create( "uint" ), v_iUsage );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetDlgCtrlID( Variant v_hWnd ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "int" ), Variant.Create( "GetDlgCtrlID" ), Variant.Create( "hwnd" ), v_hWnd );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetDlgItem( Variant v_hWnd, Variant v_iItemID ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "hwnd" ), Variant.Create( "GetDlgItem" ), Variant.Create( "hwnd" ), v_hWnd,
                Variant.Create( "int" ), v_iItemID );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetFileSizeEx( Variant v_hFile ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "kernel32.dll" ), Variant.Create( "bool" ), Variant.Create( "GetFileSizeEx" ), Variant.Create( "handle" ), v_hFile,
                Variant.Create( "int64*" ), Variant.Create( 0 ) );
            if ( _functions.OR( _context.Error, _functions.NOT( v_aResult[Variant.Create( 0 )] ) ) ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( -1 ) );
            }

            return v_aResult[Variant.Create( 2 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetFocus() {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall( Variant.Create( "user32.dll" ), Variant.Create( "hwnd" ), Variant.Create( "GetFocus" ) );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetForegroundWindow() {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall( Variant.Create( "user32.dll" ), Variant.Create( "hwnd" ), Variant.Create( "GetForegroundWindow" ) );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetGuiResources( Variant v_iFlag = null, Variant v_hProcess = null ) {
            Variant v_aResult = Variant.Create( (object) null );
            if ( v_hProcess == null ) {
                v_hProcess = Variant.Create( -1 );
            }
            if ( v_iFlag == null ) {
                v_iFlag = Variant.Create( 0 );
            }
            if ( v_hProcess == Variant.Create( -1 ) ) {
                v_hProcess = f__WinAPI_GetCurrentProcess();
            }

            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "dword" ), Variant.Create( "GetGuiResources" ), Variant.Create( "handle" ), v_hProcess,
                Variant.Create( "dword" ), v_iFlag );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetIconInfo( Variant v_hIcon ) {
            Variant v_tInfo = Variant.Create( (object) null );
            Variant v_aRet = Variant.Create( (object) null );
            Variant v_aIcon = Variant.Create( (object) null );
            v_tInfo = _functions.DllStructCreate( v_tagICONINFO );
            v_aRet = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "bool" ), Variant.Create( "GetIconInfo" ), Variant.Create( "handle" ), v_hIcon,
                Variant.Create( "struct*" ), v_tInfo );
            if ( _functions.OR( _context.Error, _functions.NOT( v_aRet[Variant.Create( 0 )] ) ) ) {
                return _functions.SetError( _context.Error+Variant.Create( 10 ), _context.Extended, Variant.Create( 0 ) );
            }

            v_aIcon = Variant.CreateArray( new Variant[Variant.Create( 6 )] );
            v_aIcon[Variant.Create( 0 )] = Variant.Create( true );
            v_aIcon[Variant.Create( 1 )] = _functions.DllStructGetData( v_tInfo, Variant.Create( "Icon" ) ) != Variant.Create( 0 );
            v_aIcon[Variant.Create( 2 )] = _functions.DllStructGetData( v_tInfo, Variant.Create( "XHotSpot" ) );
            v_aIcon[Variant.Create( 3 )] = _functions.DllStructGetData( v_tInfo, Variant.Create( "YHotSpot" ) );
            v_aIcon[Variant.Create( 4 )] = _functions.DllStructGetData( v_tInfo, Variant.Create( "hMask" ) );
            v_aIcon[Variant.Create( 5 )] = _functions.DllStructGetData( v_tInfo, Variant.Create( "hColor" ) );
            return v_aIcon;
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetLastErrorMessage() {
            Variant v_iLastError = Variant.Create( (object) null );
            Variant v_tBufferPtr = Variant.Create( (object) null );
            Variant v_nCount = Variant.Create( (object) null );
            Variant v_sText = Variant.Create( (object) null );
            Variant v_pBuffer = Variant.Create( (object) null );
            Variant v_tBuffer = Variant.Create( (object) null );
            v_iLastError = f__WinAPI_GetLastError();
            v_tBufferPtr = _functions.DllStructCreate( Variant.Create( "ptr" ) );
            v_nCount = f__WinAPI_FormatMessage(
                _functions.BitOR( v_FORMAT_MESSAGE_ALLOCATE_BUFFER, v_FORMAT_MESSAGE_FROM_SYSTEM ), Variant.Create( 0 ), v_iLastError, Variant.Create( 0 ),
                ref v_tBufferPtr, Variant.Create( 0 ), Variant.Create( 0 ) );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, Variant.Create( 0 ), Variant.Create( "" ) );
            }

            v_sText = Variant.Create( "" );
            v_pBuffer = _functions.DllStructGetData( v_tBufferPtr, Variant.Create( 1 ) );
            if ( v_pBuffer ) {
                if ( v_nCount > Variant.Create( 0 ) ) {
                    v_tBuffer =
                        _functions.DllStructCreate(
                            _functions.Concat( _functions.Concat( Variant.Create( "wchar[" ), v_nCount+Variant.Create( 1 ) ), Variant.Create( "]" ) ), v_pBuffer );
                    v_sText = _functions.DllStructGetData( v_tBuffer, Variant.Create( 1 ) );
                }

                f__WinAPI_LocalFree( v_pBuffer );
            }

            return v_sText;
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetLayeredWindowAttributes(
            Variant v_hWnd,
            ref Variant v_iTranscolor,
            ref Variant v_iTransparency,
            Variant v_bColorRef = null ) {
            Variant v_aResult = Variant.Create( (object) null );
            if ( v_bColorRef == null ) {
                v_bColorRef = Variant.Create( false );
            }
            v_iTranscolor = Variant.Create( -1 );
            v_iTransparency = Variant.Create( -1 );
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "bool" ), Variant.Create( "GetLayeredWindowAttributes" ), Variant.Create( "hwnd" ), v_hWnd,
                Variant.Create( "INT*" ), v_iTranscolor, Variant.Create( "byte*" ), v_iTransparency, Variant.Create( "dword*" ), Variant.Create( 0 ) );
            if ( _functions.OR( _context.Error, _functions.NOT( v_aResult[Variant.Create( 0 )] ) ) ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            if ( _functions.NOT( v_bColorRef ) ) {
                v_aResult[Variant.Create( 2 )] =
                    _functions.Int(
                        _functions.Concat(
                            _functions.Concat(
                                _functions.BinaryMid( v_aResult[Variant.Create( 2 )], Variant.Create( 3 ), Variant.Create( 1 ) ),
                                _functions.BinaryMid( v_aResult[Variant.Create( 2 )], Variant.Create( 2 ), Variant.Create( 1 ) ) ),
                            _functions.BinaryMid( v_aResult[Variant.Create( 2 )], Variant.Create( 1 ), Variant.Create( 1 ) ) ) );
            }

            v_iTranscolor = v_aResult[Variant.Create( 2 )];
            v_iTransparency = v_aResult[Variant.Create( 3 )];
            return v_aResult[Variant.Create( 4 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetModuleHandle( Variant v_sModuleName ) {
            Variant v_sModuleNameType = Variant.Create( (object) null );
            Variant v_aResult = Variant.Create( (object) null );
            v_sModuleNameType = Variant.Create( "wstr" );
            if ( v_sModuleName == Variant.Create( "" ) ) {
                v_sModuleName = Variant.Create( 0 );
                v_sModuleNameType = Variant.Create( "ptr" );
            }

            v_aResult = _functions.DllCall(
                Variant.Create( "kernel32.dll" ), Variant.Create( "handle" ), Variant.Create( "GetModuleHandleW" ), v_sModuleNameType, v_sModuleName );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetMousePos( Variant v_bToClient = null, Variant v_hWnd = null ) {
            Variant v_iMode = Variant.Create( (object) null );
            Variant v_aPos = Variant.Create( (object) null );
            Variant v_tPoint = Variant.Create( (object) null );
            if ( v_hWnd == null ) {
                v_hWnd = Variant.Create( 0 );
            }
            if ( v_bToClient == null ) {
                v_bToClient = Variant.Create( false );
            }
            v_iMode = _functions.Opt( Variant.Create( "MouseCoordMode" ), Variant.Create( 1 ) );
            v_aPos = _functions.MouseGetPos();
            _functions.Opt( Variant.Create( "MouseCoordMode" ), v_iMode );
            v_tPoint = _functions.DllStructCreate( v_tagPOINT );
            _functions.DllStructSetData( v_tPoint, Variant.Create( "X" ), v_aPos[Variant.Create( 0 )] );
            _functions.DllStructSetData( v_tPoint, Variant.Create( "Y" ), v_aPos[Variant.Create( 1 )] );
            if ( _functions.AND( v_bToClient, _functions.NOT( f__WinAPI_ScreenToClient( v_hWnd, ref v_tPoint ) ) ) ) {
                return _functions.SetError( _context.Error+Variant.Create( 20 ), _context.Extended, Variant.Create( 0 ) );
            }

            return v_tPoint;
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetMousePosX( Variant v_bToClient = null, Variant v_hWnd = null ) {
            Variant v_tPoint = Variant.Create( (object) null );
            if ( v_hWnd == null ) {
                v_hWnd = Variant.Create( 0 );
            }
            if ( v_bToClient == null ) {
                v_bToClient = Variant.Create( false );
            }
            v_tPoint = f__WinAPI_GetMousePos( v_bToClient, v_hWnd );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return _functions.DllStructGetData( v_tPoint, Variant.Create( "X" ) );
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetMousePosY( Variant v_bToClient = null, Variant v_hWnd = null ) {
            Variant v_tPoint = Variant.Create( (object) null );
            if ( v_hWnd == null ) {
                v_hWnd = Variant.Create( 0 );
            }
            if ( v_bToClient == null ) {
                v_bToClient = Variant.Create( false );
            }
            v_tPoint = f__WinAPI_GetMousePos( v_bToClient, v_hWnd );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return _functions.DllStructGetData( v_tPoint, Variant.Create( "Y" ) );
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetObject( Variant v_hObject, Variant v_iSize, Variant v_pObject ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "gdi32.dll" ), Variant.Create( "int" ), Variant.Create( "GetObjectW" ), Variant.Create( "handle" ), v_hObject,
                Variant.Create( "int" ), v_iSize, Variant.Create( "ptr" ), v_pObject );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetOpenFileName(
            Variant v_sTitle = null,
            Variant v_sFilter = null,
            Variant v_sInitalDir = null,
            Variant v_sDefaultFile = null,
            Variant v_sDefaultExt = null,
            Variant v_iFilterIndex = null,
            Variant v_iFlags = null,
            Variant v_iFlagsEx = null,
            Variant v_hWndOwner = null ) {
            Variant v_iPathLen = Variant.Create( (object) null );
            Variant v_iNulls = Variant.Create( (object) null );
            Variant v_tOFN = Variant.Create( (object) null );
            Variant v_aFiles = Variant.Create( (object) null );
            Variant v_iFlag = Variant.Create( (object) null );
            Variant v_asFLines = Variant.Create( (object) null );
            Variant v_asFilter = Variant.Create( (object) null );
            Variant v_iStart = Variant.Create( (object) null );
            Variant v_iFinal = Variant.Create( (object) null );
            Variant v_tagFilter = Variant.Create( (object) null );
            ForToNextLooper loopHandler89f9ad6ee6c14e4caccbd16cc7a7f77a;
            Variant v_i = null;
            Variant v_tTitle = Variant.Create( (object) null );
            Variant v_tInitialDir = Variant.Create( (object) null );
            Variant v_tFilter = Variant.Create( (object) null );
            Variant v_tPath = Variant.Create( (object) null );
            Variant v_tExtn = Variant.Create( (object) null );
            ForToNextLooper loopHandlerc86a252ad5154c30a7bf62fafd35de0f;
            Variant v_aRes = Variant.Create( (object) null );
            ForToNextLooper loopHandler2d28e401e4a34a0dac7476a0a7b95d67;
            Variant v_x = null;
            if ( v_hWndOwner == null ) {
                v_hWndOwner = Variant.Create( 0 );
            }
            if ( v_iFlagsEx == null ) {
                v_iFlagsEx = Variant.Create( 0 );
            }
            if ( v_iFlags == null ) {
                v_iFlags = Variant.Create( 0 );
            }
            if ( v_iFilterIndex == null ) {
                v_iFilterIndex = Variant.Create( 1 );
            }
            if ( v_sDefaultExt == null ) {
                v_sDefaultExt = Variant.Create( "" );
            }
            if ( v_sDefaultFile == null ) {
                v_sDefaultFile = Variant.Create( "" );
            }
            if ( v_sInitalDir == null ) {
                v_sInitalDir = Variant.Create( "." );
            }
            if ( v_sFilter == null ) {
                v_sFilter = Variant.Create( "All files (*.*)" );
            }
            if ( v_sTitle == null ) {
                v_sTitle = Variant.Create( "" );
            }
            v_iPathLen = Variant.Create( 4096 );
            v_iNulls = Variant.Create( 0 );
            v_tOFN = _functions.DllStructCreate( v_tagOPENFILENAME );
            v_aFiles = Variant.CreateArray( new Variant[Variant.Create( 1 )] );
            v_aFiles.InitArray(
                new[] {
                    Variant.Create( 0 )
                }
                );
            v_iFlag = v_iFlags;
            v_asFLines = _functions.StringSplit( v_sFilter, Variant.Create( "|" ) );
            v_asFilter = Variant.CreateArray( new Variant[v_asFLines[Variant.Create( 0 )] * Variant.Create( 2 )+Variant.Create( 1 )] );
            v_asFilter[Variant.Create( 0 )] = v_asFLines[Variant.Create( 0 )] * Variant.Create( 2 );
            loopHandler89f9ad6ee6c14e4caccbd16cc7a7f77a = new ForToNextLooper( Variant.Create( 1 ), v_asFLines[Variant.Create( 0 )], null );
            v_i = loopHandler89f9ad6ee6c14e4caccbd16cc7a7f77a.Index;
            for ( ; loopHandler89f9ad6ee6c14e4caccbd16cc7a7f77a.MoveNext(); v_i = loopHandler89f9ad6ee6c14e4caccbd16cc7a7f77a.Index ) {
                v_iStart = _functions.StringInStr( v_asFLines[v_i], Variant.Create( "(" ), Variant.Create( 0 ), Variant.Create( 1 ) );
                v_iFinal = _functions.StringInStr( v_asFLines[v_i], Variant.Create( ")" ), Variant.Create( 0 ), Variant.Create( -1 ) );
                v_asFilter[v_i * Variant.Create( 2 )-Variant.Create( 1 )] =
                    _functions.StringStripWS( _functions.StringLeft( v_asFLines[v_i], v_iStart-Variant.Create( 1 ) ), v_STR_STRIPLEADING+v_STR_STRIPTRAILING );
                v_asFilter[v_i * Variant.Create( 2 )] =
                    _functions.StringStripWS(
                        _functions.StringTrimRight(
                            _functions.StringTrimLeft( v_asFLines[v_i], v_iStart ), _functions.StringLen( v_asFLines[v_i] )-v_iFinal+Variant.Create( 1 ) ),
                        v_STR_STRIPLEADING+v_STR_STRIPTRAILING );
                v_tagFilter =
                    v_tagFilter.ConcatAssign(
                        _functions.Concat(
                            _functions.Concat(
                                _functions.Concat(
                                    _functions.Concat(
                                        Variant.Create( "wchar[" ),
                                        _functions.StringLen( v_asFilter[v_i * Variant.Create( 2 )-Variant.Create( 1 )] )+Variant.Create( 1 ) ),
                                    Variant.Create( "];wchar[" ) ), _functions.StringLen( v_asFilter[v_i * Variant.Create( 2 )] )+Variant.Create( 1 ) ),
                            Variant.Create( "];" ) ) );
            }

            v_tTitle =
                _functions.DllStructCreate(
                    _functions.Concat(
                        _functions.Concat( Variant.Create( "wchar Title[" ), _functions.StringLen( v_sTitle )+Variant.Create( 1 ) ), Variant.Create( "]" ) ) );
            v_tInitialDir =
                _functions.DllStructCreate(
                    _functions.Concat(
                        _functions.Concat( Variant.Create( "wchar InitDir[" ), _functions.StringLen( v_sInitalDir )+Variant.Create( 1 ) ), Variant.Create( "]" ) ) );
            v_tFilter = _functions.DllStructCreate( _functions.Concat( v_tagFilter, Variant.Create( "wchar" ) ) );
            v_tPath = _functions.DllStructCreate( _functions.Concat( _functions.Concat( Variant.Create( "wchar Path[" ), v_iPathLen ), Variant.Create( "]" ) ) );
            v_tExtn =
                _functions.DllStructCreate(
                    _functions.Concat(
                        _functions.Concat( Variant.Create( "wchar Extension[" ), _functions.StringLen( v_sDefaultExt )+Variant.Create( 1 ) ),
                        Variant.Create( "]" ) ) );
            loopHandlerc86a252ad5154c30a7bf62fafd35de0f = new ForToNextLooper( Variant.Create( 1 ), v_asFilter[Variant.Create( 0 )], null );
            v_i = loopHandlerc86a252ad5154c30a7bf62fafd35de0f.Index;
            for ( ; loopHandlerc86a252ad5154c30a7bf62fafd35de0f.MoveNext(); v_i = loopHandlerc86a252ad5154c30a7bf62fafd35de0f.Index ) {
                _functions.DllStructSetData( v_tFilter, v_i, v_asFilter[v_i] );
            }

            _functions.DllStructSetData( v_tTitle, Variant.Create( "Title" ), v_sTitle );
            _functions.DllStructSetData( v_tInitialDir, Variant.Create( "InitDir" ), v_sInitalDir );
            _functions.DllStructSetData( v_tPath, Variant.Create( "Path" ), v_sDefaultFile );
            _functions.DllStructSetData( v_tExtn, Variant.Create( "Extension" ), v_sDefaultExt );
            _functions.DllStructSetData( v_tOFN, Variant.Create( "StructSize" ), _functions.DllStructGetSize( v_tOFN ) );
            _functions.DllStructSetData( v_tOFN, Variant.Create( "hwndOwner" ), v_hWndOwner );
            _functions.DllStructSetData( v_tOFN, Variant.Create( "lpstrFilter" ), _functions.DllStructGetPtr( v_tFilter ) );
            _functions.DllStructSetData( v_tOFN, Variant.Create( "nFilterIndex" ), v_iFilterIndex );
            _functions.DllStructSetData( v_tOFN, Variant.Create( "lpstrFile" ), _functions.DllStructGetPtr( v_tPath ) );
            _functions.DllStructSetData( v_tOFN, Variant.Create( "nMaxFile" ), v_iPathLen );
            _functions.DllStructSetData( v_tOFN, Variant.Create( "lpstrInitialDir" ), _functions.DllStructGetPtr( v_tInitialDir ) );
            _functions.DllStructSetData( v_tOFN, Variant.Create( "lpstrTitle" ), _functions.DllStructGetPtr( v_tTitle ) );
            _functions.DllStructSetData( v_tOFN, Variant.Create( "Flags" ), v_iFlag );
            _functions.DllStructSetData( v_tOFN, Variant.Create( "lpstrDefExt" ), _functions.DllStructGetPtr( v_tExtn ) );
            _functions.DllStructSetData( v_tOFN, Variant.Create( "FlagsEx" ), v_iFlagsEx );
            v_aRes = _functions.DllCall(
                Variant.Create( "comdlg32.dll" ), Variant.Create( "bool" ), Variant.Create( "GetOpenFileNameW" ), Variant.Create( "struct*" ), v_tOFN );
            if ( _functions.OR( _context.Error, _functions.NOT( v_aRes[Variant.Create( 0 )] ) ) ) {
                return _functions.SetError( _context.Error+Variant.Create( 10 ), _context.Extended, v_aFiles );
            }

            if ( _functions.AND(
                _functions.BitAND( v_iFlags, v_OFN_ALLOWMULTISELECT ) == v_OFN_ALLOWMULTISELECT, _functions.BitAND( v_iFlags, v_OFN_EXPLORER ) == v_OFN_EXPLORER ) ) {
                loopHandler2d28e401e4a34a0dac7476a0a7b95d67 = new ForToNextLooper( Variant.Create( 1 ), v_iPathLen, null );
                v_x = loopHandler2d28e401e4a34a0dac7476a0a7b95d67.Index;
                for ( ; loopHandler2d28e401e4a34a0dac7476a0a7b95d67.MoveNext(); v_x = loopHandler2d28e401e4a34a0dac7476a0a7b95d67.Index ) {
                    if ( _functions.DllStructGetData( v_tPath, Variant.Create( "Path" ), v_x ) == _functions.Chr( Variant.Create( 0 ) ) ) {
                        _functions.DllStructSetData( v_tPath, Variant.Create( "Path" ), Variant.Create( "|" ), v_x );
                        v_iNulls += Variant.Create( 1 );
                    }
                    else {
                        v_iNulls = Variant.Create( 0 );
                    }

                    if ( v_iNulls == Variant.Create( 2 ) ) {
                        goto ExitLooP_level_1_count_2;
                    }
                }

                ExitLooP_level_1_count_2:
                ;
                _functions.DllStructSetData( v_tPath, Variant.Create( "Path" ), _functions.Chr( Variant.Create( 0 ) ), v_x-Variant.Create( 1 ) );
                v_aFiles = _functions.StringSplit( _functions.DllStructGetData( v_tPath, Variant.Create( "Path" ) ), Variant.Create( "|" ) );
                if ( v_aFiles[Variant.Create( 0 )] == Variant.Create( 1 ) ) {
                    return f___WinAPI_ParseFileDialogPath( _functions.DllStructGetData( v_tPath, Variant.Create( "Path" ) ) );
                }

                return _functions.StringSplit( _functions.DllStructGetData( v_tPath, Variant.Create( "Path" ) ), Variant.Create( "|" ) );
            }
            if ( _functions.BitAND( v_iFlags, v_OFN_ALLOWMULTISELECT ) == v_OFN_ALLOWMULTISELECT ) {
                v_aFiles = _functions.StringSplit( _functions.DllStructGetData( v_tPath, Variant.Create( "Path" ) ), Variant.Create( " " ) );
                if ( v_aFiles[Variant.Create( 0 )] == Variant.Create( 1 ) ) {
                    return f___WinAPI_ParseFileDialogPath( _functions.DllStructGetData( v_tPath, Variant.Create( "Path" ) ) );
                }

                return
                    _functions.StringSplit(
                        _functions.StringReplace(
                            _functions.DllStructGetData( v_tPath, Variant.Create( "Path" ) ), Variant.Create( " " ), Variant.Create( "|" ) ),
                        Variant.Create( "|" ) );
            }
            return f___WinAPI_ParseFileDialogPath( _functions.DllStructGetData( v_tPath, Variant.Create( "Path" ) ) );

            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetOverlappedResult( Variant v_hFile, Variant v_pOverlapped, ref Variant v_iBytes, Variant v_bWait = null ) {
            Variant v_aResult = Variant.Create( (object) null );
            if ( v_bWait == null ) {
                v_bWait = Variant.Create( false );
            }
            v_aResult = _functions.DllCall(
                Variant.Create( "kernel32.dll" ), Variant.Create( "bool" ), Variant.Create( "GetOverlappedResult" ), Variant.Create( "handle" ), v_hFile,
                Variant.Create( "ptr" ), v_pOverlapped, Variant.Create( "dword*" ), Variant.Create( 0 ), Variant.Create( "bool" ), v_bWait );
            if ( _functions.OR( _context.Error, _functions.NOT( v_aResult[Variant.Create( 0 )] ) ) ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            v_iBytes = v_aResult[Variant.Create( 3 )];
            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetParent( Variant v_hWnd ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "hwnd" ), Variant.Create( "GetParent" ), Variant.Create( "hwnd" ), v_hWnd );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetProcAddress( Variant v_hModule, Variant v_vName ) {
            Variant v_sType = Variant.Create( (object) null );
            Variant v_aResult = Variant.Create( (object) null );
            v_sType = Variant.Create( "str" );
            if ( _functions.IsNumber( v_vName ) ) {
                v_sType = Variant.Create( "word" );
            }

            v_aResult = _functions.DllCall(
                Variant.Create( "kernel32.dll" ), Variant.Create( "ptr" ), Variant.Create( "GetProcAddress" ), Variant.Create( "handle" ), v_hModule, v_sType,
                v_vName );
            if ( _functions.OR( _context.Error, _functions.NOT( v_aResult[Variant.Create( 0 )] ) ) ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetProcessAffinityMask( Variant v_hProcess ) {
            Variant v_aResult = Variant.Create( (object) null );
            Variant v_aMask = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "kernel32.dll" ), Variant.Create( "bool" ), Variant.Create( "GetProcessAffinityMask" ), Variant.Create( "handle" ), v_hProcess,
                Variant.Create( "dword_ptr*" ), Variant.Create( 0 ), Variant.Create( "dword_ptr*" ), Variant.Create( 0 ) );
            if ( _functions.OR( _context.Error, _functions.NOT( v_aResult[Variant.Create( 0 )] ) ) ) {
                return _functions.SetError( _context.Error+Variant.Create( 10 ), _context.Extended, Variant.Create( 0 ) );
            }

            v_aMask = Variant.CreateArray( new Variant[Variant.Create( 3 )] );
            v_aMask[Variant.Create( 0 )] = Variant.Create( true );
            v_aMask[Variant.Create( 1 )] = v_aResult[Variant.Create( 2 )];
            v_aMask[Variant.Create( 2 )] = v_aResult[Variant.Create( 3 )];
            return v_aMask;
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetSaveFileName(
            Variant v_sTitle = null,
            Variant v_sFilter = null,
            Variant v_sInitalDir = null,
            Variant v_sDefaultFile = null,
            Variant v_sDefaultExt = null,
            Variant v_iFilterIndex = null,
            Variant v_iFlags = null,
            Variant v_iFlagsEx = null,
            Variant v_hWndOwner = null ) {
            Variant v_iPathLen = Variant.Create( (object) null );
            Variant v_tOFN = Variant.Create( (object) null );
            Variant v_aFiles = Variant.Create( (object) null );
            Variant v_iFlag = Variant.Create( (object) null );
            Variant v_asFLines = Variant.Create( (object) null );
            Variant v_asFilter = Variant.Create( (object) null );
            Variant v_iStart = Variant.Create( (object) null );
            Variant v_iFinal = Variant.Create( (object) null );
            Variant v_tagFilter = Variant.Create( (object) null );
            ForToNextLooper loopHandler6752c062581044549ba68655f593475a;
            Variant v_i = null;
            Variant v_tTitle = Variant.Create( (object) null );
            Variant v_tInitialDir = Variant.Create( (object) null );
            Variant v_tFilter = Variant.Create( (object) null );
            Variant v_tPath = Variant.Create( (object) null );
            Variant v_tExtn = Variant.Create( (object) null );
            ForToNextLooper loopHandlerfa869a4e4fd24068a2ecf271e66f3928;
            Variant v_aRes = Variant.Create( (object) null );
            if ( v_hWndOwner == null ) {
                v_hWndOwner = Variant.Create( 0 );
            }
            if ( v_iFlagsEx == null ) {
                v_iFlagsEx = Variant.Create( 0 );
            }
            if ( v_iFlags == null ) {
                v_iFlags = Variant.Create( 0 );
            }
            if ( v_iFilterIndex == null ) {
                v_iFilterIndex = Variant.Create( 1 );
            }
            if ( v_sDefaultExt == null ) {
                v_sDefaultExt = Variant.Create( "" );
            }
            if ( v_sDefaultFile == null ) {
                v_sDefaultFile = Variant.Create( "" );
            }
            if ( v_sInitalDir == null ) {
                v_sInitalDir = Variant.Create( "." );
            }
            if ( v_sFilter == null ) {
                v_sFilter = Variant.Create( "All files (*.*)" );
            }
            if ( v_sTitle == null ) {
                v_sTitle = Variant.Create( "" );
            }
            v_iPathLen = Variant.Create( 4096 );
            v_tOFN = _functions.DllStructCreate( v_tagOPENFILENAME );
            v_aFiles = Variant.CreateArray( new Variant[Variant.Create( 1 )] );
            v_aFiles.InitArray(
                new[] {
                    Variant.Create( 0 )
                }
                );
            v_iFlag = v_iFlags;
            v_asFLines = _functions.StringSplit( v_sFilter, Variant.Create( "|" ) );
            v_asFilter = Variant.CreateArray( new Variant[v_asFLines[Variant.Create( 0 )] * Variant.Create( 2 )+Variant.Create( 1 )] );
            v_asFilter[Variant.Create( 0 )] = v_asFLines[Variant.Create( 0 )] * Variant.Create( 2 );
            loopHandler6752c062581044549ba68655f593475a = new ForToNextLooper( Variant.Create( 1 ), v_asFLines[Variant.Create( 0 )], null );
            v_i = loopHandler6752c062581044549ba68655f593475a.Index;
            for ( ; loopHandler6752c062581044549ba68655f593475a.MoveNext(); v_i = loopHandler6752c062581044549ba68655f593475a.Index ) {
                v_iStart = _functions.StringInStr( v_asFLines[v_i], Variant.Create( "(" ), Variant.Create( 0 ), Variant.Create( 1 ) );
                v_iFinal = _functions.StringInStr( v_asFLines[v_i], Variant.Create( ")" ), Variant.Create( 0 ), Variant.Create( -1 ) );
                v_asFilter[v_i * Variant.Create( 2 )-Variant.Create( 1 )] =
                    _functions.StringStripWS( _functions.StringLeft( v_asFLines[v_i], v_iStart-Variant.Create( 1 ) ), v_STR_STRIPLEADING+v_STR_STRIPTRAILING );
                v_asFilter[v_i * Variant.Create( 2 )] =
                    _functions.StringStripWS(
                        _functions.StringTrimRight(
                            _functions.StringTrimLeft( v_asFLines[v_i], v_iStart ), _functions.StringLen( v_asFLines[v_i] )-v_iFinal+Variant.Create( 1 ) ),
                        v_STR_STRIPLEADING+v_STR_STRIPTRAILING );
                v_tagFilter =
                    v_tagFilter.ConcatAssign(
                        _functions.Concat(
                            _functions.Concat(
                                _functions.Concat(
                                    _functions.Concat(
                                        Variant.Create( "wchar[" ),
                                        _functions.StringLen( v_asFilter[v_i * Variant.Create( 2 )-Variant.Create( 1 )] )+Variant.Create( 1 ) ),
                                    Variant.Create( "];wchar[" ) ), _functions.StringLen( v_asFilter[v_i * Variant.Create( 2 )] )+Variant.Create( 1 ) ),
                            Variant.Create( "];" ) ) );
            }

            v_tTitle =
                _functions.DllStructCreate(
                    _functions.Concat(
                        _functions.Concat( Variant.Create( "wchar Title[" ), _functions.StringLen( v_sTitle )+Variant.Create( 1 ) ), Variant.Create( "]" ) ) );
            v_tInitialDir =
                _functions.DllStructCreate(
                    _functions.Concat(
                        _functions.Concat( Variant.Create( "wchar InitDir[" ), _functions.StringLen( v_sInitalDir )+Variant.Create( 1 ) ), Variant.Create( "]" ) ) );
            v_tFilter = _functions.DllStructCreate( _functions.Concat( v_tagFilter, Variant.Create( "wchar" ) ) );
            v_tPath = _functions.DllStructCreate( _functions.Concat( _functions.Concat( Variant.Create( "wchar Path[" ), v_iPathLen ), Variant.Create( "]" ) ) );
            v_tExtn =
                _functions.DllStructCreate(
                    _functions.Concat(
                        _functions.Concat( Variant.Create( "wchar Extension[" ), _functions.StringLen( v_sDefaultExt )+Variant.Create( 1 ) ),
                        Variant.Create( "]" ) ) );
            loopHandlerfa869a4e4fd24068a2ecf271e66f3928 = new ForToNextLooper( Variant.Create( 1 ), v_asFilter[Variant.Create( 0 )], null );
            v_i = loopHandlerfa869a4e4fd24068a2ecf271e66f3928.Index;
            for ( ; loopHandlerfa869a4e4fd24068a2ecf271e66f3928.MoveNext(); v_i = loopHandlerfa869a4e4fd24068a2ecf271e66f3928.Index ) {
                _functions.DllStructSetData( v_tFilter, v_i, v_asFilter[v_i] );
            }

            _functions.DllStructSetData( v_tTitle, Variant.Create( "Title" ), v_sTitle );
            _functions.DllStructSetData( v_tInitialDir, Variant.Create( "InitDir" ), v_sInitalDir );
            _functions.DllStructSetData( v_tPath, Variant.Create( "Path" ), v_sDefaultFile );
            _functions.DllStructSetData( v_tExtn, Variant.Create( "Extension" ), v_sDefaultExt );
            _functions.DllStructSetData( v_tOFN, Variant.Create( "StructSize" ), _functions.DllStructGetSize( v_tOFN ) );
            _functions.DllStructSetData( v_tOFN, Variant.Create( "hwndOwner" ), v_hWndOwner );
            _functions.DllStructSetData( v_tOFN, Variant.Create( "lpstrFilter" ), _functions.DllStructGetPtr( v_tFilter ) );
            _functions.DllStructSetData( v_tOFN, Variant.Create( "nFilterIndex" ), v_iFilterIndex );
            _functions.DllStructSetData( v_tOFN, Variant.Create( "lpstrFile" ), _functions.DllStructGetPtr( v_tPath ) );
            _functions.DllStructSetData( v_tOFN, Variant.Create( "nMaxFile" ), v_iPathLen );
            _functions.DllStructSetData( v_tOFN, Variant.Create( "lpstrInitialDir" ), _functions.DllStructGetPtr( v_tInitialDir ) );
            _functions.DllStructSetData( v_tOFN, Variant.Create( "lpstrTitle" ), _functions.DllStructGetPtr( v_tTitle ) );
            _functions.DllStructSetData( v_tOFN, Variant.Create( "Flags" ), v_iFlag );
            _functions.DllStructSetData( v_tOFN, Variant.Create( "lpstrDefExt" ), _functions.DllStructGetPtr( v_tExtn ) );
            _functions.DllStructSetData( v_tOFN, Variant.Create( "FlagsEx" ), v_iFlagsEx );
            v_aRes = _functions.DllCall(
                Variant.Create( "comdlg32.dll" ), Variant.Create( "bool" ), Variant.Create( "GetSaveFileNameW" ), Variant.Create( "struct*" ), v_tOFN );
            if ( _functions.OR( _context.Error, _functions.NOT( v_aRes[Variant.Create( 0 )] ) ) ) {
                return _functions.SetError( _context.Error+Variant.Create( 10 ), _context.Extended, v_aFiles );
            }

            return f___WinAPI_ParseFileDialogPath( _functions.DllStructGetData( v_tPath, Variant.Create( "Path" ) ) );
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetStockObject( Variant v_iObject ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "gdi32.dll" ), Variant.Create( "handle" ), Variant.Create( "GetStockObject" ), Variant.Create( "int" ), v_iObject );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetStdHandle( Variant v_iStdHandle ) {
            Variant v_aHandle = Variant.Create( (object) null );
            Variant v_aResult = Variant.Create( (object) null );
            if ( _functions.OR( v_iStdHandle < Variant.Create( 0 ), v_iStdHandle > Variant.Create( 2 ) ) ) {
                return _functions.SetError( Variant.Create( 2 ), Variant.Create( 0 ), Variant.Create( -1 ) );
            }

            v_aHandle = Variant.CreateArray( new Variant[Variant.Create( 3 )] );
            v_aHandle.InitArray(
                new[] {
                    Variant.Create( -10 ),
                    Variant.Create( -11 ),
                    Variant.Create( -12 )
                }
                );
            v_aResult = _functions.DllCall(
                Variant.Create( "kernel32.dll" ), Variant.Create( "handle" ), Variant.Create( "GetStdHandle" ), Variant.Create( "dword" ),
                v_aHandle[v_iStdHandle] );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( -1 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetSysColor( Variant v_iIndex ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "INT" ), Variant.Create( "GetSysColor" ), Variant.Create( "int" ), v_iIndex );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetSysColorBrush( Variant v_iIndex ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "handle" ), Variant.Create( "GetSysColorBrush" ), Variant.Create( "int" ), v_iIndex );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetSystemMetrics( Variant v_iIndex ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "int" ), Variant.Create( "GetSystemMetrics" ), Variant.Create( "int" ), v_iIndex );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetTextExtentPoint32( Variant v_hDC, Variant v_sText ) {
            Variant v_tSize = Variant.Create( (object) null );
            Variant v_iSize = Variant.Create( (object) null );
            Variant v_aRet = Variant.Create( (object) null );
            v_tSize = _functions.DllStructCreate( v_tagSIZE );
            v_iSize = _functions.StringLen( v_sText );
            v_aRet = _functions.DllCall(
                Variant.Create( "gdi32.dll" ), Variant.Create( "bool" ), Variant.Create( "GetTextExtentPoint32W" ), Variant.Create( "handle" ), v_hDC,
                Variant.Create( "wstr" ), v_sText, Variant.Create( "int" ), v_iSize, Variant.Create( "struct*" ), v_tSize );
            if ( _functions.OR( _context.Error, _functions.NOT( v_aRet[Variant.Create( 0 )] ) ) ) {
                return _functions.SetError( _context.Error+Variant.Create( 10 ), _context.Extended, Variant.Create( 0 ) );
            }

            return v_tSize;
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetTextMetrics( Variant v_hDC ) {
            Variant v_tTEXTMETRIC = Variant.Create( (object) null );
            Variant v_aRet = Variant.Create( (object) null );
            v_tTEXTMETRIC = _functions.DllStructCreate( v_tagTEXTMETRIC );
            v_aRet = _functions.DllCall(
                Variant.Create( "gdi32.dll" ), Variant.Create( "bool" ), Variant.Create( "GetTextMetricsW" ), Variant.Create( "handle" ), v_hDC,
                Variant.Create( "struct*" ), v_tTEXTMETRIC );
            if ( _functions.OR( _context.Error, _functions.NOT( v_aRet[Variant.Create( 0 )] ) ) ) {
                return _functions.SetError( _context.Error+Variant.Create( 10 ), _context.Extended, Variant.Create( 0 ) );
            }

            return v_tTEXTMETRIC;
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetWindow( Variant v_hWnd, Variant v_iCmd ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "hwnd" ), Variant.Create( "GetWindow" ), Variant.Create( "hwnd" ), v_hWnd,
                Variant.Create( "uint" ), v_iCmd );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetWindowDC( Variant v_hWnd ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "handle" ), Variant.Create( "GetWindowDC" ), Variant.Create( "hwnd" ), v_hWnd );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetWindowHeight( Variant v_hWnd ) {
            Variant v_tRect = Variant.Create( (object) null );
            v_tRect = f__WinAPI_GetWindowRect( v_hWnd );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return _functions.DllStructGetData( v_tRect, Variant.Create( "Bottom" ) )-_functions.DllStructGetData( v_tRect, Variant.Create( "Top" ) );
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetWindowLong( Variant v_hWnd, Variant v_iIndex ) {
            Variant v_sFuncName = Variant.Create( (object) null );
            Variant v_aResult = Variant.Create( (object) null );
            v_sFuncName = Variant.Create( "GetWindowLongW" );
            if ( _context.AutoItX64 ) {
                v_sFuncName = Variant.Create( "GetWindowLongPtrW" );
            }

            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "long_ptr" ), v_sFuncName, Variant.Create( "hwnd" ), v_hWnd, Variant.Create( "int" ), v_iIndex );
            if ( _functions.OR( _context.Error, _functions.NOT( v_aResult[Variant.Create( 0 )] ) ) ) {
                return _functions.SetError( _context.Error+Variant.Create( 10 ), _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetWindowPlacement( Variant v_hWnd ) {
            Variant v_tWindowPlacement = Variant.Create( (object) null );
            Variant v_aRet = Variant.Create( (object) null );
            v_tWindowPlacement = _functions.DllStructCreate( v_tagWINDOWPLACEMENT );
            _functions.DllStructSetData( v_tWindowPlacement, Variant.Create( "length" ), _functions.DllStructGetSize( v_tWindowPlacement ) );
            v_aRet = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "bool" ), Variant.Create( "GetWindowPlacement" ), Variant.Create( "hwnd" ), v_hWnd,
                Variant.Create( "struct*" ), v_tWindowPlacement );
            if ( _functions.OR( _context.Error, _functions.NOT( v_aRet[Variant.Create( 0 )] ) ) ) {
                return _functions.SetError( _context.Error+Variant.Create( 10 ), _context.Extended, Variant.Create( 0 ) );
            }

            return v_tWindowPlacement;
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetWindowRect( Variant v_hWnd ) {
            Variant v_tRect = Variant.Create( (object) null );
            Variant v_aRet = Variant.Create( (object) null );
            v_tRect = _functions.DllStructCreate( v_tagRECT );
            v_aRet = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "bool" ), Variant.Create( "GetWindowRect" ), Variant.Create( "hwnd" ), v_hWnd,
                Variant.Create( "struct*" ), v_tRect );
            if ( _functions.OR( _context.Error, _functions.NOT( v_aRet[Variant.Create( 0 )] ) ) ) {
                return _functions.SetError( _context.Error+Variant.Create( 10 ), _context.Extended, Variant.Create( 0 ) );
            }

            return v_tRect;
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetWindowRgn( Variant v_hWnd, Variant v_hRgn ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "int" ), Variant.Create( "GetWindowRgn" ), Variant.Create( "hwnd" ), v_hWnd,
                Variant.Create( "handle" ), v_hRgn );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetWindowText( Variant v_hWnd ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "int" ), Variant.Create( "GetWindowTextW" ), Variant.Create( "hwnd" ), v_hWnd,
                Variant.Create( "wstr" ), Variant.Create( "" ), Variant.Create( "int" ), Variant.Create( 4096 ) );
            if ( _functions.OR( _context.Error, _functions.NOT( v_aResult[Variant.Create( 0 )] ) ) ) {
                return _functions.SetError( _context.Error+Variant.Create( 10 ), _context.Extended, Variant.Create( "" ) );
            }

            return _functions.SetExtended( v_aResult[Variant.Create( 0 )], v_aResult[Variant.Create( 2 )] );
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetWindowThreadProcessId( Variant v_hWnd, ref Variant v_iPID ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "dword" ), Variant.Create( "GetWindowThreadProcessId" ), Variant.Create( "hwnd" ), v_hWnd,
                Variant.Create( "dword*" ), Variant.Create( 0 ) );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            v_iPID = v_aResult[Variant.Create( 2 )];
            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetWindowWidth( Variant v_hWnd ) {
            Variant v_tRect = Variant.Create( (object) null );
            v_tRect = f__WinAPI_GetWindowRect( v_hWnd );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return _functions.DllStructGetData( v_tRect, Variant.Create( "Right" ) )-_functions.DllStructGetData( v_tRect, Variant.Create( "Left" ) );
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GetXYFromPoint( ref Variant v_tPoint, ref Variant v_iX, ref Variant v_iY ) {
            v_iX = _functions.DllStructGetData( v_tPoint, Variant.Create( "X" ) );
            v_iY = _functions.DllStructGetData( v_tPoint, Variant.Create( "Y" ) );
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GlobalMemoryStatus() {
            Variant v_tMem = Variant.Create( (object) null );
            Variant v_aRet = Variant.Create( (object) null );
            Variant v_aMem = Variant.Create( (object) null );
            v_tMem = _functions.DllStructCreate( v_tagMEMORYSTATUSEX );
            _functions.DllStructSetData( v_tMem, Variant.Create( 1 ), _functions.DllStructGetSize( v_tMem ) );
            v_aRet = _functions.DllCall(
                Variant.Create( "kernel32.dll" ), Variant.Create( "bool" ), Variant.Create( "GlobalMemoryStatusEx" ), Variant.Create( "struct*" ), v_tMem );
            if ( _functions.OR( _context.Error, _functions.NOT( v_aRet[Variant.Create( 0 )] ) ) ) {
                return _functions.SetError( _context.Error+Variant.Create( 10 ), _context.Extended, Variant.Create( 0 ) );
            }

            v_aMem = Variant.CreateArray( new Variant[Variant.Create( 7 )] );
            v_aMem[Variant.Create( 0 )] = _functions.DllStructGetData( v_tMem, Variant.Create( 2 ) );
            v_aMem[Variant.Create( 1 )] = _functions.DllStructGetData( v_tMem, Variant.Create( 3 ) );
            v_aMem[Variant.Create( 2 )] = _functions.DllStructGetData( v_tMem, Variant.Create( 4 ) );
            v_aMem[Variant.Create( 3 )] = _functions.DllStructGetData( v_tMem, Variant.Create( 5 ) );
            v_aMem[Variant.Create( 4 )] = _functions.DllStructGetData( v_tMem, Variant.Create( 6 ) );
            v_aMem[Variant.Create( 5 )] = _functions.DllStructGetData( v_tMem, Variant.Create( 7 ) );
            v_aMem[Variant.Create( 6 )] = _functions.DllStructGetData( v_tMem, Variant.Create( 8 ) );
            return v_aMem;
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GUIDFromString( Variant v_sGUID ) {
            Variant v_tGUID = Variant.Create( (object) null );
            v_tGUID = _functions.DllStructCreate( v_tagGUID );
            f__WinAPI_GUIDFromStringEx( v_sGUID, v_tGUID );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error+Variant.Create( 10 ), _context.Extended, Variant.Create( 0 ) );
            }

            return v_tGUID;
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_GUIDFromStringEx( Variant v_sGUID, Variant v_pGUID ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "ole32.dll" ), Variant.Create( "long" ), Variant.Create( "CLSIDFromString" ), Variant.Create( "wstr" ), v_sGUID,
                Variant.Create( "struct*" ), v_pGUID );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_HiWord( Variant v_iLong ) {
            return _functions.BitShift( v_iLong, Variant.Create( 16 ) );
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_InProcess( Variant v_hWnd, ref Variant v_hLastWnd ) {
            ForToNextLooper loopHandlerf97e87bf4add43d3a6e10a18d4045c82;
            Variant v_iI = null;
            Variant v_iPID = Variant.Create( (object) null );
            Variant v_iCount = Variant.Create( (object) null );
            if ( v_hWnd == v_hLastWnd ) {
                return Variant.Create( true );
            }

            loopHandlerf97e87bf4add43d3a6e10a18d4045c82 = new ForToNextLooper(
                v___g_aInProcess_WinAPI[Variant.Create( 0 ), Variant.Create( 0 )], Variant.Create( 1 ), Variant.Create( -1 ) );
            v_iI = loopHandlerf97e87bf4add43d3a6e10a18d4045c82.Index;
            for ( ; loopHandlerf97e87bf4add43d3a6e10a18d4045c82.MoveNext(); v_iI = loopHandlerf97e87bf4add43d3a6e10a18d4045c82.Index ) {
                if ( v_hWnd == v___g_aInProcess_WinAPI[v_iI, Variant.Create( 0 )] ) {
                    if ( v___g_aInProcess_WinAPI[v_iI, Variant.Create( 1 )] ) {
                        v_hLastWnd = v_hWnd;
                        return Variant.Create( true );
                    }
                    return Variant.Create( false );
                }
            }

            f__WinAPI_GetWindowThreadProcessId( v_hWnd, ref v_iPID );
            v_iCount = v___g_aInProcess_WinAPI[Variant.Create( 0 ), Variant.Create( 0 )]+Variant.Create( 1 );
            if ( v_iCount >= Variant.Create( 64 ) ) {
                v_iCount = Variant.Create( 1 );
            }

            v___g_aInProcess_WinAPI[Variant.Create( 0 ), Variant.Create( 0 )] = v_iCount;
            v___g_aInProcess_WinAPI[v_iCount, Variant.Create( 0 )] = v_hWnd;
            v___g_aInProcess_WinAPI[v_iCount, Variant.Create( 1 )] = v_iPID == _context.AutoItPID;
            return v___g_aInProcess_WinAPI[v_iCount, Variant.Create( 1 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_IntToFloat( Variant v_iInt ) {
            Variant v_tInt = Variant.Create( (object) null );
            Variant v_tFloat = Variant.Create( (object) null );
            v_tInt = _functions.DllStructCreate( Variant.Create( "int" ) );
            v_tFloat = _functions.DllStructCreate( Variant.Create( "float" ), _functions.DllStructGetPtr( v_tInt ) );
            _functions.DllStructSetData( v_tInt, Variant.Create( 1 ), v_iInt );
            return _functions.DllStructGetData( v_tFloat, Variant.Create( 1 ) );
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_IsClassName( Variant v_hWnd, Variant v_sClassName ) {
            Variant v_sSeparator = Variant.Create( (object) null );
            Variant v_aClassName = Variant.Create( (object) null );
            Variant v_sClassCheck = Variant.Create( (object) null );
            ForToNextLooper loopHandler7888529306b645d68724faa27e8c4ae8;
            Variant v_x = null;
            v_sSeparator = _functions.Opt( Variant.Create( "GUIDataSeparatorChar" ) );
            v_aClassName = _functions.StringSplit( v_sClassName, v_sSeparator );
            if ( _functions.NOT( _functions.IsHWnd( v_hWnd ) ) ) {
                v_hWnd = _functions.GUICtrlGetHandle( v_hWnd );
            }

            v_sClassCheck = f__WinAPI_GetClassName( v_hWnd );
            loopHandler7888529306b645d68724faa27e8c4ae8 = new ForToNextLooper(
                Variant.Create( 1 ), _functions.UBound( v_aClassName )-Variant.Create( 1 ), null );
            v_x = loopHandler7888529306b645d68724faa27e8c4ae8.Index;
            for ( ; loopHandler7888529306b645d68724faa27e8c4ae8.MoveNext(); v_x = loopHandler7888529306b645d68724faa27e8c4ae8.Index ) {
                if ( _functions.StringUpper( _functions.StringMid( v_sClassCheck, Variant.Create( 1 ), _functions.StringLen( v_aClassName[v_x] ) ) ) ==
                     _functions.StringUpper( v_aClassName[v_x] ) ) {
                    return Variant.Create( true );
                }
            }

            return Variant.Create( false );
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_IsWindow( Variant v_hWnd ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "bool" ), Variant.Create( "IsWindow" ), Variant.Create( "hwnd" ), v_hWnd );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_IsWindowVisible( Variant v_hWnd ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "bool" ), Variant.Create( "IsWindowVisible" ), Variant.Create( "hwnd" ), v_hWnd );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_InvalidateRect( Variant v_hWnd, Variant v_tRect = null, Variant v_bErase = null ) {
            Variant v_aResult = Variant.Create( (object) null );
            if ( v_bErase == null ) {
                v_bErase = Variant.Create( true );
            }
            if ( v_tRect == null ) {
                v_tRect = Variant.Create( 0 );
            }
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "bool" ), Variant.Create( "InvalidateRect" ), Variant.Create( "hwnd" ), v_hWnd,
                Variant.Create( "struct*" ), v_tRect, Variant.Create( "bool" ), v_bErase );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_LineTo( Variant v_hDC, Variant v_iX, Variant v_iY ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "gdi32.dll" ), Variant.Create( "bool" ), Variant.Create( "LineTo" ), Variant.Create( "handle" ), v_hDC, Variant.Create( "int" ),
                v_iX, Variant.Create( "int" ), v_iY );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_LoadBitmap( Variant v_hInstance, Variant v_sBitmap ) {
            Variant v_sBitmapType = Variant.Create( (object) null );
            Variant v_aResult = Variant.Create( (object) null );
            v_sBitmapType = Variant.Create( "int" );
            if ( _functions.IsString( v_sBitmap ) ) {
                v_sBitmapType = Variant.Create( "wstr" );
            }

            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "handle" ), Variant.Create( "LoadBitmapW" ), Variant.Create( "handle" ), v_hInstance,
                v_sBitmapType, v_sBitmap );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_LoadImage( Variant v_hInstance, Variant v_sImage, Variant v_iType, Variant v_iXDesired, Variant v_iYDesired, Variant v_iLoad ) {
            Variant v_aResult = Variant.Create( (object) null );
            Variant v_sImageType = Variant.Create( (object) null );
            v_sImageType = Variant.Create( "int" );
            if ( _functions.IsString( v_sImage ) ) {
                v_sImageType = Variant.Create( "wstr" );
            }

            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "handle" ), Variant.Create( "LoadImageW" ), Variant.Create( "handle" ), v_hInstance,
                v_sImageType, v_sImage, Variant.Create( "uint" ), v_iType, Variant.Create( "int" ), v_iXDesired, Variant.Create( "int" ), v_iYDesired,
                Variant.Create( "uint" ), v_iLoad );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_LoadLibrary( Variant v_sFileName ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "kernel32.dll" ), Variant.Create( "handle" ), Variant.Create( "LoadLibraryW" ), Variant.Create( "wstr" ), v_sFileName );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_LoadLibraryEx( Variant v_sFileName, Variant v_iFlags = null ) {
            Variant v_aResult = Variant.Create( (object) null );
            if ( v_iFlags == null ) {
                v_iFlags = Variant.Create( 0 );
            }
            v_aResult = _functions.DllCall(
                Variant.Create( "kernel32.dll" ), Variant.Create( "handle" ), Variant.Create( "LoadLibraryExW" ), Variant.Create( "wstr" ), v_sFileName,
                Variant.Create( "ptr" ), Variant.Create( 0 ), Variant.Create( "dword" ), v_iFlags );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_LoadShell32Icon( Variant v_iIconID ) {
            Variant v_tIcons = Variant.Create( (object) null );
            Variant v_iIcons = Variant.Create( (object) null );
            v_tIcons = _functions.DllStructCreate( Variant.Create( "ptr Data" ) );
            v_iIcons = f__WinAPI_ExtractIconEx( Variant.Create( "shell32.dll" ), v_iIconID, Variant.Create( 0 ), v_tIcons, Variant.Create( 1 ) );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            if ( v_iIcons <= Variant.Create( 0 ) ) {
                return _functions.SetError( Variant.Create( 10 ), Variant.Create( 0 ), Variant.Create( 0 ) );
            }

            return _functions.DllStructGetData( v_tIcons, Variant.Create( "Data" ) );
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_LoadString( Variant v_hInstance, Variant v_iStringId ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "int" ), Variant.Create( "LoadStringW" ), Variant.Create( "handle" ), v_hInstance,
                Variant.Create( "uint" ), v_iStringId, Variant.Create( "wstr" ), Variant.Create( "" ), Variant.Create( "int" ), Variant.Create( 4096 ) );
            if ( _functions.OR( _context.Error, _functions.NOT( v_aResult[Variant.Create( 0 )] ) ) ) {
                return _functions.SetError( _context.Error+Variant.Create( 10 ), _context.Extended, Variant.Create( "" ) );
            }

            return _functions.SetExtended( v_aResult[Variant.Create( 0 )], v_aResult[Variant.Create( 3 )] );
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_LocalFree( Variant v_hMem ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "kernel32.dll" ), Variant.Create( "handle" ), Variant.Create( "LocalFree" ), Variant.Create( "handle" ), v_hMem );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_LoWord( Variant v_iLong ) {
            return _functions.BitAND( v_iLong, Variant.Create( 65535 ) );
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_MAKELANGID( Variant v_iPrimary, Variant v_iSub ) {
            return _functions.BitOR( _functions.BitShift( v_iSub, Variant.Create( -10 ) ), v_iPrimary );
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_MAKELCID( Variant v_iLgid, Variant v_iSrtid ) {
            return _functions.BitOR( _functions.BitShift( v_iSrtid, Variant.Create( -16 ) ), v_iLgid );
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_MakeLong( Variant v_iLo, Variant v_iHi ) {
            return _functions.BitOR( _functions.BitShift( v_iHi, Variant.Create( -16 ) ), _functions.BitAND( v_iLo, Variant.Create( 65535 ) ) );
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_MakeQWord( Variant v_iLoDWORD, Variant v_iHiDWORD ) {
            Variant v_tInt64 = Variant.Create( (object) null );
            Variant v_tDwords = Variant.Create( (object) null );
            v_tInt64 = _functions.DllStructCreate( Variant.Create( "uint64" ) );
            v_tDwords = _functions.DllStructCreate( Variant.Create( "dword;dword" ), _functions.DllStructGetPtr( v_tInt64 ) );
            _functions.DllStructSetData( v_tDwords, Variant.Create( 1 ), v_iLoDWORD );
            _functions.DllStructSetData( v_tDwords, Variant.Create( 2 ), v_iHiDWORD );
            return _functions.DllStructGetData( v_tInt64, Variant.Create( 1 ) );
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_MessageBeep( Variant v_iType = null ) {
            Variant v_iSound = Variant.Create( (object) null );
            Variant v_aResult = Variant.Create( (object) null );
            if ( v_iType == null ) {
                v_iType = Variant.Create( 1 );
            }
            if ( _functions.Equal( v_iType, Variant.Create( 1 ) ) ) {
                v_iSound = Variant.Create( 0 );
            }
            else if ( _functions.Equal( v_iType, Variant.Create( 2 ) ) ) {
                v_iSound = Variant.Create( 16 );
            }
            else if ( _functions.Equal( v_iType, Variant.Create( 3 ) ) ) {
                v_iSound = Variant.Create( 32 );
            }
            else if ( _functions.Equal( v_iType, Variant.Create( 4 ) ) ) {
                v_iSound = Variant.Create( 48 );
            }
            else if ( _functions.Equal( v_iType, Variant.Create( 5 ) ) ) {
                v_iSound = Variant.Create( 64 );
            }
            else {
                v_iSound = Variant.Create( -1 );
            }

            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "bool" ), Variant.Create( "MessageBeep" ), Variant.Create( "uint" ), v_iSound );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_MsgBox( Variant v_iFlags, Variant v_sTitle, Variant v_sText ) {
            _functions.BlockInput( Variant.Create( 0 ) );
            _functions.MsgBox( v_iFlags, v_sTitle, _functions.Concat( v_sText, Variant.Create( "      " ) ) );
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_Mouse_Event( Variant v_iFlags, Variant v_iX = null, Variant v_iY = null, Variant v_iData = null, Variant v_iExtraInfo = null ) {
            if ( v_iExtraInfo == null ) {
                v_iExtraInfo = Variant.Create( 0 );
            }
            if ( v_iData == null ) {
                v_iData = Variant.Create( 0 );
            }
            if ( v_iY == null ) {
                v_iY = Variant.Create( 0 );
            }
            if ( v_iX == null ) {
                v_iX = Variant.Create( 0 );
            }
            _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "none" ), Variant.Create( "mouse_event" ), Variant.Create( "dword" ), v_iFlags,
                Variant.Create( "dword" ), v_iX, Variant.Create( "dword" ), v_iY, Variant.Create( "dword" ), v_iData, Variant.Create( "ulong_ptr" ),
                v_iExtraInfo );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended );
            }

            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_MoveTo( Variant v_hDC, Variant v_iX, Variant v_iY ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "gdi32.dll" ), Variant.Create( "bool" ), Variant.Create( "MoveToEx" ), Variant.Create( "handle" ), v_hDC,
                Variant.Create( "int" ), v_iX, Variant.Create( "int" ), v_iY, Variant.Create( "ptr" ), Variant.Create( 0 ) );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_MoveWindow( Variant v_hWnd, Variant v_iX, Variant v_iY, Variant v_iWidth, Variant v_iHeight, Variant v_bRepaint = null ) {
            Variant v_aResult = Variant.Create( (object) null );
            if ( v_bRepaint == null ) {
                v_bRepaint = Variant.Create( true );
            }
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "bool" ), Variant.Create( "MoveWindow" ), Variant.Create( "hwnd" ), v_hWnd,
                Variant.Create( "int" ), v_iX, Variant.Create( "int" ), v_iY, Variant.Create( "int" ), v_iWidth, Variant.Create( "int" ), v_iHeight,
                Variant.Create( "bool" ), v_bRepaint );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_MulDiv( Variant v_iNumber, Variant v_iNumerator, Variant v_iDenominator ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "kernel32.dll" ), Variant.Create( "int" ), Variant.Create( "MulDiv" ), Variant.Create( "int" ), v_iNumber,
                Variant.Create( "int" ), v_iNumerator, Variant.Create( "int" ), v_iDenominator );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( -1 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_MultiByteToWideChar( Variant v_sText, Variant v_iCodePage = null, Variant v_iFlags = null, Variant v_bRetString = null ) {
            Variant v_sTextType = Variant.Create( (object) null );
            Variant v_aResult = Variant.Create( (object) null );
            Variant v_iOut = Variant.Create( (object) null );
            Variant v_tOut = Variant.Create( (object) null );
            if ( v_bRetString == null ) {
                v_bRetString = Variant.Create( false );
            }
            if ( v_iFlags == null ) {
                v_iFlags = Variant.Create( 0 );
            }
            if ( v_iCodePage == null ) {
                v_iCodePage = Variant.Create( 0 );
            }
            v_sTextType = Variant.Create( "str" );
            if ( _functions.NOT( _functions.IsString( v_sText ) ) ) {
                v_sTextType = Variant.Create( "struct*" );
            }

            v_aResult = _functions.DllCall(
                Variant.Create( "kernel32.dll" ), Variant.Create( "int" ), Variant.Create( "MultiByteToWideChar" ), Variant.Create( "uint" ), v_iCodePage,
                Variant.Create( "dword" ), v_iFlags, v_sTextType, v_sText, Variant.Create( "int" ), Variant.Create( -1 ), Variant.Create( "ptr" ),
                Variant.Create( 0 ), Variant.Create( "int" ), Variant.Create( 0 ) );
            if ( _functions.OR( _context.Error, _functions.NOT( v_aResult[Variant.Create( 0 )] ) ) ) {
                return _functions.SetError( _context.Error+Variant.Create( 10 ), _context.Extended, Variant.Create( 0 ) );
            }

            v_iOut = v_aResult[Variant.Create( 0 )];
            v_tOut = _functions.DllStructCreate( _functions.Concat( _functions.Concat( Variant.Create( "wchar[" ), v_iOut ), Variant.Create( "]" ) ) );
            v_aResult = _functions.DllCall(
                Variant.Create( "kernel32.dll" ), Variant.Create( "int" ), Variant.Create( "MultiByteToWideChar" ), Variant.Create( "uint" ), v_iCodePage,
                Variant.Create( "dword" ), v_iFlags, v_sTextType, v_sText, Variant.Create( "int" ), Variant.Create( -1 ), Variant.Create( "struct*" ), v_tOut,
                Variant.Create( "int" ), v_iOut );
            if ( _functions.OR( _context.Error, _functions.NOT( v_aResult[Variant.Create( 0 )] ) ) ) {
                return _functions.SetError( _context.Error+Variant.Create( 20 ), _context.Extended, Variant.Create( 0 ) );
            }

            if ( v_bRetString ) {
                return _functions.DllStructGetData( v_tOut, Variant.Create( 1 ) );
            }

            return v_tOut;
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_MultiByteToWideCharEx( Variant v_sText, Variant v_pText, Variant v_iCodePage = null, Variant v_iFlags = null ) {
            Variant v_aResult = Variant.Create( (object) null );
            if ( v_iFlags == null ) {
                v_iFlags = Variant.Create( 0 );
            }
            if ( v_iCodePage == null ) {
                v_iCodePage = Variant.Create( 0 );
            }
            v_aResult = _functions.DllCall(
                Variant.Create( "kernel32.dll" ), Variant.Create( "int" ), Variant.Create( "MultiByteToWideChar" ), Variant.Create( "uint" ), v_iCodePage,
                Variant.Create( "dword" ), v_iFlags, Variant.Create( "STR" ), v_sText, Variant.Create( "int" ), Variant.Create( -1 ),
                Variant.Create( "struct*" ), v_pText, Variant.Create( "int" ), _functions.StringLen( v_sText )+Variant.Create( 1 ) * Variant.Create( 2 ) );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_OpenProcess( Variant v_iAccess, Variant v_bInherit, Variant v_iPID, Variant v_bDebugPriv = null ) {
            Variant v_aResult = Variant.Create( (object) null );
            Variant v_hToken = Variant.Create( (object) null );
            Variant v_iError = Variant.Create( (object) null );
            Variant v_iExtended = Variant.Create( (object) null );
            Variant v_iRet = Variant.Create( (object) null );
            if ( v_bDebugPriv == null ) {
                v_bDebugPriv = Variant.Create( false );
            }
            v_aResult = _functions.DllCall(
                Variant.Create( "kernel32.dll" ), Variant.Create( "handle" ), Variant.Create( "OpenProcess" ), Variant.Create( "dword" ), v_iAccess,
                Variant.Create( "bool" ), v_bInherit, Variant.Create( "dword" ), v_iPID );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            if ( v_aResult[Variant.Create( 0 )] ) {
                return v_aResult[Variant.Create( 0 )];
            }

            if ( _functions.NOT( v_bDebugPriv ) ) {
                return _functions.SetError( Variant.Create( 100 ), Variant.Create( 0 ), Variant.Create( 0 ) );
            }

            v_hToken = f__Security__OpenThreadTokenEx( _functions.BitOR( v_TOKEN_ADJUST_PRIVILEGES, v_TOKEN_QUERY ) );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error+Variant.Create( 10 ), _context.Extended, Variant.Create( 0 ) );
            }

            f__Security__SetPrivilege( v_hToken, Variant.Create( "SeDebugPrivilege" ), Variant.Create( true ) );
            v_iError = _context.Error;
            v_iExtended = _context.Extended;
            v_iRet = Variant.Create( 0 );
            if ( _functions.NOT( _context.Error ) ) {
                v_aResult = _functions.DllCall(
                    Variant.Create( "kernel32.dll" ), Variant.Create( "handle" ), Variant.Create( "OpenProcess" ), Variant.Create( "dword" ), v_iAccess,
                    Variant.Create( "bool" ), v_bInherit, Variant.Create( "dword" ), v_iPID );
                v_iError = _context.Error;
                v_iExtended = _context.Extended;
                if ( v_aResult[Variant.Create( 0 )] ) {
                    v_iRet = v_aResult[Variant.Create( 0 )];
                }

                f__Security__SetPrivilege( v_hToken, Variant.Create( "SeDebugPrivilege" ), Variant.Create( false ) );
                if ( _context.Error ) {
                    v_iError = _context.Error+Variant.Create( 20 );
                    v_iExtended = _context.Extended;
                }
            }
            else {
                v_iError = _context.Error+Variant.Create( 30 );
            }

            f__WinAPI_CloseHandle( v_hToken );
            return _functions.SetError( v_iError, v_iExtended, v_iRet );
            return Variant.Create( (object) null );
        }

        public Variant f___WinAPI_ParseFileDialogPath( Variant v_sPath ) {
            Variant v_aFiles = Variant.Create( (object) null );
            Variant v_sTemp = Variant.Create( (object) null );
            v_aFiles = Variant.CreateArray( new Variant[Variant.Create( 3 )] );
            v_aFiles[Variant.Create( 0 )] = Variant.Create( 2 );
            v_sTemp = _functions.StringMid(
                v_sPath, Variant.Create( 1 ),
                _functions.StringInStr( v_sPath, Variant.Create( "\\" ), Variant.Create( 0 ), Variant.Create( -1 ) )-Variant.Create( 1 ) );
            v_aFiles[Variant.Create( 1 )] = v_sTemp;
            v_aFiles[Variant.Create( 2 )] = _functions.StringMid(
                v_sPath, _functions.StringInStr( v_sPath, Variant.Create( "\\" ), Variant.Create( 0 ), Variant.Create( -1 ) )+Variant.Create( 1 ) );
            return v_aFiles;
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_PathFindOnPath( Variant v_sFile, Variant v_aExtraPaths = null, Variant v_sPathDelimiter = null ) {
            Variant v_iExtraCount = Variant.Create( (object) null );
            Variant v_tPaths = Variant.Create( (object) null );
            Variant v_tPathPtrs = Variant.Create( (object) null );
            Variant v_tagStruct = Variant.Create( (object) null );
            ForToNextLooper loopHandlere2e7366d1c344fd7909a92468c184e48;
            Variant v_i = null;
            Variant v_aResult = Variant.Create( (object) null );
            if ( v_sPathDelimiter == null ) {
                v_sPathDelimiter = _context.LF;
            }
            if ( v_aExtraPaths == null ) {
                v_aExtraPaths = Variant.Create( "" );
            }
            v_iExtraCount = Variant.Create( 0 );
            if ( _functions.IsString( v_aExtraPaths ) ) {
                if ( _functions.StringLen( v_aExtraPaths ) ) {
                    v_aExtraPaths = _functions.StringSplit( v_aExtraPaths, v_sPathDelimiter, v_STR_ENTIRESPLIT+v_STR_NOCOUNT );
                    v_iExtraCount = _functions.UBound( v_aExtraPaths, v_UBOUND_ROWS );
                }
            }
            else if ( _functions.IsArray( v_aExtraPaths ) ) {
                v_iExtraCount = _functions.UBound( v_aExtraPaths );
            }

            if ( v_iExtraCount ) {
                v_tagStruct = Variant.Create( "" );
                foreach (Variant v_path in v_aExtraPaths) {
                    v_tagStruct =
                        v_tagStruct.ConcatAssign(
                            _functions.Concat(
                                _functions.Concat( Variant.Create( "wchar[" ), _functions.StringLen( v_path )+Variant.Create( 1 ) ), Variant.Create( "];" ) ) );
                }

                v_tPaths = _functions.DllStructCreate( v_tagStruct );
                v_tPathPtrs =
                    _functions.DllStructCreate(
                        _functions.Concat( _functions.Concat( Variant.Create( "ptr[" ), v_iExtraCount+Variant.Create( 1 ) ), Variant.Create( "]" ) ) );
                loopHandlere2e7366d1c344fd7909a92468c184e48 = new ForToNextLooper( Variant.Create( 1 ), v_iExtraCount, null );
                v_i = loopHandlere2e7366d1c344fd7909a92468c184e48.Index;
                for ( ; loopHandlere2e7366d1c344fd7909a92468c184e48.MoveNext(); v_i = loopHandlere2e7366d1c344fd7909a92468c184e48.Index ) {
                    _functions.DllStructSetData( v_tPaths, v_i, v_aExtraPaths[v_i-Variant.Create( 1 )] );
                    _functions.DllStructSetData( v_tPathPtrs, Variant.Create( 1 ), _functions.DllStructGetPtr( v_tPaths, v_i ), v_i );
                }

                _functions.DllStructSetData( v_tPathPtrs, Variant.Create( 1 ), _functions.Ptr( Variant.Create( 0 ) ), v_iExtraCount+Variant.Create( 1 ) );
            }

            v_aResult = _functions.DllCall(
                Variant.Create( "shlwapi.dll" ), Variant.Create( "bool" ), Variant.Create( "PathFindOnPathW" ), Variant.Create( "wstr" ), v_sFile,
                Variant.Create( "struct*" ), v_tPathPtrs );
            if ( _functions.OR( _context.Error, _functions.NOT( v_aResult[Variant.Create( 0 )] ) ) ) {
                return _functions.SetError( _context.Error+Variant.Create( 10 ), _context.Extended, v_sFile );
            }

            return v_aResult[Variant.Create( 1 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_PointFromRect( ref Variant v_tRect, Variant v_bCenter = null ) {
            Variant v_iX1 = Variant.Create( (object) null );
            Variant v_iY1 = Variant.Create( (object) null );
            Variant v_iX2 = Variant.Create( (object) null );
            Variant v_iY2 = Variant.Create( (object) null );
            Variant v_tPoint = Variant.Create( (object) null );
            if ( v_bCenter == null ) {
                v_bCenter = Variant.Create( true );
            }
            v_iX1 = _functions.DllStructGetData( v_tRect, Variant.Create( "Left" ) );
            v_iY1 = _functions.DllStructGetData( v_tRect, Variant.Create( "Top" ) );
            v_iX2 = _functions.DllStructGetData( v_tRect, Variant.Create( "Right" ) );
            v_iY2 = _functions.DllStructGetData( v_tRect, Variant.Create( "Bottom" ) );
            if ( v_bCenter ) {
                v_iX1 = v_iX1+v_iX2-v_iX1 / Variant.Create( 2 );
                v_iY1 = v_iY1+v_iY2-v_iY1 / Variant.Create( 2 );
            }

            v_tPoint = _functions.DllStructCreate( v_tagPOINT );
            _functions.DllStructSetData( v_tPoint, Variant.Create( "X" ), v_iX1 );
            _functions.DllStructSetData( v_tPoint, Variant.Create( "Y" ), v_iY1 );
            return v_tPoint;
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_PostMessage( Variant v_hWnd, Variant v_iMsg, Variant v_iwParam, Variant v_ilParam ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "bool" ), Variant.Create( "PostMessage" ), Variant.Create( "hwnd" ), v_hWnd,
                Variant.Create( "uint" ), v_iMsg, Variant.Create( "wparam" ), v_iwParam, Variant.Create( "lparam" ), v_ilParam );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_PrimaryLangId( Variant v_iLgid ) {
            return _functions.BitAND( v_iLgid, Variant.Create( 1023 ) );
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_PtInRect( ref Variant v_tRect, ref Variant v_tPoint ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "bool" ), Variant.Create( "PtInRect" ), Variant.Create( "struct*" ), v_tRect,
                Variant.Create( "struct" ), v_tPoint );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_ReadFile( Variant v_hFile, Variant v_pBuffer, Variant v_iToRead, ref Variant v_iRead, Variant v_pOverlapped = null ) {
            Variant v_aResult = Variant.Create( (object) null );
            if ( v_pOverlapped == null ) {
                v_pOverlapped = Variant.Create( 0 );
            }
            v_aResult = _functions.DllCall(
                Variant.Create( "kernel32.dll" ), Variant.Create( "bool" ), Variant.Create( "ReadFile" ), Variant.Create( "handle" ), v_hFile,
                Variant.Create( "ptr" ), v_pBuffer, Variant.Create( "dword" ), v_iToRead, Variant.Create( "dword*" ), Variant.Create( 0 ),
                Variant.Create( "ptr" ), v_pOverlapped );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            v_iRead = v_aResult[Variant.Create( 4 )];
            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_ReadProcessMemory( Variant v_hProcess, Variant v_pBaseAddress, Variant v_pBuffer, Variant v_iSize, ref Variant v_iRead ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "kernel32.dll" ), Variant.Create( "bool" ), Variant.Create( "ReadProcessMemory" ), Variant.Create( "handle" ), v_hProcess,
                Variant.Create( "ptr" ), v_pBaseAddress, Variant.Create( "ptr" ), v_pBuffer, Variant.Create( "ulong_ptr" ), v_iSize,
                Variant.Create( "ulong_ptr*" ), Variant.Create( 0 ) );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            v_iRead = v_aResult[Variant.Create( 5 )];
            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_RectIsEmpty( ref Variant v_tRect ) {
            return
                _functions.AND(
                    _functions.AND(
                        _functions.AND(
                            _functions.DllStructGetData( v_tRect, Variant.Create( "Left" ) ) == Variant.Create( 0 ),
                            _functions.DllStructGetData( v_tRect, Variant.Create( "Top" ) ) == Variant.Create( 0 ) ),
                        _functions.DllStructGetData( v_tRect, Variant.Create( "Right" ) ) == Variant.Create( 0 ) ),
                    _functions.DllStructGetData( v_tRect, Variant.Create( "Bottom" ) ) == Variant.Create( 0 ) );
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_RedrawWindow( Variant v_hWnd, Variant v_tRect = null, Variant v_hRegion = null, Variant v_iFlags = null ) {
            Variant v_aResult = Variant.Create( (object) null );
            if ( v_iFlags == null ) {
                v_iFlags = Variant.Create( 5 );
            }
            if ( v_hRegion == null ) {
                v_hRegion = Variant.Create( 0 );
            }
            if ( v_tRect == null ) {
                v_tRect = Variant.Create( 0 );
            }
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "bool" ), Variant.Create( "RedrawWindow" ), Variant.Create( "hwnd" ), v_hWnd,
                Variant.Create( "struct*" ), v_tRect, Variant.Create( "handle" ), v_hRegion, Variant.Create( "uint" ), v_iFlags );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_RegisterWindowMessage( Variant v_sMessage ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "uint" ), Variant.Create( "RegisterWindowMessageW" ), Variant.Create( "wstr" ), v_sMessage );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_ReleaseCapture() {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall( Variant.Create( "user32.dll" ), Variant.Create( "bool" ), Variant.Create( "ReleaseCapture" ) );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_ReleaseDC( Variant v_hWnd, Variant v_hDC ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "int" ), Variant.Create( "ReleaseDC" ), Variant.Create( "hwnd" ), v_hWnd,
                Variant.Create( "handle" ), v_hDC );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_ScreenToClient( Variant v_hWnd, ref Variant v_tPoint ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "bool" ), Variant.Create( "ScreenToClient" ), Variant.Create( "hwnd" ), v_hWnd,
                Variant.Create( "struct*" ), v_tPoint );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_SelectObject( Variant v_hDC, Variant v_hGDIObj ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "gdi32.dll" ), Variant.Create( "handle" ), Variant.Create( "SelectObject" ), Variant.Create( "handle" ), v_hDC,
                Variant.Create( "handle" ), v_hGDIObj );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_SetBkColor( Variant v_hDC, Variant v_iColor ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "gdi32.dll" ), Variant.Create( "INT" ), Variant.Create( "SetBkColor" ), Variant.Create( "handle" ), v_hDC,
                Variant.Create( "INT" ), v_iColor );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( -1 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_SetBkMode( Variant v_hDC, Variant v_iBkMode ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "gdi32.dll" ), Variant.Create( "int" ), Variant.Create( "SetBkMode" ), Variant.Create( "handle" ), v_hDC,
                Variant.Create( "int" ), v_iBkMode );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_SetCapture( Variant v_hWnd ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "hwnd" ), Variant.Create( "SetCapture" ), Variant.Create( "hwnd" ), v_hWnd );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_SetCursor( Variant v_hCursor ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "handle" ), Variant.Create( "SetCursor" ), Variant.Create( "handle" ), v_hCursor );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_SetDefaultPrinter( Variant v_sPrinter ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "winspool.drv" ), Variant.Create( "bool" ), Variant.Create( "SetDefaultPrinterW" ), Variant.Create( "wstr" ), v_sPrinter );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_SetDIBits(
            Variant v_hDC,
            Variant v_hBmp,
            Variant v_iStartScan,
            Variant v_iScanLines,
            Variant v_pBits,
            Variant v_pBMI,
            Variant v_iColorUse = null ) {
            Variant v_aResult = Variant.Create( (object) null );
            if ( v_iColorUse == null ) {
                v_iColorUse = Variant.Create( 0 );
            }
            v_aResult = _functions.DllCall(
                Variant.Create( "gdi32.dll" ), Variant.Create( "int" ), Variant.Create( "SetDIBits" ), Variant.Create( "handle" ), v_hDC,
                Variant.Create( "handle" ), v_hBmp, Variant.Create( "uint" ), v_iStartScan, Variant.Create( "uint" ), v_iScanLines, Variant.Create( "ptr" ),
                v_pBits, Variant.Create( "ptr" ), v_pBMI, Variant.Create( "INT" ), v_iColorUse );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_SetEndOfFile( Variant v_hFile ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "kernel32.dll" ), Variant.Create( "bool" ), Variant.Create( "SetEndOfFile" ), Variant.Create( "handle" ), v_hFile );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_SetEvent( Variant v_hEvent ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "kernel32.dll" ), Variant.Create( "bool" ), Variant.Create( "SetEvent" ), Variant.Create( "handle" ), v_hEvent );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_SetFilePointer( Variant v_hFile, Variant v_iPos, Variant v_iMethod = null ) {
            Variant v_aResult = Variant.Create( (object) null );
            if ( v_iMethod == null ) {
                v_iMethod = Variant.Create( 0 );
            }
            v_aResult = _functions.DllCall(
                Variant.Create( "kernel32.dll" ), Variant.Create( "INT" ), Variant.Create( "SetFilePointer" ), Variant.Create( "handle" ), v_hFile,
                Variant.Create( "long" ), v_iPos, Variant.Create( "ptr" ), Variant.Create( 0 ), Variant.Create( "long" ), v_iMethod );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( -1 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_SetFocus( Variant v_hWnd ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "hwnd" ), Variant.Create( "SetFocus" ), Variant.Create( "hwnd" ), v_hWnd );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_SetFont( Variant v_hWnd, Variant v_hFont, Variant v_bRedraw = null ) {
            if ( v_bRedraw == null ) {
                v_bRedraw = Variant.Create( true );
            }
            f__SendMessage( v_hWnd, v___WINAPICONSTANT_WM_SETFONT, v_hFont, v_bRedraw, Variant.Create( 0 ), Variant.Create( "hwnd" ) );
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_SetHandleInformation( Variant v_hObject, Variant v_iMask, Variant v_iFlags ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "kernel32.dll" ), Variant.Create( "bool" ), Variant.Create( "SetHandleInformation" ), Variant.Create( "handle" ), v_hObject,
                Variant.Create( "dword" ), v_iMask, Variant.Create( "dword" ), v_iFlags );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_SetLayeredWindowAttributes(
            Variant v_hWnd,
            Variant v_iTranscolor,
            Variant v_iTransGUI = null,
            Variant v_iFlags = null,
            Variant v_bColorRef = null ) {
            Variant v_aResult = Variant.Create( (object) null );
            if ( v_bColorRef == null ) {
                v_bColorRef = Variant.Create( false );
            }
            if ( v_iFlags == null ) {
                v_iFlags = Variant.Create( 3 );
            }
            if ( v_iTransGUI == null ) {
                v_iTransGUI = Variant.Create( 255 );
            }
            if ( _functions.OR( _functions.OR( v_iFlags == Variant.Create( new Default() ), v_iFlags == Variant.Create( "" ) ), v_iFlags < Variant.Create( 0 ) ) ) {
                v_iFlags = Variant.Create( 3 );
            }

            if ( _functions.NOT( v_bColorRef ) ) {
                v_iTranscolor =
                    _functions.Int(
                        _functions.Concat(
                            _functions.Concat(
                                _functions.BinaryMid( v_iTranscolor, Variant.Create( 3 ), Variant.Create( 1 ) ),
                                _functions.BinaryMid( v_iTranscolor, Variant.Create( 2 ), Variant.Create( 1 ) ) ),
                            _functions.BinaryMid( v_iTranscolor, Variant.Create( 1 ), Variant.Create( 1 ) ) ) );
            }

            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "bool" ), Variant.Create( "SetLayeredWindowAttributes" ), Variant.Create( "hwnd" ), v_hWnd,
                Variant.Create( "INT" ), v_iTranscolor, Variant.Create( "byte" ), v_iTransGUI, Variant.Create( "dword" ), v_iFlags );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_SetParent( Variant v_hWndChild, Variant v_hWndParent ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "hwnd" ), Variant.Create( "SetParent" ), Variant.Create( "hwnd" ), v_hWndChild,
                Variant.Create( "hwnd" ), v_hWndParent );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_SetProcessAffinityMask( Variant v_hProcess, Variant v_iMask ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "kernel32.dll" ), Variant.Create( "bool" ), Variant.Create( "SetProcessAffinityMask" ), Variant.Create( "handle" ), v_hProcess,
                Variant.Create( "ulong_ptr" ), v_iMask );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_SetSysColors( Variant v_vElements, Variant v_vColors ) {
            Variant v_bIsEArray = Variant.Create( (object) null );
            Variant v_bIsCArray = Variant.Create( (object) null );
            Variant v_iElementNum = Variant.Create( (object) null );
            Variant v_tElements = Variant.Create( (object) null );
            Variant v_tColors = Variant.Create( (object) null );
            ForToNextLooper loopHandler3b46652f566a40769fe92d533ade131f;
            Variant v_x = null;
            ForToNextLooper loopHandler78067e16d52a4759aa4ced7e6913630d;
            Variant v_aResult = Variant.Create( (object) null );
            v_bIsEArray = _functions.IsArray( v_vElements );
            v_bIsCArray = _functions.IsArray( v_vColors );
            if ( _functions.NOT( _functions.AND( v_bIsCArray, _functions.NOT( v_bIsEArray ) ) ) ) {
                v_iElementNum = Variant.Create( 1 );
            }
            else if ( _functions.OR( v_bIsCArray, v_bIsEArray ) ) {
                if ( _functions.NOT( _functions.OR( v_bIsCArray, _functions.NOT( v_bIsEArray ) ) ) ) {
                    return _functions.SetError( Variant.Create( -1 ), Variant.Create( -1 ), Variant.Create( false ) );
                }

                if ( _functions.UBound( v_vElements ) != _functions.UBound( v_vColors ) ) {
                    return _functions.SetError( Variant.Create( -1 ), Variant.Create( -1 ), Variant.Create( false ) );
                }

                v_iElementNum = _functions.UBound( v_vElements );
            }

            v_tElements =
                _functions.DllStructCreate( _functions.Concat( _functions.Concat( Variant.Create( "int Element[" ), v_iElementNum ), Variant.Create( "]" ) ) );
            v_tColors =
                _functions.DllStructCreate( _functions.Concat( _functions.Concat( Variant.Create( "INT NewColor[" ), v_iElementNum ), Variant.Create( "]" ) ) );
            if ( _functions.NOT( v_bIsEArray ) ) {
                _functions.DllStructSetData( v_tElements, Variant.Create( "Element" ), v_vElements, Variant.Create( 1 ) );
            }
            else {
                loopHandler3b46652f566a40769fe92d533ade131f = new ForToNextLooper( Variant.Create( 0 ), v_iElementNum-Variant.Create( 1 ), null );
                v_x = loopHandler3b46652f566a40769fe92d533ade131f.Index;
                for ( ; loopHandler3b46652f566a40769fe92d533ade131f.MoveNext(); v_x = loopHandler3b46652f566a40769fe92d533ade131f.Index ) {
                    _functions.DllStructSetData( v_tElements, Variant.Create( "Element" ), v_vElements[v_x], v_x+Variant.Create( 1 ) );
                }
            }

            if ( _functions.NOT( v_bIsCArray ) ) {
                _functions.DllStructSetData( v_tColors, Variant.Create( "NewColor" ), v_vColors, Variant.Create( 1 ) );
            }
            else {
                loopHandler78067e16d52a4759aa4ced7e6913630d = new ForToNextLooper( Variant.Create( 0 ), v_iElementNum-Variant.Create( 1 ), null );
                v_x = loopHandler78067e16d52a4759aa4ced7e6913630d.Index;
                for ( ; loopHandler78067e16d52a4759aa4ced7e6913630d.MoveNext(); v_x = loopHandler78067e16d52a4759aa4ced7e6913630d.Index ) {
                    _functions.DllStructSetData( v_tColors, Variant.Create( "NewColor" ), v_vColors[v_x], v_x+Variant.Create( 1 ) );
                }
            }

            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "bool" ), Variant.Create( "SetSysColors" ), Variant.Create( "int" ), v_iElementNum,
                Variant.Create( "struct*" ), v_tElements, Variant.Create( "struct*" ), v_tColors );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_SetTextColor( Variant v_hDC, Variant v_iColor ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "gdi32.dll" ), Variant.Create( "INT" ), Variant.Create( "SetTextColor" ), Variant.Create( "handle" ), v_hDC,
                Variant.Create( "INT" ), v_iColor );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( -1 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_SetWindowLong( Variant v_hWnd, Variant v_iIndex, Variant v_iValue ) {
            Variant v_sFuncName = Variant.Create( (object) null );
            Variant v_aResult = Variant.Create( (object) null );
            f__WinAPI_SetLastError( Variant.Create( 0 ) );
            v_sFuncName = Variant.Create( "SetWindowLongW" );
            if ( _context.AutoItX64 ) {
                v_sFuncName = Variant.Create( "SetWindowLongPtrW" );
            }

            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "long_ptr" ), v_sFuncName, Variant.Create( "hwnd" ), v_hWnd, Variant.Create( "int" ), v_iIndex,
                Variant.Create( "long_ptr" ), v_iValue );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_SetWindowPlacement( Variant v_hWnd, Variant v_pWindowPlacement ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "bool" ), Variant.Create( "SetWindowPlacement" ), Variant.Create( "hwnd" ), v_hWnd,
                Variant.Create( "ptr" ), v_pWindowPlacement );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_SetWindowPos( Variant v_hWnd, Variant v_hAfter, Variant v_iX, Variant v_iY, Variant v_iCX, Variant v_iCY, Variant v_iFlags ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "bool" ), Variant.Create( "SetWindowPos" ), Variant.Create( "hwnd" ), v_hWnd,
                Variant.Create( "hwnd" ), v_hAfter, Variant.Create( "int" ), v_iX, Variant.Create( "int" ), v_iY, Variant.Create( "int" ), v_iCX,
                Variant.Create( "int" ), v_iCY, Variant.Create( "uint" ), v_iFlags );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_SetWindowRgn( Variant v_hWnd, Variant v_hRgn, Variant v_bRedraw = null ) {
            Variant v_aResult = Variant.Create( (object) null );
            if ( v_bRedraw == null ) {
                v_bRedraw = Variant.Create( true );
            }
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "int" ), Variant.Create( "SetWindowRgn" ), Variant.Create( "hwnd" ), v_hWnd,
                Variant.Create( "handle" ), v_hRgn, Variant.Create( "bool" ), v_bRedraw );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_SetWindowsHookEx( Variant v_idHook, Variant v_pFn, Variant v_hMod, Variant v_iThreadId = null ) {
            Variant v_aResult = Variant.Create( (object) null );
            if ( v_iThreadId == null ) {
                v_iThreadId = Variant.Create( 0 );
            }
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "handle" ), Variant.Create( "SetWindowsHookEx" ), Variant.Create( "int" ), v_idHook,
                Variant.Create( "ptr" ), v_pFn, Variant.Create( "handle" ), v_hMod, Variant.Create( "dword" ), v_iThreadId );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_SetWindowText( Variant v_hWnd, Variant v_sText ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "bool" ), Variant.Create( "SetWindowTextW" ), Variant.Create( "hwnd" ), v_hWnd,
                Variant.Create( "wstr" ), v_sText );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_ShowCursor( Variant v_bShow ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "int" ), Variant.Create( "ShowCursor" ), Variant.Create( "bool" ), v_bShow );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_ShowError( Variant v_sText, Variant v_bExit = null ) {
            if ( v_bExit == null ) {
                v_bExit = Variant.Create( true );
            }
            f__WinAPI_MsgBox( v_MB_SYSTEMMODAL, Variant.Create( "Error" ), v_sText );
            if ( v_bExit ) {
                _functions.Exit( 0 );
            }

            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_ShowMsg( Variant v_sText ) {
            f__WinAPI_MsgBox( v_MB_SYSTEMMODAL, Variant.Create( "Information" ), v_sText );
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_ShowWindow( Variant v_hWnd, Variant v_iCmdShow = null ) {
            Variant v_aResult = Variant.Create( (object) null );
            if ( v_iCmdShow == null ) {
                v_iCmdShow = Variant.Create( 5 );
            }
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "bool" ), Variant.Create( "ShowWindow" ), Variant.Create( "hwnd" ), v_hWnd,
                Variant.Create( "int" ), v_iCmdShow );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_StringFromGUID( Variant v_pGUID ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "ole32.dll" ), Variant.Create( "int" ), Variant.Create( "StringFromGUID2" ), Variant.Create( "struct*" ), v_pGUID,
                Variant.Create( "wstr" ), Variant.Create( "" ), Variant.Create( "int" ), Variant.Create( 40 ) );
            if ( _functions.OR( _context.Error, _functions.NOT( v_aResult[Variant.Create( 0 )] ) ) ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( "" ) );
            }

            return _functions.SetExtended( v_aResult[Variant.Create( 0 )], v_aResult[Variant.Create( 2 )] );
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_StringLenA( ref Variant v_tString ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "kernel32.dll" ), Variant.Create( "int" ), Variant.Create( "lstrlenA" ), Variant.Create( "struct*" ), v_tString );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_StringLenW( ref Variant v_tString ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "kernel32.dll" ), Variant.Create( "int" ), Variant.Create( "lstrlenW" ), Variant.Create( "struct*" ), v_tString );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_SubLangId( Variant v_iLgid ) {
            return _functions.BitShift( v_iLgid, Variant.Create( 10 ) );
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_SystemParametersInfo( Variant v_iAction, Variant v_iParam = null, Variant v_vParam = null, Variant v_iWinIni = null ) {
            Variant v_aResult = Variant.Create( (object) null );
            if ( v_iWinIni == null ) {
                v_iWinIni = Variant.Create( 0 );
            }
            if ( v_vParam == null ) {
                v_vParam = Variant.Create( 0 );
            }
            if ( v_iParam == null ) {
                v_iParam = Variant.Create( 0 );
            }
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "bool" ), Variant.Create( "SystemParametersInfoW" ), Variant.Create( "uint" ), v_iAction,
                Variant.Create( "uint" ), v_iParam, Variant.Create( "ptr" ), v_vParam, Variant.Create( "uint" ), v_iWinIni );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_TwipsPerPixelX() {
            Variant v_hDC = Variant.Create( (object) null );
            Variant v_iTwipsPerPixelX = Variant.Create( (object) null );
            v_hDC = f__WinAPI_GetDC( Variant.Create( 0 ) );
            v_iTwipsPerPixelX = Variant.Create( 1440 ) / f__WinAPI_GetDeviceCaps( v_hDC, v___WINAPICONSTANT_LOGPIXELSX );
            f__WinAPI_ReleaseDC( Variant.Create( 0 ), v_hDC );
            return v_iTwipsPerPixelX;
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_TwipsPerPixelY() {
            Variant v_hDC = Variant.Create( (object) null );
            Variant v_iTwipsPerPixelY = Variant.Create( (object) null );
            v_hDC = f__WinAPI_GetDC( Variant.Create( 0 ) );
            v_iTwipsPerPixelY = Variant.Create( 1440 ) / f__WinAPI_GetDeviceCaps( v_hDC, v___WINAPICONSTANT_LOGPIXELSY );
            f__WinAPI_ReleaseDC( Variant.Create( 0 ), v_hDC );
            return v_iTwipsPerPixelY;
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_UnhookWindowsHookEx( Variant v_hHk ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "bool" ), Variant.Create( "UnhookWindowsHookEx" ), Variant.Create( "handle" ), v_hHk );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_UpdateLayeredWindow(
            Variant v_hWnd,
            Variant v_hDCDest,
            Variant v_pPTDest,
            Variant v_pSize,
            Variant v_hDCSrce,
            Variant v_pPTSrce,
            Variant v_iRGB,
            Variant v_pBlend,
            Variant v_iFlags ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "bool" ), Variant.Create( "UpdateLayeredWindow" ), Variant.Create( "hwnd" ), v_hWnd,
                Variant.Create( "handle" ), v_hDCDest, Variant.Create( "ptr" ), v_pPTDest, Variant.Create( "ptr" ), v_pSize, Variant.Create( "handle" ),
                v_hDCSrce, Variant.Create( "ptr" ), v_pPTSrce, Variant.Create( "dword" ), v_iRGB, Variant.Create( "ptr" ), v_pBlend, Variant.Create( "dword" ),
                v_iFlags );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_UpdateWindow( Variant v_hWnd ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "bool" ), Variant.Create( "UpdateWindow" ), Variant.Create( "hwnd" ), v_hWnd );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_WaitForInputIdle( Variant v_hProcess, Variant v_iTimeout = null ) {
            Variant v_aResult = Variant.Create( (object) null );
            if ( v_iTimeout == null ) {
                v_iTimeout = Variant.Create( -1 );
            }
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "dword" ), Variant.Create( "WaitForInputIdle" ), Variant.Create( "handle" ), v_hProcess,
                Variant.Create( "dword" ), v_iTimeout );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_WaitForMultipleObjects( Variant v_iCount, Variant v_pHandles, Variant v_bWaitAll = null, Variant v_iTimeout = null ) {
            Variant v_aResult = Variant.Create( (object) null );
            if ( v_iTimeout == null ) {
                v_iTimeout = Variant.Create( -1 );
            }
            if ( v_bWaitAll == null ) {
                v_bWaitAll = Variant.Create( false );
            }
            v_aResult = _functions.DllCall(
                Variant.Create( "kernel32.dll" ), Variant.Create( "INT" ), Variant.Create( "WaitForMultipleObjects" ), Variant.Create( "dword" ), v_iCount,
                Variant.Create( "ptr" ), v_pHandles, Variant.Create( "bool" ), v_bWaitAll, Variant.Create( "dword" ), v_iTimeout );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( -1 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_WaitForSingleObject( Variant v_hHandle, Variant v_iTimeout = null ) {
            Variant v_aResult = Variant.Create( (object) null );
            if ( v_iTimeout == null ) {
                v_iTimeout = Variant.Create( -1 );
            }
            v_aResult = _functions.DllCall(
                Variant.Create( "kernel32.dll" ), Variant.Create( "INT" ), Variant.Create( "WaitForSingleObject" ), Variant.Create( "handle" ), v_hHandle,
                Variant.Create( "dword" ), v_iTimeout );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( -1 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_WideCharToMultiByte( Variant v_pUnicode, Variant v_iCodePage = null, Variant v_bRetString = null ) {
            Variant v_sUnicodeType = Variant.Create( (object) null );
            Variant v_aResult = Variant.Create( (object) null );
            Variant v_tMultiByte = Variant.Create( (object) null );
            if ( v_bRetString == null ) {
                v_bRetString = Variant.Create( true );
            }
            if ( v_iCodePage == null ) {
                v_iCodePage = Variant.Create( 0 );
            }
            v_sUnicodeType = Variant.Create( "wstr" );
            if ( _functions.NOT( _functions.IsString( v_pUnicode ) ) ) {
                v_sUnicodeType = Variant.Create( "struct*" );
            }

            v_aResult = _functions.DllCall(
                Variant.Create( "kernel32.dll" ), Variant.Create( "int" ), Variant.Create( "WideCharToMultiByte" ), Variant.Create( "uint" ), v_iCodePage,
                Variant.Create( "dword" ), Variant.Create( 0 ), v_sUnicodeType, v_pUnicode, Variant.Create( "int" ), Variant.Create( -1 ),
                Variant.Create( "ptr" ), Variant.Create( 0 ), Variant.Create( "int" ), Variant.Create( 0 ), Variant.Create( "ptr" ), Variant.Create( 0 ),
                Variant.Create( "ptr" ), Variant.Create( 0 ) );
            if ( _functions.OR( _context.Error, _functions.NOT( v_aResult[Variant.Create( 0 )] ) ) ) {
                return _functions.SetError( _context.Error+Variant.Create( 20 ), _context.Extended, Variant.Create( "" ) );
            }

            v_tMultiByte =
                _functions.DllStructCreate(
                    _functions.Concat( _functions.Concat( Variant.Create( "char[" ), v_aResult[Variant.Create( 0 )] ), Variant.Create( "]" ) ) );
            v_aResult = _functions.DllCall(
                Variant.Create( "kernel32.dll" ), Variant.Create( "int" ), Variant.Create( "WideCharToMultiByte" ), Variant.Create( "uint" ), v_iCodePage,
                Variant.Create( "dword" ), Variant.Create( 0 ), v_sUnicodeType, v_pUnicode, Variant.Create( "int" ), Variant.Create( -1 ),
                Variant.Create( "struct*" ), v_tMultiByte, Variant.Create( "int" ), v_aResult[Variant.Create( 0 )], Variant.Create( "ptr" ), Variant.Create( 0 ),
                Variant.Create( "ptr" ), Variant.Create( 0 ) );
            if ( _functions.OR( _context.Error, _functions.NOT( v_aResult[Variant.Create( 0 )] ) ) ) {
                return _functions.SetError( _context.Error+Variant.Create( 10 ), _context.Extended, Variant.Create( "" ) );
            }

            if ( v_bRetString ) {
                return _functions.DllStructGetData( v_tMultiByte, Variant.Create( 1 ) );
            }

            return v_tMultiByte;
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_WindowFromPoint( ref Variant v_tPoint ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "user32.dll" ), Variant.Create( "hwnd" ), Variant.Create( "WindowFromPoint" ), Variant.Create( "struct" ), v_tPoint );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( 0 ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_WriteConsole( Variant v_hConsole, Variant v_sText ) {
            Variant v_aResult = Variant.Create( (object) null );
            v_aResult = _functions.DllCall(
                Variant.Create( "kernel32.dll" ), Variant.Create( "bool" ), Variant.Create( "WriteConsoleW" ), Variant.Create( "handle" ), v_hConsole,
                Variant.Create( "wstr" ), v_sText, Variant.Create( "dword" ), _functions.StringLen( v_sText ), Variant.Create( "dword*" ), Variant.Create( 0 ),
                Variant.Create( "ptr" ), Variant.Create( 0 ) );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_WriteFile( Variant v_hFile, Variant v_pBuffer, Variant v_iToWrite, ref Variant v_iWritten, Variant v_pOverlapped = null ) {
            Variant v_aResult = Variant.Create( (object) null );
            if ( v_pOverlapped == null ) {
                v_pOverlapped = Variant.Create( 0 );
            }
            v_aResult = _functions.DllCall(
                Variant.Create( "kernel32.dll" ), Variant.Create( "bool" ), Variant.Create( "WriteFile" ), Variant.Create( "handle" ), v_hFile,
                Variant.Create( "ptr" ), v_pBuffer, Variant.Create( "dword" ), v_iToWrite, Variant.Create( "dword*" ), Variant.Create( 0 ),
                Variant.Create( "ptr" ), v_pOverlapped );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            v_iWritten = v_aResult[Variant.Create( 4 )];
            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }

        public Variant f__WinAPI_WriteProcessMemory(
            Variant v_hProcess,
            Variant v_pBaseAddress,
            Variant v_pBuffer,
            Variant v_iSize,
            ref Variant v_iWritten,
            Variant v_sBuffer = null ) {
            Variant v_aResult = Variant.Create( (object) null );
            if ( v_sBuffer == null ) {
                v_sBuffer = Variant.Create( "ptr" );
            }
            v_aResult = _functions.DllCall(
                Variant.Create( "kernel32.dll" ), Variant.Create( "bool" ), Variant.Create( "WriteProcessMemory" ), Variant.Create( "handle" ), v_hProcess,
                Variant.Create( "ptr" ), v_pBaseAddress, v_sBuffer, v_pBuffer, Variant.Create( "ulong_ptr" ), v_iSize, Variant.Create( "ulong_ptr*" ),
                Variant.Create( 0 ) );
            if ( _context.Error ) {
                return _functions.SetError( _context.Error, _context.Extended, Variant.Create( false ) );
            }

            v_iWritten = v_aResult[Variant.Create( 5 )];
            return v_aResult[Variant.Create( 0 )];
            return Variant.Create( (object) null );
        }
    }
}
