﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appiume.Apm.Collections.Extensions;
using Appiume.Apm.Threading;

namespace Appiume.Apm.Authorization
{
    /// <summary>
    /// Extension methods for <see cref="IPermissionChecker"/>
    /// </summary>
    public static class PermissionCheckerExtensions
    {
        /// <summary>
        /// Checks if current user is granted for a permission.
        /// </summary>
        /// <param name="permissionChecker">Permission checker</param>
        /// <param name="permissionName">Name of the permission</param>
        public static bool IsGranted(this IPermissionChecker permissionChecker, string permissionName)
        {
            return AsyncHelper.RunSync(() => permissionChecker.IsGrantedAsync(permissionName));
        }

        /// <summary>
        /// Checks if a user is granted for a permission.
        /// </summary>
        /// <param name="permissionChecker">Permission checker</param>
        /// <param name="userId">Id of the user to check</param>
        /// <param name="permissionName">Name of the permission</param>
        [Obsolete("Use IsGranted(IPermissionChecker, UserIdentifier, string) instead.")]
        public static bool IsGranted(this IPermissionChecker permissionChecker, long userId, string permissionName)
        {
            return AsyncHelper.RunSync(() => permissionChecker.IsGrantedAsync(userId, permissionName));
        }

        /// <summary>
        /// Checks if a user is granted for a permission.
        /// </summary>
        /// <param name="permissionChecker">Permission checker</param>
        /// <param name="user">User to check</param>
        /// <param name="permissionName">Name of the permission</param>
        public static bool IsGranted(this IPermissionChecker permissionChecker, UserIdentifier user, string permissionName)
        {
            return AsyncHelper.RunSync(() => permissionChecker.IsGrantedAsync(user, permissionName));
        }

        /// <summary>
        /// Checks if given user is granted for given permission.
        /// </summary>
        /// <param name="permissionChecker">Permission checker</param>
        /// <param name="userId">User id</param>
        /// <param name="requiresAll">True, to require all given permissions are granted. False, to require one or more.</param>
        /// <param name="permissionNames">Name of the permissions</param>
        [Obsolete("Use IsGranted(IPermissionChecker, UserIdentifier, bool, params string[]) instead")]
        public static bool IsGranted(this IPermissionChecker permissionChecker, long userId, bool requiresAll, params string[] permissionNames)
        {
            return AsyncHelper.RunSync(() => IsGrantedAsync(permissionChecker, userId, requiresAll, permissionNames));
        }

        /// <summary>
        /// Checks if given user is granted for given permission.
        /// </summary>
        /// <param name="permissionChecker">Permission checker</param>
        /// <param name="user">User</param>
        /// <param name="requiresAll">True, to require all given permissions are granted. False, to require one or more.</param>
        /// <param name="permissionNames">Name of the permissions</param>
        public static bool IsGranted(this IPermissionChecker permissionChecker, UserIdentifier user, bool requiresAll, params string[] permissionNames)
        {
            return AsyncHelper.RunSync(() => IsGrantedAsync(permissionChecker, user, requiresAll, permissionNames));
        }

        /// <summary>
        /// Checks if given user is granted for given permission.
        /// </summary>
        /// <param name="permissionChecker">Permission checker</param>
        /// <param name="userId">User id</param>
        /// <param name="requiresAll">True, to require all given permissions are granted. False, to require one or more.</param>
        /// <param name="permissionNames">Name of the permissions</param>
        [Obsolete("Use IsGrantedAsync(IPermissionChecker, UserIdentifier, bool, params string[]) instead")]
        public static async Task<bool> IsGrantedAsync(this IPermissionChecker permissionChecker, long userId, bool requiresAll, params string[] permissionNames)
        {
            if (permissionNames.IsNullOrEmpty())
            {
                return true;
            }

            if (requiresAll)
            {
                foreach (var permissionName in permissionNames)
                {
                    if (!(await permissionChecker.IsGrantedAsync(userId, permissionName)))
                    {
                        return false;
                    }
                }

                return true;
            }
            else
            {
                foreach (var permissionName in permissionNames)
                {
                    if (await permissionChecker.IsGrantedAsync(userId, permissionName))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// Checks if given user is granted for given permission.
        /// </summary>
        /// <param name="permissionChecker">Permission checker</param>
        /// <param name="user">User</param>
        /// <param name="requiresAll">True, to require all given permissions are granted. False, to require one or more.</param>
        /// <param name="permissionNames">Name of the permissions</param>
        public static async Task<bool> IsGrantedAsync(this IPermissionChecker permissionChecker, UserIdentifier user, bool requiresAll, params string[] permissionNames)
        {
            if (permissionNames.IsNullOrEmpty())
            {
                return true;
            }

            if (requiresAll)
            {
                foreach (var permissionName in permissionNames)
                {
                    if (!(await permissionChecker.IsGrantedAsync(user, permissionName)))
                    {
                        return false;
                    }
                }

                return true;
            }
            else
            {
                foreach (var permissionName in permissionNames)
                {
                    if (await permissionChecker.IsGrantedAsync(user, permissionName))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// Checks if current user is granted for given permission.
        /// </summary>
        /// <param name="permissionChecker">Permission checker</param>
        /// <param name="requiresAll">True, to require all given permissions are granted. False, to require one or more.</param>
        /// <param name="permissionNames">Name of the permissions</param>
        public static bool IsGranted(this IPermissionChecker permissionChecker, bool requiresAll, params string[] permissionNames)
        {
            return AsyncHelper.RunSync(() => IsGrantedAsync(permissionChecker, requiresAll, permissionNames));
        }

        /// <summary>
        /// Checks if current user is granted for given permission.
        /// </summary>
        /// <param name="permissionChecker">Permission checker</param>
        /// <param name="requiresAll">True, to require all given permissions are granted. False, to require one or more.</param>
        /// <param name="permissionNames">Name of the permissions</param>
        public static async Task<bool> IsGrantedAsync(this IPermissionChecker permissionChecker, bool requiresAll, params string[] permissionNames)
        {
            if (permissionNames.IsNullOrEmpty())
            {
                return true;
            }

            if (requiresAll)
            {
                foreach (var permissionName in permissionNames)
                {
                    if (!(await permissionChecker.IsGrantedAsync(permissionName)))
                    {
                        return false;
                    }
                }

                return true;
            }
            else
            {
                foreach (var permissionName in permissionNames)
                {
                    if (await permissionChecker.IsGrantedAsync(permissionName))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// Authorizes current user for given permission or permissions,
        /// throws <see cref="ApmAuthorizationException"/> if not authorized.
        /// User it authorized if any of the <see cref="permissionNames"/> are granted.
        /// </summary>
        /// <param name="permissionChecker">Permission checker</param>
        /// <param name="permissionNames">Name of the permissions to authorize</param>
        /// <exception cref="ApmAuthorizationException">Throws authorization exception if</exception>
        public static void Authorize(this IPermissionChecker permissionChecker, params string[] permissionNames)
        {
            Authorize(permissionChecker, false, permissionNames);
        }

        /// <summary>
        /// Authorizes current user for given permission or permissions,
        /// throws <see cref="ApmAuthorizationException"/> if not authorized.
        /// User it authorized if any of the <see cref="permissionNames"/> are granted.
        /// </summary>
        /// <param name="permissionChecker">Permission checker</param>
        /// <param name="requireAll">
        /// If this is set to true, all of the <see cref="permissionNames"/> must be granted.
        /// If it's false, at least one of the <see cref="permissionNames"/> must be granted.
        /// </param>
        /// <param name="permissionNames">Name of the permissions to authorize</param>
        /// <exception cref="ApmAuthorizationException">Throws authorization exception if</exception>
        public static void Authorize(this IPermissionChecker permissionChecker, bool requireAll, params string[] permissionNames)
        {
            AsyncHelper.RunSync(() => AuthorizeAsync(permissionChecker, requireAll, permissionNames));
        }

        /// <summary>
        /// Authorizes current user for given permission or permissions,
        /// throws <see cref="ApmAuthorizationException"/> if not authorized.
        /// User it authorized if any of the <see cref="permissionNames"/> are granted.
        /// </summary>
        /// <param name="permissionChecker">Permission checker</param>
        /// <param name="permissionNames">Name of the permissions to authorize</param>
        /// <exception cref="ApmAuthorizationException">Throws authorization exception if</exception>
        public static Task AuthorizeAsync(this IPermissionChecker permissionChecker, params string[] permissionNames)
        {
            return AuthorizeAsync(permissionChecker, false, permissionNames);
        }

        /// <summary>
        /// Authorizes current user for given permission or permissions,
        /// throws <see cref="ApmAuthorizationException"/> if not authorized.
        /// </summary>
        /// <param name="permissionChecker">Permission checker</param>
        /// <param name="requireAll">
        /// If this is set to true, all of the <see cref="permissionNames"/> must be granted.
        /// If it's false, at least one of the <see cref="permissionNames"/> must be granted.
        /// </param>
        /// <param name="permissionNames">Name of the permissions to authorize</param>
        /// <exception cref="ApmAuthorizationException">Throws authorization exception if</exception>
        public static async Task AuthorizeAsync(this IPermissionChecker permissionChecker, bool requireAll, params string[] permissionNames)
        {
            if (await IsGrantedAsync(permissionChecker, requireAll, permissionNames))
            {
                return;
            }

            if (requireAll)
            {
                throw new ApmAuthorizationException(
                    "Required permissions are not granted. All of these permissions must be granted: " +
                    string.Join(", ", permissionNames)
                    );
            }
            else
            {
                throw new ApmAuthorizationException(
                    "Required permissions are not granted. At least one of these permissions must be granted: " +
                    string.Join(", ", permissionNames)
                    );
            }
        }
    }
}
