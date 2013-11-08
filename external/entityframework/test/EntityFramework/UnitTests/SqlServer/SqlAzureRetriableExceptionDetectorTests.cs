﻿// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

namespace System.Data.Entity.SqlServer
{
    using System.Data.Entity.Core;
    using System.Data.Entity.Infrastructure;
    using Xunit;

    public class SqlAzureRetriableExceptionDetectorTests
    {
        [Fact]
        public void ShouldRetryOn_returns_false_for_null()
        {
            Assert.False(new SqlAzureRetriableExceptionDetector().ShouldRetryOn(null));
        }

        [Fact]
        public void ShouldRetryOn_returns_false_for_non_transient_exception()
        {
            Assert.False(new SqlAzureRetriableExceptionDetector().ShouldRetryOn(new EntityException()));
        }

        [Fact]
        public void ShouldRetryOn_returns_true_for_TimeoutException()
        {
            Assert.True(new SqlAzureRetriableExceptionDetector().ShouldRetryOn(new TimeoutException()));
        }

        [Fact]
        public void ShouldRetryOn_returns_true_for_exception_nested_in_EntityException()
        {
            Assert.True(new SqlAzureRetriableExceptionDetector().ShouldRetryOn(new EntityException("", new TimeoutException())));
        }

        [Fact]
        public void ShouldRetryOn_returns_true_for_exception_nested_in_DbUpdateException()
        {
            Assert.True(new SqlAzureRetriableExceptionDetector().ShouldRetryOn(new DbUpdateException("", new TimeoutException())));
        }

        [Fact]
        public void ShouldRetryOn_returns_true_for_exception_nested_in_UpdateException()
        {
            Assert.True(new SqlAzureRetriableExceptionDetector().ShouldRetryOn(new UpdateException("", new TimeoutException())));
        }
    }
}
