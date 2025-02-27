// Copyright © 2012-2018 Vaughn Vernon. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Linq;
using Vlingo.Directory.Model.Message;
using Vlingo.Wire.Node;
using Xunit;

namespace Vlingo.Directory.Tests.Model.Message
{
    public class RegisterServiceTest
    {
        private readonly string _textMessage = "REGSRVC\nnm=test-service\naddr=1.2.3.4:111";

        [Fact]
        public void TestMessage()
        {
            var registerService = new RegisterService(Name.Of("test-service"),
                Address.From(Host.Of("1.2.3.4"), 111, AddressType.Main));
            
            Assert.Single(registerService.Addresses);
            Assert.Equal(_textMessage, registerService.ToString());
        }

        [Fact]
        public void TestValidity()
        {
            var registerService = new RegisterService(Name.Of("test-service"),
                Address.From(Host.Of("1.2.3.4"), 111, AddressType.Main));
            
            Assert.True(registerService.IsValid);
            Assert.False(RegisterService.From("blah").IsValid);
            Assert.True(RegisterService.From(_textMessage).IsValid);
        }
    }
}