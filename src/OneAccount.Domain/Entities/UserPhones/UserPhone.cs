using OneAccount.Domain.Abstraction;
using OneAccount.Domain.Abstraction.Exceptions;
using OneAccount.Domain.Enumerators;

namespace OneAccount.Domain.Entities.UserPhones;

public sealed class UserPhone : Entity
{
    private string _callingCode = string.Empty;
    private string _regionCode = string.Empty;
    private string? _areaCode = string.Empty;
    private PhoneType _phoneType;
    private string _phoneNumber = string.Empty;
    private string _e164 = string.Empty;
    private bool _isVerified;
    private DateTimeOffset? _verifiedAt;
    private bool _isPrimary;

    public string CallingCode => _callingCode;
    public string RegionCode => _regionCode;
    public string? AreaCode => _areaCode;
    public PhoneType PhoneType => _phoneType;
    public string PhoneNumber => _phoneNumber;
    public string E164 => _e164;
    public DateTimeOffset? VerifiedAt => _verifiedAt;
    public bool IsVerified => _isVerified;
    public bool IsPrimary => _isPrimary;
    public DateTimeOffset CreatedAt { get; private set; }

    private UserPhone() { }

    private UserPhone(
        string callingCode,
        string regionCode,
        string? areaCode,
        PhoneType phoneType,
        string phoneNumber,
        string e164,
        bool isPrimary)
    {
        _callingCode = callingCode;
        _regionCode = regionCode;
        _areaCode = areaCode;
        _phoneType = phoneType;
        _phoneNumber = phoneNumber;
        _e164 = e164;
        _isPrimary = isPrimary;

        CreatedAt = DateTimeOffset.UtcNow;
    }

    public static UserPhone Create(
        string callingCode,
        string regionCode,
        string? areaCode,
        PhoneType phoneType,
        string phoneNumber,
        string e164,
        bool isPrimary)
    {
        callingCode = (callingCode ?? string.Empty).Trim();
        regionCode = (regionCode ?? string.Empty).Trim().ToUpperInvariant();
        areaCode = string.IsNullOrWhiteSpace(areaCode) ? null : areaCode.Trim();
        phoneNumber = (phoneNumber ?? string.Empty).Trim();
        e164 = (e164 ?? string.Empty).Trim();

        if (string.IsNullOrWhiteSpace(callingCode))
            throw new DomainException(message: "CallingCode cannot be null or empty.", identifier: "PHONE_CALLING_CODE_EMPTY");

        if (!callingCode.StartsWith("+") || callingCode.Length < 2 || callingCode.Skip(1).Any(ch => !char.IsDigit(ch)))
            throw new DomainException(message: "CallingCode must be in format +<digits> (e.g., +55, +1).", identifier: "PHONE_CALLING_CODE_INVALID");

        if (string.IsNullOrWhiteSpace(regionCode))
            throw new DomainException(message: "RegionCode cannot be null or empty.", identifier: "PHONE_REGION_EMPTY");

        // ISO2 básico: 2 letras (BR/US/CA)
        if (regionCode.Length != 2 || regionCode.Any(ch => ch < 'A' || ch > 'Z'))
            throw new DomainException(message: "RegionCode must be a valid ISO2 code (e.g., BR, US, CA).", identifier: "PHONE_REGION_INVALID");

        // AreaCode é opcional (nullable). Se vier preenchido, valida básico: só dígitos.
        if (areaCode is not null && areaCode.Any(ch => !char.IsDigit(ch)))
            throw new DomainException(message: "AreaCode must contain digits only.", identifier: "PHONE_AREA_CODE_INVALID");

        if (phoneType == PhoneType.Unknown)
            throw new DomainException(message: "Phone type cannot be unknown.", identifier: "PHONE_TYPE_UNKNOWN");

        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new DomainException(message: "Phone number cannot be null or empty.", identifier: "PHONE_NUMBER_EMPTY");

        if (phoneNumber.Any(ch => !char.IsDigit(ch)))
            throw new DomainException(message: "Phone number must contain digits only.", identifier: "PHONE_NUMBER_INVALID");

        if (string.IsNullOrWhiteSpace(e164))
            throw new DomainException(message: "E164 cannot be null or empty.", identifier: "PHONE_E164_EMPTY");

        // E.164 básico: '+' seguido de dígitos (validação oficial fica na libphonenumber)
        if (!e164.StartsWith("+") || e164.Length < 2 || e164.Skip(1).Any(ch => !char.IsDigit(ch)))
            throw new DomainException(message: "E164 must be in format +<digits>.", identifier: "PHONE_E164_INVALID");

        return new UserPhone(callingCode, regionCode, areaCode, phoneType, phoneNumber, e164, isPrimary);
    }

    internal void Update(
         string callingCode,
         string regionCode,
         string? areaCode,
         PhoneType phoneType,
         string phoneNumber,
         string e164)
    {
        callingCode = (callingCode ?? string.Empty).Trim();
        regionCode = (regionCode ?? string.Empty).Trim().ToUpperInvariant();
        areaCode = string.IsNullOrWhiteSpace(areaCode) ? null : areaCode.Trim();
        phoneNumber = (phoneNumber ?? string.Empty).Trim();
        e164 = (e164 ?? string.Empty).Trim();

        if (string.IsNullOrWhiteSpace(callingCode))
            throw new DomainException(message: "CallingCode cannot be null or empty.", identifier: "PHONE_CALLING_CODE_EMPTY");

        if (!callingCode.StartsWith("+") || callingCode.Length < 2 || callingCode.Skip(1).Any(ch => !char.IsDigit(ch)))
            throw new DomainException(message: "CallingCode must be in format +<digits> (e.g., +55, +1).", identifier: "PHONE_CALLING_CODE_INVALID");

        if (string.IsNullOrWhiteSpace(regionCode))
            throw new DomainException(message: "RegionCode cannot be null or empty.", identifier: "PHONE_REGION_EMPTY");

        if (regionCode.Length != 2 || regionCode.Any(ch => ch < 'A' || ch > 'Z'))
            throw new DomainException(message: "RegionCode must be a valid ISO2 code (e.g., BR, US, CA).", identifier: "PHONE_REGION_INVALID");

        if (areaCode is not null && areaCode.Any(ch => !char.IsDigit(ch)))
            throw new DomainException(message: "AreaCode must contain digits only.", identifier: "PHONE_AREA_CODE_INVALID");

        if (phoneType == PhoneType.Unknown)
            throw new DomainException(message: "Phone type cannot be unknown.", identifier: "PHONE_TYPE_UNKNOWN");

        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new DomainException(message: "Phone number cannot be null or empty.", identifier: "PHONE_NUMBER_EMPTY");

        if (phoneNumber.Any(ch => !char.IsDigit(ch)))
            throw new DomainException(message: "Phone number must contain digits only.", identifier: "PHONE_NUMBER_INVALID");

        if (string.IsNullOrWhiteSpace(e164))
            throw new DomainException(message: "E164 cannot be null or empty.", identifier: "PHONE_E164_EMPTY");

        if (!e164.StartsWith("+") || e164.Length < 2 || e164.Skip(1).Any(ch => !char.IsDigit(ch)))
            throw new DomainException(message: "E164 must be in format +<digits>.", identifier: "PHONE_E164_INVALID");

        var changed =
            _callingCode != callingCode ||
            _regionCode != regionCode ||
            _areaCode != areaCode ||
            _phoneType != phoneType ||
            _phoneNumber != phoneNumber ||
            _e164 != e164;

        if (!changed)
            return;

        _callingCode = callingCode;
        _regionCode = regionCode;
        _areaCode = areaCode;
        _phoneType = phoneType;
        _phoneNumber = phoneNumber;
        _e164 = e164;

        // mudou telefone => invalida verificação
        _isVerified = false;
        _verifiedAt = null;
    }

    internal void MarkVerified(DateTimeOffset verifiedAtUtc)
    {
        if (_isVerified)
            return;

        if (verifiedAtUtc == default)
            throw new DomainException(message: "VerifiedAt cannot be default.", identifier: "PHONE_VERIFIED_AT_INVALID");

        _isVerified = true;
        _verifiedAt = verifiedAtUtc;
    }

    internal void SetPrimary(bool isPrimary)
    {
        if (_isPrimary == isPrimary)
            return;

        _isPrimary = isPrimary;
    }
}
