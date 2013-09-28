#region

using Autofac;
using Autofac.Configuration;

#endregion

namespace BuildMotion.Membership.Business
{
	public static class Bootstrapper
	{
		/// <summary>
		/// Installs this instance.
		/// </summary>
		/// <returns></returns>
		public static IMembershipService Install()
		{
			var builder = new ContainerBuilder();
			builder.RegisterModule(new ConfigurationSettingsReader());
			var container = builder.Build();
			return container.Resolve<IMembershipService>();
		}
	}
}