using Microsoft.Extensions.Logging;
using MailKit.Net.Smtp;
using MimeKit;
using ThothShop.Infrastructure.Contracts;
using ThothShop.Service.Contract;
using ThothShop.Domain.Helpers;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace ThothShop.Service.implementations
{

    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly ILogger<EmailService> _logger;
        private readonly IUserRepository _userRepository;

        public EmailService(
            EmailSettings emailSettings,
            ILogger<EmailService> logger,
            IUserRepository userRepository)
        {
            _emailSettings = emailSettings ?? throw new ArgumentNullException(nameof(emailSettings));
            _logger = logger;
            _userRepository = userRepository;
        }

        public async Task<bool> SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                //sending the Message of passwordResetLink
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_emailSettings.Host, _emailSettings.Port, true);
                    client.Authenticate(_emailSettings.FromEmail, _emailSettings.Password);
                    var bodybuilder = new BodyBuilder
                    {
                        HtmlBody = $"{message}",
                        TextBody = "welcome",
                    };
                    var Message = new MimeMessage
                    {
                        Body = bodybuilder.ToMessageBody()
                    };
                    Message.From.Add(new MailboxAddress("Future Team", _emailSettings.FromEmail));
                    Message.To.Add(new MailboxAddress("testing", email));
                    Message.Subject = subject ?? "Not Submitted";
                    await client.SendAsync(Message);
                    await client.DisconnectAsync(true);
                }
                //end of sending email
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> SendConfirmationEmailAsync(string email, string callbackUrl)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                _logger.LogWarning($"Attempted to send confirmation email to non-existent user: {email}");
                return false;
            }

            var subject = "Confirm your email";
            var message = $@"
                              <html>
                              <head>
                                <style>
                                  body {{
                                    font-family: Arial, sans-serif;
                                    background-color: #f4f4f4;
                                    margin: 0;
                                    padding: 0;
                                  }}
                                  .container {{
                                    max-width: 600px;
                                    margin: 40px auto;
                                    background-color: #ffffff;
                                    padding: 30px;
                                    border-radius: 8px;
                                    box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
                                  }}
                                  .header {{
                                    text-align: center;
                                    padding-bottom: 20px;
                                  }}
                                  .header h1 {{
                                    color: #333;
                                  }}
                                  .content {{
                                    font-size: 16px;
                                    line-height: 1.6;
                                    color: #555;
                                  }}
                                  .btn {{
                                    display: inline-block;
                                    margin-top: 25px;
                                    padding: 12px 25px;
                                    background-color: #007bff;
                                    color: #ffffff;
                                    text-decoration: none;
                                    border-radius: 5px;
                                    font-weight: bold;
                                  }}
                                  .footer {{
                                    margin-top: 30px;
                                    font-size: 12px;
                                    text-align: center;
                                    color: #888;
                                  }}
                                </style>
                              </head>
                              <body>
                                <div class='container'>
                                  <div class='header'>
                                    <h1>Confirm Your Email</h1>
                                  </div>
                                  <div class='content'>
                                    <p>Hi {user.UserName},</p>
                                    <p>Thank you for registering! Please confirm your email address by clicking the button below:</p>
                                    <p style='text-align:center;'>
                                      <a href='{callbackUrl}' class='btn'>Confirm Email</a>
                                    </p>
                                    <p>If you didn’t create an account, you can safely ignore this email.</p>
                                  </div>
                                  <div class='footer'>
                                    &copy; {DateTime.UtcNow.Year} Thoth Store . All rights reserved.
                                  </div>
                                </div>
                              </body>
                              </html>";


            return await SendEmailAsync(email, subject, message);
        }


        public async Task<bool> SendOrderConfirmationAsync(string userId, Guid orderId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                _logger.LogWarning($"Attempted to send order confirmation to non-existent user: {userId}");
                return false;
            }

            var subject = $"Order Confirmation #{orderId}";
            var message = $@"
                    <html>
                    <head>
                      <style>
                        body {{
                          font-family: Arial, sans-serif;
                          background-color: #f4f4f4;
                          margin: 0;
                          padding: 0;
                        }}
                        .container {{
                          max-width: 600px;
                          margin: 40px auto;
                          background-color: #ffffff;
                          padding: 30px;
                          border-radius: 8px;
                          box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
                        }}
                        .header {{
                          text-align: center;
                          padding-bottom: 20px;
                        }}
                        .header h1 {{
                          color: #333;
                        }}
                        .content {{
                          font-size: 16px;
                          line-height: 1.6;
                          color: #555;
                        }}
                        .btn {{
                          display: inline-block;
                          margin-top: 25px;
                          padding: 12px 25px;
                          background-color: #007bff;
                          color: #ffffff;
                          text-decoration: none;
                          border-radius: 5px;
                          font-weight: bold;
                        }}
                        .footer {{
                          margin-top: 30px;
                          font-size: 12px;
                          text-align: center;
                          color: #888;
                        }}
                      </style>
                    </head>
                    <body>
                      <div class='container'>
                        <div class='header'>
                          <h1>Reset YOur Password </h1>
                        </div>
                        <div class='content'>
                          <p>Hi {user.UserName},</p>
                          <p style='text-align:center;'>
                            Thank you for your order! Your order ID is: {orderId}
                          </p>
                        </div>
                        <div class='footer'>
                          &copy; {DateTime.UtcNow.Year} Thoth Store. All rights reserved.
                        </div>
                      </div>
                    </body>
                    </html>";

            return await SendEmailAsync(user.Email, subject, message);
        }

        public async Task<bool> SendPasswordResetEmailAsync(string email, string subject, string message)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                _logger.LogWarning($"Attempted to send password reset email to non-existent user: {email}");
                return false;
            }

            var Subject = subject ?? "Reset your password";
            return await SendEmailAsync(email, Subject, message);
        }
    }

}
