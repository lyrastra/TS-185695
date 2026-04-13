using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Moedelo.Changelog.Shared.Domain.Attributes;
using Moedelo.Changelog.Shared.Domain.Enums;
using Moedelo.Common.AccessRules.Abstractions;

namespace Moedelo.Changelog.Shared.Domain.Definitions.Calendar
{
    public class CalendarEventDefinition
        : AutoEntityStateDefinition<
            CalendarEventDefinition,
            CalendarEventDefinition.State>
    {
        public CalendarEventDefinition()
        {
            AddTag("calendar");
        }

        public override ChangeLogEntityType EntityType { get; } = ChangeLogEntityType.CalendarEvent;

        public class State
        {
            [Display(Name="Название")]
            public string Title { get; set; }

            /// <summary>
            /// техполе: идентификатор состояния календарного события
            /// </summary>
            public int EventStateId { get; set; }

            [Display(Name="Год")]
            [FieldTypeYear]
            public int Year { get; set; }
            
            [Display(Name="Источник изменений")]
            public EventChangeSource ActionSource { get; set; }

            [Display(Name = "Статус события")]
            public CalendarEventStatus Status { get; set; }

            /// <summary> Статус календарного события </summary>
            public enum CalendarEventStatus
            {
                [Display(Name="Не создано")]
                NotCreated = 0,

                [Display(Name= "Открыто")]
                Open = 1,

                [Display(Name="Перенесено на следующий квартал")]
                MoveToNextQuarter = 2,

                [Display(Name="Перенесено")]
                Moved = 20,

                [Display(Name="Завершено")]
                Completed = 100,
        
                [Display(Name="Завершено (техн.)")]
                Hidden = 101,

                [Display(Name="Удалено")]
                Deleted = 200,
            }

            public enum EventChangeSource
            {
                [Display(Name="Вручную")]
                Manual = 0,

                [Display(Name="Из мастера")]
                FromWizard = 1
            }
        }

        public override IReadOnlyCollection<AccessRule> RequiredReadPermissions { get; }
            = new[] {AccessRule.ViewCalendar};

        public override long GetEntityId(State state)
        {
            return state.EventStateId;
        }

        protected override string GetEntityName(State entityState)
        {
            return entityState.Title;
        }
    }
}
