<template>
    <v-dialog
        @click:outside="clearInputs"
		v-model="params.show"
		:max-width="params.maxWidth"
	>
		<v-form class="whiteBackground" ref="createProposalForm" v-model="isFormValid">
			<v-container class="table">
				<v-row>
					<v-col :cols="8">
						<v-select
							:items="faculties"
							:rules="selectRules"
							:label="$i18n.t('faculty.faculty') + '*'"
							v-model="chosenFaculty"
							@change="onChangeFaculty"
							return-object
							color="rgb(18,98,141)"
						>
							<template slot="selection" slot-scope="data">
								{{ data.item.name }}
							</template>
							<template slot="item" slot-scope="data">
								{{ data.item.name }}
							</template>
						</v-select>
					</v-col>
					<v-col :cols="4">
						<v-select
							:items="courses"
							:rules="selectRules"
							:label="$i18n.t('course.course') + '*'"
							:disabled="selectCoursesDisabled"
							v-model="chosenCourse"
							return-object
							color="rgb(18,98,141)"
						>
							<template slot="selection" slot-scope="data">
								{{ data.item.name }}
							</template>
							<template slot="item" slot-scope="data">
								{{ data.item.name }}
							</template>
						</v-select>
					</v-col>
				</v-row>
				<v-row>
					<v-col :cols="4">
						<v-select
							:items="levels"
							:rules="selectRules"
							:label="$i18n.t('level.level') + '*'"
							v-model="chosenLevel"
							return-object
							color="rgb(18,98,141)"
						>
							<template slot="selection" slot-scope="data">
								{{ data.item.name }}
							</template>
							<template slot="item" slot-scope="data">
								{{ data.item.name }}
							</template>
						</v-select>
					</v-col>
					<v-col :cols="4">
						<v-select
							:items="modes"
							:rules="selectRules"
							:label="$i18n.t('mode.mode') + '*'"
							v-model="chosenMode"
							return-object
							color="rgb(18,98,141)"
						>
							<template slot="selection" slot-scope="data">
								{{ data.item.name }}
							</template>
							<template slot="item" slot-scope="data">
								{{ data.item.name }}
							</template>
						</v-select>
					</v-col>
					<v-col :cols="4">
						<v-select
							:items="profiles"
							:rules="selectRules"
							:label="$i18n.t('profile.profile') + '*'"
							v-model="chosenProfile"
							return-object
							color="rgb(18,98,141)"
						>
							<template slot="selection" slot-scope="data">
								{{ data.item.name }}
							</template>
							<template slot="item" slot-scope="data">
								{{ data.item.name }}
							</template>
						</v-select>
					</v-col>
				</v-row>
				<v-row>
					<v-col>
						<v-text-field 
							:label="$i18n.t('proposal.titlePolish') + '*'" 
							:rules="topicRules" 
							v-model="topicPolish"
						/>
					</v-col>
				</v-row>
				<v-row>
					<v-col>
						<v-text-field 
							:label="$i18n.t('proposal.titleEnglish') + '*'" 
							:rules="topicRules" 
							v-model="topicEnglish"
						/>
					</v-col>
				</v-row>
				<v-row>
					<v-col>
						<v-text-field 
							type="number"
							single-line
							:rules="maxNumberOfStudentsRules"
							:label="$i18n.t('proposal.maxNumberOfStudents') + '*'" 
							v-model="chosenMaximalNumberOfStudents"
							@change="onChangeMaximalNumberOfStudents"
						/>
					</v-col>
				</v-row>
				<v-row>
					<v-col>
						<v-textarea 
							:value="'Write a description here'" 
							:rules="descriptionRules" 
							:label="$i18n.t('proposal.description') + '*'" 
							v-model="description"
						/>
					</v-col>
				</v-row>
				<v-row>
					<v-col>
						<v-textarea 
							:label="$i18n.t('proposal.outputData') + ' ' + $i18n.t('commons.optional')"
                            :rules="outputDataRules"
							v-model="outputData"
						/>
					</v-col>
				</v-row>
				<v-row>
					<v-col>
						<v-textarea 
							:rules="specializationRules"
							:label="$i18n.t('proposal.specialization') + ' ' + $i18n.t('commons.optional')"
							v-model="specialization"
						/>
					</v-col>
				</v-row>
				<v-row>
					<v-col>
						<v-list v-show="!hideStudentList">
							<v-subheader>
								{{ $i18n.t('student.studentPlural') + ' ' + $i18n.t('commons.optional')}}
							</v-subheader>
							<v-list-item v-for="indexNumber in indexNumbers" :key="indexNumber">
								<v-list-item-title>
									<v-icon 
										@click.stop="deleteIndexNumFromList(indexNumber)" 
										color="rgba(255, 0, 0, 0.9)" 
										large
									>
										delete
									</v-icon>
									{{ indexNumber }}
								</v-list-item-title>
							</v-list-item>
							<v-list-item>
								<v-list-item-title>
									<v-tooltip top>
                                        <template v-slot:activator="{ on }">
                                            <v-text-field @keyup.enter.stop="addStudent"
                                                          @click:prepend.stop="addStudent"
                                                          ref="studentIndexNumberInput"
                                                          type="number"
                                                          single-line
                                                          prepend-icon="group_add"
                                                          :label="$i18n.t('student.indexNumber')"
                                                          v-model="chosenIndexNumber"
                                                          v-on="on" />
                                            <!--:rules="studentRules"-->
                                        </template>
										<span>{{ $i18n.t('info.studentsAreNotNecessary') }}</span>
									</v-tooltip>
								</v-list-item-title>
							</v-list-item>
						</v-list>
					</v-col>
				</v-row>
				<v-row>
					<v-col>
						<v-btn 
							color="#12628d" 
							class="cancelButton" 
							@click="params.show = false"
						>
							{{ $i18n.t('commons.cancel') }}
						</v-btn>
					</v-col>
					<v-col>
						<v-btn 
							class="submitButton green" 
							@click="submit"
						>
							{{ $i18n.t('commons.create') }}
						</v-btn>
					</v-col>
				</v-row>
			</v-container>
		</v-form>
	</v-dialog>
</template>


<script>
import { Vue } from 'vue-property-decorator';
import { proposalService } from '@src/services/proposalService'
import { facultyService } from '@src/services/facultyService'
import { courseService } from '@src/services/courseService'
import { bus } from '@src/services/eventBus'

export default Vue.component('createProposalPopUp',{
	props: {
		params: Object
	},
	methods: {
		addStudent: function() {
			var maxNumOfStudents = parseInt(this.chosenMaximalNumberOfStudents);
			if(this.indexNumbers.length < maxNumOfStudents) {
				var indexNumber = this.chosenIndexNumber;
				var isnum = /^\d+$/.test(indexNumber);
				if(isnum && !indexNumber.startsWith("0")) {
					var num = parseInt(indexNumber);
					if(!this.indexNumbers.includes(num)) {
						this.indexNumbers.push(num);
					}
				}
			}
			this.$refs.studentIndexNumberInput.reset();
		},
		onChangeFaculty: function() {
			this.courses = [];
			this.chosenCourse = null;
			this.chosenFaculty.courses.forEach(courseId => {
				courseService.get(courseId)
					.then(response => {
						if(response.status == 200) {
							var course = response.data;
							this.courses.push(course);
						}
					});
			});
			this.selectCoursesDisabled = false;
		},
		onChangeMaximalNumberOfStudents: function() {
			if(this.chosenMaximalNumberOfStudents.length > 0) {
				this.hideStudentList = false;
				var maxNum = parseInt(this.chosenMaximalNumberOfStudents);
				this.indexNumbers = this.indexNumbers.slice(0, maxNum);
			}
			else {
				this.hideStudentList = true;
				this.indexNumbers = [];
			}
		},
		deleteIndexNumFromList: function(indexNumber) {
			var i = this.indexNumbers.indexOf(indexNumber);
			if (i !== -1) this.indexNumbers.splice(i, 1);
        },
        clearInputs: function() {
			//this.$refs.updateProposalForm.resetValidation();
			this.chosenFaculty = null;
			this.chosenCourse = null;
			this.courses = [];
			this.chosenLevel = null;
			this.chosenMode = null;
			this.chosenProfile = null;
			this.topicPolish = "";
			this.topicEnglish = "";
			this.description = "";
			this.outputData = "";
			this.specialization = "";
			this.chosenMaximalNumberOfStudents = "";
		},
        submit: function () {
            if (this.$refs.studentIndexNumberInput.value == "") {
				this.$refs.studentIndexNumberInput.resetValidation();
            }
            if (this.$refs.createProposalForm.validate()) {
				var proposalRegistration = {
					courseId: this.chosenCourse.id,
					students: this.indexNumbers,
					topicPolish: this.topicPolish,
					topicEnglish: this.topicEnglish,
					description: this.description,
					outputData: this.outputData,
					specialization: this.specialization,
					maxNumberOfStudents: this.chosenMaximalNumberOfStudents,
					studyProfile: this.chosenProfile.value,
					level: this.chosenLevel.value,
					mode: this.chosenMode.value
				}
				proposalService.create(proposalRegistration)
					.then(response => {
						if(response.status == 200) {
							bus.$emit('proposalWasCreated');
							this.params.show = false;
                            this.$refs.createProposalForm.reset();
                            this.clearInputs();
						}
					});
			}
		},
	},
	data() {
		return {
			isFormValid: false,
			selectCoursesDisabled: true,
			hideStudentList: true,
			topicPolish: "",
			topicEnglish: "",
			description: "",
			outputData: "",
			specialization: "",
			indexNumbers: [],
			faculties: [],
			courses: [],
			levels: [
				{
					name: this.$i18n.t('level.bachelor'),
					value: 0
				},
				{
					name: this.$i18n.t('level.master'),
					value: 1
				}
			],
			modes: [
				{
					name: this.$i18n.t('mode.fullTime'),
					value: 0
				},
				{
					name: this.$i18n.t('mode.partTime'),
					value: 1
				}
			],
			profiles: [
				{
					name: this.$i18n.t('profile.generalAcademic'),
					value: 0
				}
			],
			chosenFaculty: null,
			chosenCourse: null,
			chosenLevel: null,
			chosenMode: null,
			chosenProfile: null,
			chosenIndexNumber: null,
			chosenMaximalNumberOfStudents: null,
			topicRules: [
				v => !!v || this.$i18n.t('rules.topic.required'),
				v => v.length >= 5 || this.$i18n.t('rules.topic.atLeast5Chars'),
				v => v.length <= 200 || this.$i18n.t('rules.topic.atMost100Chars')
			],
			descriptionRules: [
				v => !!v || this.$i18n.t('rules.description.required'),
				v => v.length >= 5 || this.$i18n.t('rules.description.atLeast5Chars'),
                //v => v.length <= 400 || this.$i18n.t('rules.description.atMost400Chars')
                v => v.length <= 1000 || this.$i18n.t('rules.description.atMost1000Chars')
			],
            outputDataRules: [
                v => v.length <= 1000 || this.$i18n.t('rules.outputData.atMost1000Chars')
                //v => v.length <= 400 || this.$i18n.t('rules.outputData.atMost400Chars')
			],
			specializationRules: [
				v => v.length <= 50 || this.$i18n.t('rules.specialization.atMost50Chars')
			],
			maxNumberOfStudentsRules: [
				v => !!v || this.$i18n.t('rules.maxNumberOfStudents.required'),
				v => v >= 1 || this.$i18n.t('rules.maxNumberOfStudents.atLeast1'),
				v => v <= 4 || this.$i18n.t('rules.maxNumberOfStudents.atMost4')
			],
			selectRules: [
				v => !!v || this.$i18n.t('rules.select.required')
			],
			//studentRules: [
			//	v => v ? v >= 1 || this.$i18n.t('rules.student.idAtLeast1') : true,
			//	v => v == "" ? this.$i18n.t('rules.student.idOnlyDigits') : false
			//]
		}
	},
	created() {
		this.selectCoursesDisabled = true;
		this.disabledStudentList = true;
		facultyService.getAll()
			.then(response => {
				if(response.status == 200) {
					this.faculties = response.data;
				}
			});
	}
})
</script>


<style lang="scss" scoped>
.table {
	background-color: #ffffff;
}
.submitButton {
    font-size: 18px !important;
	color: rgb(255, 255, 255) !important;
	width: 180px !important;
	height: 70px !important;
	margin-left: 50px;
	float: left;
}

.cancelButton {
	font-size: 18px !important;
	color: rgb(255, 255, 255) !important;
	width: 180px !important;
	height: 70px !important;
	margin-right: 50px;
	float: right;
}
</style>
