const app = Vue.createApp({
    components: { 'default-header': header_component },
    data() {
        let params = new URLSearchParams(window.location.search)
        let date = new Date()
        return {
            search_params: params,
            // objectName: "Нет названия",
            // objectId: 1,
            // capacity: 64,
            date_string: params.get("date") ?? date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + date.getDate(),
            view_data: null
        }
    },
    mounted() {
        this.view_data_update()
    },
    computed: {
        objectId() {
            return this.search_params.get("id");
        },
        events() {
            return this.view_data?.events ?? [];
        },
        objectName() {
            return this.view_data?.objectName ?? "Загрузка...";
        },
        capacity() {
            return this.view_data?.capacity;
        }
    },
    methods: {
        get_right_form(num, words) {
            if (num % 10 === 0 || num % 10 > 4 || (num % 100 / 10 | 0) === 1)
                return words[0]
            else if (num % 10 === 1)
                return words[1]
            else
                return words[2]
        },
        view_data_update() {
            let date = new Date(this.date_string)
            let options = {
                day: "numeric",
                month: "long",
                year: "numeric"
            }
            axios
                .get("https://localhost:5001/api/event/get-events-by-date-sport-object?id=" + this.objectId + "&date=" + this.date_string)
                .then(response => {
                    this.view_data = response.data;
                    document.title = response.data.objectName + " - " + date.toLocaleDateString("ru", options)
                    // history.pushState({id:this.objectId,date:this.date_string}, '', "events")
                })
                .catch(error => console.log(error));
            history.pushState(null, '', "events?id=" + this.objectId + "&date=" + this.date_string)
        }
    },
    watch: {
        date_string(old_val, new_val) {
            this.view_data_update()
        }
    }
});

app.component('date-picker', {
    data() {
        return {
            month_strings: [
                "янв",
                "фев",
                "мар",
                "апр",
                "май",
                "июн",
                "июл",
                "авг",
                "сен",
                "окт",
                "ноя",
                "дек",
            ]
        }
    },
    props: {
      date: String
    },
    computed: {
        day() {
            return (new Date(this.date)).getDate();
        },
        month() {
            return this.month_strings[(new Date(this.date)).getMonth()];
        },
        year() {
            return (new Date(this.date)).getFullYear();
        },
        date() {
            return new Date(this.date);
        }
    },
    template: `
    <div class="date-picker-block">
        <p class="current-date"><span id="day">{{ day }}</span> <span id="month">{{ month }}</span> <span id="year">{{ year }}</span></p>
        <span class="datepicker-toggle">
            <span class="datepicker-toggle-button"></span>
            <input type="date" class="datepicker-input" v-model="this.$root.date_string">
        </span>
    </div>
    `
});

app.component('page-info', {
    props: ['object_name', 'date'],
    template: `
        <div class="date-block">
            <div class="wrapper">
                <p class="sport-object-name">{{ object_name }}</p>
                <date-picker :date="date"></date-picker>
            </div>
        </div>
    `
});

app.component('event-block', {
    data() {
        return {
            signed: false
        }
    },
    props: ['event', 'capacity'],
    computed: {
        value_height() {
            return Math.max((1 - this.event.freeSpaces/this.capacity)*100, 15)+'%';
        },
        value_background() {
            return `hsl(${this.event.freeSpaces/this.capacity*100},85%,65%)`;
        }
    },
    methods: {
        sign_up_for_the_event(event_id)
        {
            if (!this.signed)
            {
                axios
                    .post("https://localhost:5001/api/event/signup-for-an-event?eventid=" + event_id)
                    .then(response => {
                        console.log(response)
                        if (response.data.type === "already_signed_up")
                        {
                            this.signed = true
                        }
                    })
                    .catch(error => console.log(error));
            }
        }
    },
    template: `
      <div class="event">
          <div class="capacity-line">
            <div class="value" :style="{ height: value_height, background: value_background}"></div>
<!--            :style="{ height:event.freeSpaces/capacity+'%'}"-->
          </div>
          <div class="info">
            <p class="name">{{ event.sectionName }}</p>
            <p class="time">{{ event.time }}</p>
            <div class="graph-info">
              <div class="duration">
                <img src="/img/clock.svg" alt="">
                <p class="text">{{ event.duration }}м</p>
              </div>
              <div class="capacity-block">
                <div class="diagram">
                  <div class="value"></div>
                </div>
                <p class="count">{{ event.freeSpaces }}</p>
                <p class="text">{{ this.$root.get_right_form(event.freeSpaces, ["мест", "место", "места"])}}<br/>свободно</p>
              </div>
            </div>
          </div>
          <a href="#" class="sign-up-for-an-event" :class="{signed: this.signed}" @click="this.sign_up_for_the_event(event.id)">Записаться</a>
      </div>
    `
});

app.component('events-block', {
    props: [],
    template: `
        <div class="day-events-container">
            <page-info :object_name="this.$root.objectName" :date="this.$root.date_string"></page-info>
            <div class="events">
              <event-block v-for="event in this.$root.events" :event="event" :capacity="this.$root.capacity"></event-block>
            </div>
            <div class="subscription-info"></div>
        </div>
    `
})
app.mount("#app");

$(window).scroll(function(){
    $('.wrapper').css({
        'top': $(this).scrollTop()
        //Why this 15, because in the CSS, we have set left 15, so as we scroll, we would want this to remain at 15px left
    });
});