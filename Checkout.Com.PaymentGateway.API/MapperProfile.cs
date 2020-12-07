using AutoMapper;
using SM = Checkout.Com.PaymentGateway.API.ServiceModels;
using DM = Checkout.Com.PaymentGateway.Models;
using Checkout.Com.PaymentGateway.Services.PaymentProcessor.Models;
using DBMODEL = Checkout.Com.PaymentGateway.Services.Persistance.Models;
using System;
using System.Linq;

namespace Checkout.Com.PaymentGateway.API
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<DM.PaymentStatus, SM.PaymentAcknowledgement>();

            CreateMap<VisaPaymentResponse, DM.PaymentStatus>()
                .ForMember((dest) => dest.Status, opt => opt.MapFrom(src => (src.Status == "success" ? DM.Status.Success : DM.Status.Failure)))
                .ForMember((dest) => dest.PublicId, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember((dest) => dest.BankId, opt => opt.MapFrom(src => src.UniqueId));

            CreateMap<MastercardPaymentResponse, DM.PaymentStatus>()
                .ForMember((dest) => dest.Status, opt => opt.MapFrom(src => (src.Status == "complete" ? DM.Status.Success : DM.Status.Failure)))
                .ForMember((dest) => dest.PublicId, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember((dest) => dest.BankId, opt => opt.MapFrom(src => src.UniqueId));

            CreateMap<DBMODEL.Payment, DM.Payment>().ReverseMap();

            CreateMap<DM.PaymentStatus, DBMODEL.PaymentStatus>()
                .ForMember((dest) => dest.Status, opt => opt.MapFrom(src => src.Status == DM.Status.Success ? "success" : "failure"));
            CreateMap<DM.Payment, DBMODEL.Payment>();
            CreateMap<DM.Card, DBMODEL.Card>()
                .ForMember((dest) => dest.MaskedNumber, opt => opt.MapFrom(src => string.Concat(new string(Enumerable.Repeat('X', src.Number.Length - 3).ToArray()), src.Number.Substring(src.Number.Length - 3, 3))));

            CreateMap<DBMODEL.Card, DM.Card>()
                .ForMember((dest) => dest.Number, opt => opt.MapFrom(src => src.MaskedNumber));

            CreateMap<SM.Payment, DM.Payment>().ReverseMap();
            CreateMap<SM.Card, DM.Card>()
                .ForMember((dest) => dest.Number, opt => opt.MapFrom(src => src.Number.Replace(" ", "")))
                .ForMember((dest) => dest.ExpiryMonth, opt => opt.MapFrom(src => src.Expiry.Split(new char[] { '/' }, StringSplitOptions.None)[0]))
                .ForMember((dest) => dest.ExpiryYear, opt => opt.MapFrom(src => src.Expiry.Split(new char[] { '/' }, StringSplitOptions.None)[1]));

            CreateMap<DM.Card, SM.Card>()
                .ForMember((dest) => dest.Expiry, opt => opt.MapFrom(src => $"{src.ExpiryMonth}/{src.ExpiryYear}"  ));
        }
    }
}
